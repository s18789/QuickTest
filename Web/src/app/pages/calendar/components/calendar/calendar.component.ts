import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, map, tap } from 'rxjs';
import { AuthService } from 'src/app/core/main/services/auth.service';
import { ExamsResultsService } from 'src/app/pages/exams-results/services/exams-results.service';
import { ExamsService } from 'src/app/pages/exams/services/exams.service';
import { UserRole } from 'src/app/shared/enums/userRole.enum';
import { ExamHeader } from '../../models/exam.model';
import { ExamMapperService } from '../../services/examMapper.service';

@Component({
  selector: 'app-calendar',
  templateUrl: './calendar.component.html',
  styleUrls: ['./calendar.component.css']
})
export class CalendarComponent implements OnInit {
  exams: ExamHeader[];

  todayDate: Date = new Date();
  currentDate: Date;
  calendar: any[];


  constructor(
    private router: Router,
    private authService: AuthService,
    private examsService: ExamsService,
    private examsResultsService: ExamsResultsService,
    private examMapperService: ExamMapperService,
  ) { }

  ngOnInit(): void {
    this.currentDate = new Date();
    this.getCalendarExams().pipe(
      tap((response) => {debugger;this.exams = response})
    ).subscribe();
    this.initCalendarForMonth();
  }

  initCalendarForMonth() {
    this.calendar = []
    const daysInMonth = new Date(this.currentDate.getFullYear(), this.currentDate.getMonth() + 1, 0).getDate();
    let month = this.currentDate.getMonth();

    for (let i = 0, j = 0; i < 42; i++) {
      var dayOfWeek = new Date(this.currentDate.getFullYear(), month, j).getDay();

      if (i % 7 == dayOfWeek) {
        this.calendar.push(j);
        j++;
      } else {
        this.calendar.push(null);
      }

      if (j == daysInMonth) {
        j = 0;
        month++;
      }
    }
  }

  setMonth(month: number) {
    this.currentDate = new Date(this.currentDate.getFullYear(), this.currentDate.getMonth() + month, this.currentDate.getDate());
    this.getCalendarExams().pipe(
      tap((response) => this.exams = response)
    ).subscribe();
    this.initCalendarForMonth();
  }

  getCalendarExams(): Observable<ExamHeader[]> {
    return this.authService.getUserRole() == UserRole.Student
      ? this.getExamsResults()
      : this.getExams();
  }

  getExams(): Observable<ExamHeader[]> {
    return this.examsService.getExamsHeader(this.currentDate.getMonth(), this.currentDate.getFullYear()).pipe(
      map((examsHeader: ExamHeader[]) => 
        examsHeader.map((examHeader: ExamHeader) => 
          this.examMapperService.mapExamHeaderResponseToExamHeader(examHeader))
      )
    );
  }

  getExamsResults(): Observable<ExamHeader[]> {
    return this.examsResultsService.getExamsResultsHeader(this.currentDate.getMonth(), this.currentDate.getFullYear()).pipe(
      map((examsHeader: ExamHeader[]) => 
      examsHeader.map((examHeader: ExamHeader) => 
        this.examMapperService.mapExamHeaderResponseToExamHeader(examHeader))
      )
    );
  }

  getExamsCountForDay(day: number): number {
    return this.exams?.filter(e => new Date(e.availableTo)?.toDateString() == new Date(this.currentDate.getFullYear(), this.currentDate.getMonth(), day).toDateString()).length;
  }

  setCurrentDay(day: number) {
    this.currentDate = new Date(this.currentDate.getFullYear(), this.currentDate.getMonth(), day);
  }

  getExamsForCurrentDay() {
    return this.exams?.filter(e => new Date(e.availableTo)?.toDateString() == this.currentDate.toDateString());
  }
}
