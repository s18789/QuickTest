import { Component, Input, OnInit } from '@angular/core';
import { FormArray, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { tap } from 'rxjs';
import { ExamToSolveMapperService } from 'src/app/core/main/components/exam-to-solve/services/examToSolveMapper.service';
import { ExamSolveService } from 'src/app/core/main/services/examSolveService.service';
import { ExamPreviewType } from './enums/examPreviewType.enum';
import { ExamsResultsService } from 'src/app/pages/exams-results/services/exams-results.service';
import { CheckedExam } from './models/examPreview.model';

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
    private route: ActivatedRoute,
    private examSolveService: ExamSolveService,
    private examsResultsService: ExamsResultsService,
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

    this.examSolveService.finishExam(resolvedExam).pipe(
      tap(() => this.examSolveService.removeStorage()),
      tap(() => this.router.navigate(['./examsResults']))
    ).subscribe();
  }

  onCheckSubmit(target: any) {
    const examResultId: string = this.route.snapshot.params["id"];
    const checkedExam: CheckedExam = this.examToSolveMapperService.mapExamPreviewToCheckedExam(examResultId, target.value);

    this.examsResultsService.checkExamResult(checkedExam).pipe(
      tap(() => this.router.navigate(['./exams']))
    ).subscribe();

  }
}
