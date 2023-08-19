import { Component, OnInit } from '@angular/core';

const maxShownDaysInCaledar: number = 42;

@Component({
  selector: 'app-calendar',
  templateUrl: './calendar.component.html',
  styleUrls: ['./calendar.component.css']
})

export class CalendarComponent implements OnInit {
  todayDate: Date = new Date();
  currentDate: Date;
  days: number[] = [];
  daysInWeek: number[][] = [
    [],[],[],[],[],[]
  ];

  constructor() { }

  ngOnInit(): void {
    this.currentDate = new Date();
    this.fillDaysInWeek();
  }

  getDaysInMonth(date: Date): number {
    return new Date(date.getFullYear(), date.getMonth() + 1, 0).getDate();
  }

  fillDaysInWeek(): void {
    let d = 0;
    this.fillDays();

    for(let i = 0; i < 6; i++) {
      for(let j = 0; j < 7; j++) {
        this.daysInWeek[i][j] = this.days[d];
        d++;
      }
    }
  }

  fillDays() {
    let dayOfWeek = new Date(this.currentDate.getFullYear(), this.currentDate.getMonth(), 1).getDay();
    dayOfWeek = dayOfWeek == 0
      ? 6
      : dayOfWeek - 1
    this.days = this.getLastDaysOfMonthBefore(dayOfWeek).concat(this.getDaysOfCurrentMonth());

    let daysToFill = maxShownDaysInCaledar - this.days.length;
    this.days = this.days.concat(this.getFirstDaysOfMonthAfter(daysToFill));
  }

  getDaysOfCurrentMonth(): number[] {
    let arrayDaysInMouth: number[] = [];
    for(let i = 0; i < this.getDaysInMonth(this.currentDate); i++) {
      arrayDaysInMouth[i] = i+1;
    }

    return arrayDaysInMouth;
  }

  getLastDaysOfMonthBefore(daysToFill: number): number[] {
    let arrayOfLastDaysOfMonthBefore: number[] = [];
    let monthBefore: number;
    let year: number;

    if (this.currentDate.getMonth() == 0) {
      monthBefore = 12;
      year = this.currentDate.getFullYear() - 1;
    } else {
      monthBefore = this.currentDate.getMonth() - 1;
      year = this.currentDate.getFullYear();
    }

    let daysInMonth = this.getDaysInMonth(new Date(year, monthBefore));
    let to = daysInMonth - daysToFill;

    for(let i = 0; daysInMonth > to; daysInMonth--, i++) {
      arrayOfLastDaysOfMonthBefore[i] = daysInMonth;
    }
    return arrayOfLastDaysOfMonthBefore.reverse();
  }

  getFirstDaysOfMonthAfter(daysToFill: number): number[] {
    let arrayOfLastDaysOfMonthAfter: number[] = [];

    for(let i = 0; i < daysToFill; i++) {
      arrayOfLastDaysOfMonthAfter[i] = i+1;
    }

    return arrayOfLastDaysOfMonthAfter;
  }

  nextMonth() {
    this.currentDate = this.currentDate.getMonth() == 11
      ? new Date(this.currentDate.getFullYear() + 1, 0)
      : new Date(this.currentDate.getFullYear(), this.currentDate.getMonth() + 1);

    this.fillDaysInWeek();
  }

  previousMonth() {
    this.currentDate = this.currentDate.getMonth() == 0
      ? new Date(this.currentDate.getFullYear() - 1, 11)
      : new Date(this.currentDate.getFullYear(), this.currentDate.getMonth() - 1);

    this.fillDaysInWeek();
  }
}
