import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { FormArray, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { tap } from 'rxjs';
import { ExamToSolveMapperService } from 'src/app/core/main/components/exam-to-solve/services/examToSolveMapper.service';
import { ExamSolveService } from 'src/app/core/main/services/examSolveService.service';
import { ExamPreviewType } from './enums/examPreviewType.enum';
import { ExamsResultsService } from 'src/app/pages/exams-results/services/exams-results.service';
import { CheckedExam } from './models/examPreview.model';
import { LoaderService } from '../../services/loaderService.service';
import { NotificationService } from '../../services/notification.service';

@Component({
  selector: 'app-exam-preview',
  templateUrl: './exam-preview.component.html',
  styleUrls: ['./exam-preview.component.css']
})
export class ExamPreviewComponent implements OnChanges {
  @Input() examPreviewForm: FormGroup;
  @Input() previewType: ExamPreviewType;
  @Input() finishExamTime: Date;

  currentQuestion: number = 0;
  ExamPreviewType = ExamPreviewType;
  intervalId: any;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private examSolveService: ExamSolveService,
    private examsResultsService: ExamsResultsService,
    private examToSolveMapperService: ExamToSolveMapperService,
    private loaderService: LoaderService,
    private notificationService: NotificationService,
  ) { }

  ngOnChanges(changes: SimpleChanges): void {
    const finishTime = changes['finishExamTime']?.currentValue;

    if (!finishTime) {
      return;
    }

    this.intervalId = setInterval(() => {
      let now = new Date().getTime();
      let distance = new Date(finishTime).getTime() - now;

      if (distance > 0) {
        return;
      }

      this.onSubmit(this.examPreviewForm);
    }, 1000);
  }

  nextQuestion() {
    this.currentQuestion++;
  }

  get questions() {
    return this.examPreviewForm.controls['questions'] as FormArray<FormGroup>;
  }

  onSubmit(target: any) {
    clearInterval(this.intervalId);
    var resolvedExam = this.examToSolveMapperService.mapExamToSolveToResolvedExam(this.examSolveService.examResultId, target.value);

    this.loaderService.show();
    this.examSolveService.finishExam(resolvedExam).pipe(
      tap(() => this.examSolveService.removeStorage()),
      tap(() => this.loaderService.hide()),
      tap(() => this.notificationService.showSuccess("Exam successfully completed.")),
      tap(() => this.router.navigate(['./examsResults'])),
    ).subscribe();
  }

  onCheckSubmit(target: any) {
    const examResultId: string = this.route.snapshot.params["id"];
    const checkedExam: CheckedExam = this.examToSolveMapperService.mapExamPreviewToCheckedExam(examResultId, target.value);

    this.loaderService.show();
    this.examsResultsService.checkExamResult(checkedExam).pipe(
      tap(() => this.router.navigate(['./exams'])),
      tap(() => this.loaderService.hide()),
    ).subscribe();

  }
}
