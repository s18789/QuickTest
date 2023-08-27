import { Component, OnInit } from '@angular/core';
import { Observable, combineLatest, map, tap } from 'rxjs';
import { CreatedExam, ExamToCheck } from '../models/exam.model';
import { ExamsResultsService } from 'src/app/pages/exams-results/services/exams-results.service';
import { ExamMapperService } from '../services/examMapper.service';
import { ExamsService } from 'src/app/pages/exams/services/exams.service';
import { LoaderService } from 'src/app/shared/services/loaderService.service';

@Component({
  selector: 'app-teacher-dashboard',
  templateUrl: './teacher-dashboard.component.html',
  styleUrls: ['./teacher-dashboard.component.css']
})
export class TeacherDashboardComponent implements OnInit {
  data$: Observable<{
    examsToCheck: ExamToCheck[];
    createdExams: CreatedExam[];
  }>;

  constructor(
    private examsService: ExamsService,
    private examsResultsService: ExamsResultsService,
    private examMapperService: ExamMapperService,
    private readonly loaderService: LoaderService,
    ) { }

  ngOnInit(): void {
    this.fetchData();
  }

  fetchData() {
    this.loaderService.show();
    this.data$ = combineLatest([
      this.getExamsToCheck(),
      this.getCreatedExams(),
    ]).pipe(
      map(
        ([ examsToCheck, createdExams ]) => 
        ({ examsToCheck, createdExams })
      ),
      tap(() => this.loaderService.hide())
    );
  }

  getExamsToCheck(): Observable<ExamToCheck[]> {
    return this.examsResultsService.getExamsToCheck().pipe(
      map((examsToCheck: ExamToCheck[]) => 
        examsToCheck.map((examToCheck: ExamToCheck) => 
          this.examMapperService.mapExamToCheckResponseToExamToCheck(examToCheck)))
    );
  }

  getCreatedExams(): Observable<CreatedExam[]> {
    return this.examsService.getCreatedExams().pipe(
      map((createdExams: CreatedExam[]) => 
        createdExams.map((createdExam: CreatedExam) =>
          this.examMapperService.mapCreatedExamResponseToCreatedExam(createdExam)))
    );
  }
}
