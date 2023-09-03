import { Component, Input, OnInit } from '@angular/core';
import { CreatedExam } from '../../../models/exam.model';

@Component({
  selector: 'app-latest-exams',
  templateUrl: './latest-exams.component.html',
  styleUrls: ['./latest-exams.component.css']
})
export class LatestExamsComponent implements OnInit {
  @Input() createdExam: CreatedExam;
  @Input() createdExams: CreatedExam[];
  
  main: boolean = true;

  constructor() { }

  ngOnInit(): void {
  }

}
