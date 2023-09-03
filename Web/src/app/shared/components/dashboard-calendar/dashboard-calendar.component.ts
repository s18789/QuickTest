import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/core/main/services/auth.service';
import { CalendarExam } from 'src/app/shared/components/dashboard-calendar/models/calendarExam.model';
import { UserRole } from '../../enums/userRole.enum';

@Component({
  selector: 'app-dashboard-calendar',
  templateUrl: './dashboard-calendar.component.html',
  styleUrls: ['./dashboard-calendar.component.css']
})
export class DashboardCalendarComponent implements OnInit {
  @Input() exams: CalendarExam[];
  @Output() newMonthEvent = new EventEmitter<any>();

  todayDate: Date = new Date();
  currentDate: Date;
  calendar: any[];

  isMutipleExamsInOneDayOptionOpen: number = -1;
  examsInOneDayOptions: CalendarExam[];
  routName: string;

  constructor(
    private router: Router,
    private authService: AuthService,
  ) { }

  ngOnInit(): void {
    console.log(this.todayDate);
    this.routName = this.authService.getUserRole() == UserRole.Student
      ? "examsResults/examResult"
      : "exams";

    this.currentDate = new Date();
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
    this.currentDate = new Date(this.currentDate.getFullYear(), this.currentDate.getMonth() + month, 1);
    this.emitChangedMonth()
    this.initCalendarForMonth();
  }

  isAnyExamOnDay(day: number): boolean {
    return this.exams.some(e => e.dayOfMonth == day);
  }

  examsOnDay(day: number): number {
    return this.exams?.filter(e => e.dayOfMonth == day).length;
  }

  getDayTitle(day: number): string {
    let title = "";
    this.exams.filter(e => e.dayOfMonth == day).forEach((exam, index) => {
      title += index == 0
        ? exam.title
        : `, ${exam.title}`
    });

    return title;
  }

  openExamForDay(day: number): void {
    const examsOnDay = this.exams.filter(e => e.dayOfMonth == day);
    debugger;
    if (examsOnDay.length == 1) {
      if(!examsOnDay.at(0).id) {
        return;
      }

      this.router.navigate([`${this.routName}/${examsOnDay.at(0).id}`]);
      return;
    }

    // co z nie rozwiazanymi

    this.examsInOneDayOptions = examsOnDay;
    this.isMutipleExamsInOneDayOptionOpen = this.isMutipleExamsInOneDayOptionOpen == -1 ? day : -1;
  }

  changeRoute(id: string) {
    if(!id) {
      return;
    }
      
    this.router.navigate([`${this.routName}/${id}`]);
  }

  emitChangedMonth() {
    this.newMonthEvent.emit({
      month: this.currentDate.getMonth(),
      year: this.currentDate.getFullYear(),
    });
  }
}
