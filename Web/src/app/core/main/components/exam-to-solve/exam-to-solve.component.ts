import { Component, OnInit } from '@angular/core';
import { FormArray, FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ExamSolveService } from '../../services/examSolveService.service';
import { ExamResultDto } from './models/examResultDto.model';
import { AnswerFormGroup, ExamResultFormGroup, QuestionFormGroup } from './models/examResultFormGroup.model';

@Component({
  selector: 'app-exam-to-solve',
  templateUrl: './exam-to-solve.component.html',
  styleUrls: ['./exam-to-solve.component.css']
})
export class ExamToSolveComponent implements OnInit {
  currentQuestion: number = 0;
  examResultId!: number;
  examResultFormGroup!: FormGroup<ExamResultFormGroup>;

  constructor(
    private examSolveService: ExamSolveService,
    private route: ActivatedRoute,
    private router: Router,
  ) { }

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      if(!params['id']) {
        return;
      }

      this.examResultId = params['id'];
    });

    this.examSolveService.startExam(this.examResultId).subscribe((response: ExamResultDto) => {
      this.examResultFormGroup = new FormGroup<ExamResultFormGroup>(
        {
          examId: new FormControl(response.examId) as FormControl<number>,
          examResultId: new FormControl(response.examResultId) as FormControl<number>,
          questions: new FormArray<FormGroup<QuestionFormGroup>>(response.questions.map(x => new FormGroup<QuestionFormGroup>(
            {
              questionId: new FormControl(x.questionId) as FormControl<number>,
              content: new FormControl(x.content) as FormControl<string>,
              answers: new FormArray<FormGroup<AnswerFormGroup>>(x.answers.map(a => new FormGroup<AnswerFormGroup>(
                {
                  answerId: new FormControl(a.answerId) as FormControl<number>,
                  content: new FormControl(a.content) as FormControl<string>,
                  isSelected: new FormControl(false) as FormControl<boolean>,
                }
              )))
            }
          )))
        }
      )
    });
  }

  get questions() {
    return this.examResultFormGroup.controls.questions;
  }

  get answers() {
    return this.questions.at(this.currentQuestion).controls.answers;
  }

  onSubmit(form: any) {
    this.examSolveService.finishExam(form.value).subscribe();

    this.router.navigate(['./exams-results']);
  }
}
