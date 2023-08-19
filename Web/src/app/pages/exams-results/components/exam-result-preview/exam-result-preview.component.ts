import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { Observable, map } from 'rxjs';
import { ExamPreviewType } from 'src/app/shared/components/exam-preview/enums/examPreviewType.enum';
import { ExamPreview, ExamPreviewForm } from 'src/app/shared/components/exam-preview/models/examPreview.model';
import { ExamPreviewResponse } from 'src/app/shared/components/exam-preview/models/examPreviewResponse.model';
import { ExamsResultsService } from '../../services/exams-results.service';
import { ActivatedRoute } from '@angular/router';
import { ExamPreviewMapperService } from 'src/app/shared/components/exam-preview/services/examPreviewMapper.service';

@Component({
  selector: 'app-exam-result-preview',
  templateUrl: './exam-result-preview.component.html',
  styleUrls: ['./exam-result-preview.component.css']
})
export class ExamResultPreviewComponent implements OnInit {
  private readonly examResultId: string = this.route.snapshot.params["id"];
  ExamResultPreviw$: Observable<FormGroup<ExamPreviewForm>>
  ExamPreviewType = ExamPreviewType;

  constructor(
    private route: ActivatedRoute,
    private examsResultsService: ExamsResultsService,
    private examPreviewMapper: ExamPreviewMapperService
  ) { }

  ngOnInit() {
    this.ExamResultPreviw$ = this.getExamPreviewForm();
  }

  getExamPreviewForm(): Observable<FormGroup<ExamPreviewForm>> {
    return this.examsResultsService.GetExamResultPreview(this.examResultId).pipe(
      map((examPreviewResponse: ExamPreviewResponse) => this.examPreviewMapper.mapExamPreviewResponseToExamPreview(examPreviewResponse)),
      map((examToSolve: ExamPreview) => this.examPreviewMapper.mpaExamPreviewToExamPreviewForm(examToSolve))
    );
  }
}
