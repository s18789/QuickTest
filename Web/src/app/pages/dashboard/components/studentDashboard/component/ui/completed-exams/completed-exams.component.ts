import { Component, Input, OnInit } from '@angular/core';
import { CompletedExam } from '../../../models/exam.model';

@Component({
  selector: 'app-completed-exams',
  templateUrl: './completed-exams.component.html',
  styleUrls: ['./completed-exams.component.css']
})
export class CompletedExamsComponent implements OnInit {
  @Input() completedExam: CompletedExam;
  @Input() completedExams: CompletedExam[];

  main: boolean = true;

  constructor() { }

  ngOnInit(): void {
  }

}
