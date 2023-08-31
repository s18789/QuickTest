import { Component } from '@angular/core';
import { AbstractControl, FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { AuthService } from 'src/app/core/main/services/auth.service';
import { ExamsService } from 'src/app/pages/exams/services/exams.service';
import { AnswerFormGroup, ExamFormGroup, QuestionFormGroup, StudentFormGroup } from '../../models/examFormGroup.model';
import { StudentDialogComponent } from './student-dialog/student-dialog.component';
import { Router } from '@angular/router';
import { StudentService } from 'src/app/pages/members/services/students/student.services';
import { KeyValue } from '@angular/common';
import { map, tap } from 'rxjs';
import { StudentMapperService } from 'src/app/pages/members/services/students/studentMapper.service';
import { StudentResponse } from 'src/app/pages/members/models/students/studentResponse.model';
import { StudentDialog } from 'src/app/pages/members/models/students/studentDialog.model';
import { QuestionType } from 'src/app/shared/enums/questionType.enum';
import { LoaderService } from 'src/app/shared/services/loaderService.service';
import { NotificationService } from 'src/app/shared/services/notification.service';

@Component({
  selector: 'app-add-exam',
  templateUrl: './add-exam.component.html',
  styleUrls: ['./add-exam.component.css']
})

export class AddExamComponent{
  QuestionType: typeof QuestionType = QuestionType;
  originalOrder = (a: KeyValue<string, QuestionType>, b: KeyValue<string, QuestionType>): number => {
    return 0;
  }
  examFormGroup: FormGroup<ExamFormGroup>;
  selectedQuestion: number = 0;
  errorMessage: string = "";
  submitted: boolean = false;

  constructor(
    private formBuilder: FormBuilder,
    private authService: AuthService,
    private examsService: ExamsService,
    private studentService: StudentService,
    private studentMapperService: StudentMapperService,
    public dialog: MatDialog,
    private router: Router,
    private loaderService: LoaderService,
    private notificationService: NotificationService,
    ) {
    this.examFormGroup = new FormGroup<ExamFormGroup>(
      {
        title: new FormControl('', [Validators.required, Validators.maxLength(100)]),
        time: new FormControl('', [Validators.required, Validators.min(1), Validators.max(480)]),
        availableFrom: new FormControl(''),
        availableTo: new FormControl(''),
        questions: this.formBuilder.array<FormGroup<QuestionFormGroup>>(
        [
          new FormGroup<QuestionFormGroup>({
            points: new FormControl('', [Validators.required, Validators.min(1), Validators.max(100)]),
            type: new FormControl(QuestionType.SingleChoice),
            questionContent: new FormControl('', [Validators.required, Validators.maxLength(300)]),
            answers: this.formBuilder.array<FormGroup<AnswerFormGroup>>(
            [
              new FormGroup<AnswerFormGroup>({
                answerContent: new FormControl('', [Validators.required, Validators.maxLength(300)]),
                correct: new FormControl(false, [Validators.required]),
              })
            ])
          })
        ]),
        students: new FormArray<FormGroup<StudentFormGroup>>([], [Validators.required])
      }
    );

    this.examFormGroup.statusChanges.subscribe(result => this.setErrorMessage());
   }

  addQuestion() {
    const questionForm = this.formBuilder.group({
      points: new FormControl('', [Validators.required, Validators.min(1), Validators.max(100)]),
      type: new FormControl(QuestionType.SingleChoice),
      questionContent: new FormControl('', [Validators.required, Validators.maxLength(300)]),
      answers: this.formBuilder.array(
        [
          this.formBuilder.group({
            answerContent: new FormControl('', [Validators.required, Validators.maxLength(300)]),
            correct: new FormControl(false, [Validators.required]),
          })
        ])
    });

    this.questions.push(questionForm);
    this.setSelectedQuestion(this.questions.length - 1);
  }

  addAnswer(content: string, disabledValue: boolean = false) {
    const answerForm = this.formBuilder.group({
      answerContent: new FormControl({value: content, disabled: disabledValue}, [Validators.required, Validators.maxLength(300)]),
      correct: new FormControl(false, [Validators.required]),
    });

    this.answers.push(answerForm);
  }

  setSelectedQuestion(index: number) {
    this.selectedQuestion = index;
  }

  removeQuestion(index: number) {
    this.questions.removeAt(index);
  }

  removeAnswer(index: number) {
    this.answers.removeAt(index);
  }

  isQuestionInvalid(index: number):boolean {
    var currentQuestionFormGroup = this.questions.at(index) as FormGroup;

    return Object.keys(currentQuestionFormGroup.controls)
      .some(key =>
        (currentQuestionFormGroup.controls[key]?.dirty || currentQuestionFormGroup.controls[key]?.touched || this.submitted) &&
        currentQuestionFormGroup.controls[key]?.invalid
      );
  }

  onQuestionTypeSelected(event: any) {
    var type: QuestionType = event.target.value;
    this.answers.clear();

    switch(type) {
      case QuestionType.Open:
        break;
      case QuestionType.YesNoChoice:
        this.addAnswer("Yes", true);
        this.addAnswer('No', true);
        break;
      default:
        this.addAnswer('');
        this.addAnswer('');
    }
  }

  clearRestCheckboxIfNeeded(event: any, selectedAnswerIndex: number) {
    var currentQuestionType: QuestionType = this.questions.at(this.selectedQuestion).controls['type'].value;

    if (!event.target.checked) {
      return;
    }

    if (currentQuestionType != QuestionType.SingleChoice && currentQuestionType != QuestionType.YesNoChoice) {
      return;
    }

    this.answers.controls.forEach(x => x.controls['correct']?.setValue(false));
    this.answers.controls.at(selectedAnswerIndex).controls['correct'].setValue(true);
  }

  setErrorMessage() {
    const examFormGroup = this.examFormGroup as FormGroup;

    if (!examFormGroup.invalid || !this.submitted) {
      this.errorMessage = "";
      return;
    }

    Object.keys(examFormGroup.controls).reverse().forEach(key => {
      this.setError(examFormGroup.controls[key]);

      if (key == "questions") {
        const questions = examFormGroup.controls[key] as FormArray<FormGroup>;

        questions.controls.forEach(question => {
          Object.keys(question.controls).forEach(questionKey => {
            this.setError(question.controls[questionKey]);

            if (questionKey == "answers") {
              const answers = question.controls[questionKey] as FormArray<FormGroup>;

              answers.controls.forEach(answer => {
                Object.keys(answer.controls).forEach(answerKey => {
                  this.setError(answer.controls[answerKey]);
                })
              })
            }
          })
        })
      }

      if (key == "students") {
        const students = examFormGroup.controls[key] as FormArray<FormGroup>;

        if (students.length < 1) {
          this.errorMessage = "No students added!";
        }
      }
    });
  }

  setError(control: AbstractControl) {
    if (!control.invalid) {
      return;
    }

    if (control.errors?.['required']) {
      this.errorMessage =  "Fill in the required fields!";
    } else if (control.errors?.['maxlength']) {
      this.errorMessage = "Character limit exceeded!";
    } else if (control.errors?.['min']) {
      this.errorMessage = "Field must be greater than 0!";
    } else if (control.errors?.['max']) {
      this.errorMessage = "Exceeded the possible range!"
    }
  }

  openStudentDialog() {
    this.studentService.getStudents().pipe(
      map((response: StudentResponse[]) => response.map(studentResponse => this.studentMapperService.mapStudentResponseToStudentDialog(studentResponse))),
      tap((students) => students.forEach(s => s.isSelected = this.students.controls.map(y => y.controls["id"]?.value).includes(s.id))),
      tap((students) => this.openDialog(students))
    ).subscribe();
  }

  openDialog(students: StudentDialog[]) {
    const dialogRef = this.dialog.open(StudentDialogComponent, {
      data: students
    });

    dialogRef.afterClosed().pipe(
      tap((selectedStudentsIndexes: string[] | null) => {
        if (!selectedStudentsIndexes) {
          return;
        }
  
        this.students.clear();
  
        selectedStudentsIndexes.forEach(selectedIndex => {
          this.students.push(new FormGroup<StudentFormGroup>({
            id: new FormControl(selectedIndex),
          }));
        })
      })
    ).subscribe();
  }

  onSubmit(form: any) {
    this.submitted = true;

    if (!form.valid) {
      this.setErrorMessage();
      return;
    }

    this.loaderService.show();
    var result =  {
        title: form.value.title,
        time: form.value.time,
        availableFrom: form.value.availableFrom,
        availableTo: form.value.availableTo,
        students: form.value.students,
        questions: form.value.questions.map((y: { points: any; type: string | number; questionContent: any; answers: any; }) => {
          var question = {
            points: y.points,
            type: +y.type,
            questionContent: y.questionContent,
            answers: y.answers
          };
          return question;
        })
      };
      
    if (this.authService.access) {
      this.examsService.add(result).pipe(
        tap(() => this.loaderService.hide()),
        tap(() => this.notificationService.showSuccess("Successfully created new exam.")),
        tap(() => this.router.navigate(['/exams']))
      ).subscribe();
    }
  }

  get questions() {
    return this.examFormGroup?.controls?.questions;
  }

  get answers() {
    return this.questions.at(this.selectedQuestion).controls.answers;
  }

  get students() {
    return this.examFormGroup.controls.students;
  }
}
