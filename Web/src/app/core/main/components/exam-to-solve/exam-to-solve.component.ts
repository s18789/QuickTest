import { Component, OnInit } from '@angular/core';
import { ExamToSolve, ExamToSolveForm } from './models/examToSolve.model';
import { Observable, map, tap } from 'rxjs';
import { ExamToSolveResponse } from './models/examToSolveResponse';
import { ExamSolveService } from '../../services/examSolveService.service';
import { ExamToSolveMapperService } from './services/examToSolveMapper.service';
import { FormGroup } from '@angular/forms';
import { ExamPreviewType } from 'src/app/shared/components/exam-preview/enums/examPreviewType.enum';

@Component({
  selector: 'app-exam-to-solve',
  templateUrl: './exam-to-solve.component.html',
  styleUrls: ['./exam-to-solve.component.css']
})
export class ExamToSolveComponent implements OnInit {
  examToSolveForm$: Observable<FormGroup<ExamToSolveForm>>
  ExamPreviewType = ExamPreviewType;

  constructor(
    private examSolveService: ExamSolveService,
    private examToSolveMapperService: ExamToSolveMapperService,
  ) { }

  ngOnInit() {
    this.examToSolveForm$ = this.getExamToSolveForm();
  }

  getExamToSolveForm(): Observable<FormGroup<ExamToSolveForm>> {
    return this.examSolveService.startExam().pipe(
      map((examToSolveResponse: ExamToSolveResponse) => this.examToSolveMapperService.mapExamToSolveResponseToExamToSolve(examToSolveResponse)),
      map((examToSolve: ExamToSolve) => this.examToSolveMapperService.mapExamToSolveToExamToSolveFormGroup(examToSolve))
    );
  }
}
