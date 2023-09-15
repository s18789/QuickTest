import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';

@Component({
  selector: 'app-timer',
  templateUrl: './timer.component.html',
  styleUrls: ['./timer.component.css']
})
export class TimerComponent implements OnChanges {
  @Input() finishTime: Date;

  hours: string;
  minutes: string;
  seconds: string;
  
  constructor() { }

  ngOnChanges(changes: SimpleChanges): void {
    const finishTime = changes['finishTime']?.currentValue;
    setInterval(() => this.calculateRemainingTime(finishTime), 1000);
  }

  calculateRemainingTime(finishDate: Date) {
    let now = new Date().getTime();
    let distance = new Date(finishDate).getTime() - now;

    let hours = Math.floor((distance % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
    let minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
    let seconds = Math.floor((distance % (1000 * 60)) / (1000));

    this.hours = hours >= 10
      ? `${hours}`
      : `0${hours}`;

    this.minutes = minutes >= 10
      ? `${minutes}`
      : `0${minutes}`;

    this.seconds = seconds >= 10
      ? `${seconds}`
      : `0${seconds}`;
  }

}
