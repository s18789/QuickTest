import { Component, Input, OnInit } from '@angular/core';
import { FormArray, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { tap } from 'rxjs';
import { ExamToSolveMapperService } from 'src/app/core/main/components/exam-to-solve/services/examToSolveMapper.service';
import { ExamSolveService } from 'src/app/core/main/services/examSolveService.service';
import { ExamPreviewType } from './enums/examPreviewType.enum';

@Component({
  selector: 'app-exam-preview',
  templateUrl: './exam-preview.component.html',
  styleUrls: ['./exam-preview.component.css']
})
export class ExamPreviewComponent implements OnInit {
  @Input() examPreviewForm: FormGroup;
  @Input() previewType: ExamPreviewType;

  currentQuestion: number = 0;
  ExamPreviewType = ExamPreviewType;

  constructor(
    private router: Router,
    private examSolveService: ExamSolveService,
    private examToSolveMapperService: ExamToSolveMapperService,
  ) { }

  ngOnInit(): void {
  }

  nextQuestion() {
    this.currentQuestion++;
  }

  get questions() {
    return this.examPreviewForm.controls['questions'] as FormArray<FormGroup>;
  }

  onSubmit(target: any) {
    var resolvedExam = this.examToSolveMapperService.mapExamToSolveToResolvedExam(this.examSolveService.examResultId, target.value);
    debugger;

    this.examSolveService.finishExam(resolvedExam).pipe(
      tap(() => this.examSolveService.removeStorage()),
      tap(() => this.router.navigate(['./examsResults']))
    ).subscribe();
  }

}
