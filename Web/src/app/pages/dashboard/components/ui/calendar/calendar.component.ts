import { Component, OnInit } from '@angular/core';
import { CalendarExam } from '../../../../../shared/components/dashboard-calendar/models/calendarExam.model';
import { Observable, map, tap } from 'rxjs';
import { ExamsService } from 'src/app/pages/exams/services/exams.service';
import { CalendarExamMapperService } from '../../../../../shared/components/dashboard-calendar/services/calendarExamMapper.service';
import { AuthService } from 'src/app/core/main/services/auth.service';
import { UserRole } from 'src/app/shared/enums/userRole.enum';
import { ExamsResultsService } from 'src/app/pages/exams-results/services/exams-results.service';
import { LoaderService } from 'src/app/shared/services/loaderService.service';

@Component({
  selector: 'app-calendar',
  templateUrl: './calendar.component.html',
  styleUrls: ['./calendar.component.css']
})

export class CalendarComponent implements OnInit {
  exams$: Observable<CalendarExam[]>;

  constructor(
    private examSService: ExamsService,
    private examsResultsService: ExamsResultsService,
    private calendarExamMapper: CalendarExamMapperService,
    private authService: AuthService,
    private loaderService: LoaderService,
  ) { }

  ngOnInit(): void {
    const today: Date = new Date();
    this.exams$ = this.getExamsForMonth(today.getMonth(), today.getFullYear());
  }

  getExamsForMonth(month: number, year: number): Observable<CalendarExam[]> {
    this.loaderService.show();
    var exams = this.authService.getUserRole() == UserRole.Student
      ? this.examsResultsService.getCalendarExamsResults(month, year)
      : this.examSService.getCalendarExams(month, year);

    return exams.pipe(
      map((calendarExams: CalendarExam[]) =>
        calendarExams.map((calendarExam: CalendarExam) => 
          this.calendarExamMapper.mapCalendarExamResponseCalendarExam(calendarExam))),
      tap(() => this.loaderService.hide())
    );
  }

  getNewExamsForMonth(emittedValue: {month: number, year: number}) {
    this.exams$ = this.getExamsForMonth(emittedValue.month, emittedValue.year);
  }
}
