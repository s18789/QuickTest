import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { AuthService } from 'src/app/core/main/services/auth.service';
import { ExamsService } from 'src/app/pages/exams/services/exams.service';
import { Student } from 'src/app/shared/utils/models/studentModel';
import { ExamFormGroup } from '../../models/examFormGroup';
import { QuestionFormGroup } from '../../models/questionFormGroup';
import { AnswerFormGroup } from '../../models/answerFormGroup';
import { StudentDialogComponent } from './student-dialog/student-dialog.component';
import { StudentFormGroup } from '../../models/studentFormGroup';
import { StudentsService } from 'src/app/pages/students/services/students.services';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-exam',
  templateUrl: './add-exam.component.html',
  styleUrls: ['./add-exam.component.css']
})

export class AddExamComponent implements OnInit {

  examFormGroup: FormGroup<ExamFormGroup>;
  selectedQuestion: number = 0;
  errorMessage: string = "";
  submitted: boolean = false;

  constructor(
    private formBuilder: FormBuilder,
    private authService: AuthService,
    private examsService: ExamsService,
    private studentService: StudentsService,
    public dialog: MatDialog,
    private router: Router
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

  ngOnInit(): void {
  }

  addQuestion() {
    const questionForm = this.formBuilder.group({
      points: new FormControl('', [Validators.required, Validators.min(1), Validators.max(100)]),
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

  addAnswer() {
    const answerForm = this.formBuilder.group({
      answerContent: new FormControl('', [Validators.required, Validators.maxLength(300)]),
      correct: new FormControl(false, [Validators.required]),
    });

    this.answers.push(answerForm);
  }

  setSelectedQuestion(index: number) {
    this.selectedQuestion = index;
  }

  isQuestionInvalid(index: number):boolean {
    var currentQuestionFormGroup = this.questions.at(index) as FormGroup;

    return Object.keys(currentQuestionFormGroup.controls)
      .some(key =>
        (currentQuestionFormGroup.controls[key]?.dirty || currentQuestionFormGroup.controls[key]?.touched || this.submitted) &&
        currentQuestionFormGroup.controls[key]?.invalid
      );
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
    this.studentService.fetch().subscribe((students: any[]) => {
      var returnStudents: Student[] = students.map(x => {
        var student: Student = {
          id: x.id,
          firstName: x.firstName,
          lastName: x.lastName,
          email: x.email,
          groupName: x.groupDto.name,
          isSelected: this.students.controls.map(y => y.controls["id"]?.value).includes(x.id),
        };
        return student;
      });

      this.openDialog(returnStudents);
    })
  }

  openDialog(students: Student[]) {

    const dialogRef = this.dialog.open(StudentDialogComponent, {
      width: '900px',
      height: '656px',
      data: students
    });

    dialogRef.afterClosed().subscribe((selectedStudentsIndexes: string[] | null) => {
      if (!selectedStudentsIndexes) {
        return;
      }

      this.students.clear();

      selectedStudentsIndexes.forEach(selectedIndex => {
        this.students.push(new FormGroup<StudentFormGroup>({
          id: new FormControl(selectedIndex),
        }));
      })
    });
  }

  onSubmit(form: any) {
    this.submitted = true;

    if (!form.valid) {
      this.setErrorMessage();
      return;
    }

    if (this.authService.access) {
      this.examsService.add(form.value).subscribe((response) => {
      });

      this.router.navigate(['/exams']);
    }
  }

  get questions() {
    return this.examFormGroup.controls.questions;
  }

  get answers() {
    return this.questions.at(this.selectedQuestion).controls.answers;
  }

  get students() {
    return this.examFormGroup.controls.students;
  }
}
