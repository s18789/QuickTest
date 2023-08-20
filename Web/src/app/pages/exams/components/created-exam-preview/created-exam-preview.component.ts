import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { Observable, map, tap } from 'rxjs';
import { ExamsService } from '../../services/exams.service';
import { ExamPreviewResponse } from 'src/app/shared/components/exam-preview/models/examPreviewResponse.model';
import { ExamPreviewMapperService } from 'src/app/shared/components/exam-preview/services/examPreviewMapper.service';
import { ExamPreview, ExamPreviewForm } from 'src/app/shared/components/exam-preview/models/examPreview.model';
import { ActivatedRoute } from '@angular/router';
import { ExamPreviewType } from 'src/app/shared/components/exam-preview/enums/examPreviewType.enum';

@Component({
  selector: 'app-created-exam-preview',
  templateUrl: './created-exam-preview.component.html',
  styleUrls: ['./created-exam-preview.component.css']
})

export class CreatedExamPreviewComponent implements OnInit {
  private readonly examId: string = this.route.snapshot.params["id"];
  createdExamPreviw$: Observable<FormGroup<ExamPreviewForm>>
  ExamPreviewType = ExamPreviewType;

  constructor(
    private route: ActivatedRoute,
    private examsService: ExamsService,
    private examPreviewMapper: ExamPreviewMapperService
  ) { }

  ngOnInit() {
    this.createdExamPreviw$ = this.getExamPreviewForm();
  }

  getExamPreviewForm(): Observable<FormGroup<ExamPreviewForm>> {
    return this.examsService.GetCreatedExamPreview(this.examId).pipe(
      map((examPreviewResponse: ExamPreviewResponse) => this.examPreviewMapper.mapExamPreviewResponseToExamPreview(examPreviewResponse)),
      map((examToSolve: ExamPreview) => this.examPreviewMapper.mpaExamPreviewToExamPreviewForm(examToSolve)),
      tap((rrr) => {var z = rrr;debugger})
    );
  }

}
