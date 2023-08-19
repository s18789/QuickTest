import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-scheduled-exam-card',
  templateUrl: './scheduled-exam-card.component.html',
  styleUrls: ['./scheduled-exam-card.component.css']
})
export class ScheduledExamCardComponent implements OnInit {
  @Input() scheduledExams: any;

  constructor() { }

  ngOnInit(): void {
  }

}
