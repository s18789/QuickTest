import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable, tap } from 'rxjs';
import { ExamResult } from '../../models/examResult.model';
import { ExamsResultsService } from '../../services/exams-results.service';

@Component({
  selector: 'app-exam-result',
  templateUrl: './exam-result.component.html',
  styleUrls: ['./exam-result.component.css']
})

export class ExamResultComponent implements OnInit {
  examResultId!: string;
  examResult!: ExamResult;
  percentageResult!: number;

  constructor(
    private route: ActivatedRoute,
    private examsResultsService: ExamsResultsService
  ) { }

  ngOnInit(): void {
    this.route.queryParams.subscribe((params) => {
      this.examResultId = params['id'];
    })

    this.checkIsExamFinished().subscribe();
  }

  checkIsExamFinished(): Observable<ExamResult> {
    return this.examsResultsService.get(this.examResultId).pipe(
      tap((response: ExamResult) => {
        this.examResult = response;
        if (!!this.examResult.score && !!this.examResult.maxPoints) {
          this.percentageResult = this.examResult.score / this.examResult.maxPoints * 100;
        }
      })
    );
  }

}
