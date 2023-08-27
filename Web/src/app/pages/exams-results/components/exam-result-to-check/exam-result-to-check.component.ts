import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Observable, map, tap } from 'rxjs';
import { ExamPreviewType } from 'src/app/shared/components/exam-preview/enums/examPreviewType.enum';
import { ExamPreview, ExamPreviewForm } from 'src/app/shared/components/exam-preview/models/examPreview.model';
import { ExamsResultsService } from '../../services/exams-results.service';
import { ExamPreviewMapperService } from 'src/app/shared/components/exam-preview/services/examPreviewMapper.service';
import { ExamPreviewResponse } from 'src/app/shared/components/exam-preview/models/examPreviewResponse.model';
import { LoaderService } from 'src/app/shared/services/loaderService.service';

@Component({
  selector: 'app-exam-result-to-check',
  templateUrl: './exam-result-to-check.component.html',
  styleUrls: ['./exam-result-to-check.component.css']
})
export class ExamResultToCheckComponent implements OnInit {
  private readonly examResultId: string = this.route.snapshot.params["id"];
  ExamResultPreviw$: Observable<FormGroup<ExamPreviewForm>>
  ExamPreviewType = ExamPreviewType;

  constructor(
    private route: ActivatedRoute,
    private examsResultsService: ExamsResultsService,
    private examPreviewMapper: ExamPreviewMapperService,
    private loaderService: LoaderService,
  ) { }

  ngOnInit() {
    this.ExamResultPreviw$ = this.getExamPreviewForm();
  }

  getExamPreviewForm(): Observable<FormGroup<ExamPreviewForm>> {
    this.loaderService.show();
    return this.examsResultsService.GetExamResultPreview(this.examResultId).pipe(
      map((examPreviewResponse: ExamPreviewResponse) => this.examPreviewMapper.mapExamPreviewResponseToExamPreview(examPreviewResponse)),
      map((examToSolve: ExamPreview) => this.examPreviewMapper.mpaExamPreviewToExamPreviewForm(examToSolve)),
      tap(() => this.loaderService.hide()),
    );
  }
}
