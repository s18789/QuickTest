import { Component, Input, OnInit } from '@angular/core';
import { ExamToCheck } from '../../../models/exam.model';

@Component({
  selector: 'app-to-check-exams',
  templateUrl: './to-check-exams.component.html',
  styleUrls: ['./to-check-exams.component.css']
})
export class ToCheckExamsComponent implements OnInit {
  @Input() examToCheck: ExamToCheck;
  @Input() examsToCheck: ExamToCheck[];

  main: boolean = true;

  constructor() { }

  ngOnInit(): void {
  }

}
