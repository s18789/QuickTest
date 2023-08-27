import { Component, OnInit } from '@angular/core';
import { Observable, combineLatest, map, tap } from 'rxjs';
import { CompletedExam, ExamToResolve } from '../models/exam.model';
import { ExamMapperService } from '../services/examMapper.service';
import { CompletedExamResponse, ExamToResolveResponse } from '../models/examResponse.model';
import { ExamsResultsService } from 'src/app/pages/exams-results/services/exams-results.service';
import { LoaderService } from 'src/app/shared/services/loaderService.service';

@Component({
  selector: 'app-student-dashboard',
  templateUrl: './student-dashboard.component.html',
  styleUrls: ['./student-dashboard.component.css']
})
export class StudentDashboardComponent implements OnInit {
  data$: Observable<{
    examsToResolve: ExamToResolve[];
    completedExams: CompletedExam[];
  }>;

  constructor(
    private examsResultsService: ExamsResultsService,
    private examMapperService: ExamMapperService,
    private readonly loaderService: LoaderService,
  ) { }

  ngOnInit(): void {
    this.fetchData();
  }

  fetchData() {
    this.loaderService.show()
    this.data$ = combineLatest([
      this.getExamsToResolve(),
      this.getCompletedExams(),
    ]).pipe(
      map(
        ([ examsToResolve, completedExams ]) => 
        ({ examsToResolve, completedExams })
      ),
      tap(() => this.loaderService.hide())
    );
  }

  getExamsToResolve(): Observable<ExamToResolve[]> {
    return this.examsResultsService.getExamsToResolve().pipe(
      map((examsToResolveResponse: ExamToResolveResponse[]) =>
        examsToResolveResponse.map((examToResolveResponse: ExamToResolveResponse) => 
          this.examMapperService.mapExamToResolveResponseToExamToResolve(examToResolveResponse)
      ))
    );
  }

  getCompletedExams(): Observable<CompletedExam[]> {
    return this.examsResultsService.getCompletedExams().pipe(
      map((completedExamsResponse: CompletedExamResponse[]) =>
      completedExamsResponse.map((completedExamResponse: CompletedExamResponse) => 
          this.examMapperService.mapCompletedExamResponseToCompletedExam(completedExamResponse)
      ))
    );
  }
}
