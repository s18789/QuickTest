import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable, map, tap } from 'rxjs';
import { ExamResult } from '../../models/examResult.model';
import { ExamsResultsService } from '../../services/exams-results.service';
import { ExamResultMapperService } from '../../services/examResultMapper.service';
import { ExamResultStatus } from '../../enums/examResultStatus.enum';
import { LoaderService } from 'src/app/shared/services/loaderService.service';

@Component({
  selector: 'app-exam-result',
  templateUrl: './exam-result.component.html',
  styleUrls: ['./exam-result.component.css']
})

export class ExamResultComponent implements OnInit {
  private readonly examResultId: string = this.route.snapshot.params["id"];
  examResult$: Observable<ExamResult>;
  ExamResultStatus = ExamResultStatus;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private examsResultsService: ExamsResultsService,
    private examResultMapperService: ExamResultMapperService,
    private loaderService: LoaderService
  ) { }

  ngOnInit(): void {
    this.examResult$ = this.getExamResult();
  }

  getExamResult(): Observable<ExamResult> {
    this.loaderService.show();
    return this.examsResultsService.get(this.examResultId).pipe(
      map((examResultResponse) => this.examResultMapperService.mapExamResultResponseToExamResult(examResultResponse)),
      tap(() => this.loaderService.hide()),
    );
  }

  getExamResultPreview() {
    this.router.navigate([`./examsResults/examResult/${this.examResultId}/preview`]);
  }
}
