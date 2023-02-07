import { Component, OnInit } from '@angular/core';
import { ExamResultGridModel } from '../models/examResultGridModel.model';
import { ExamsResultsService } from '../services/exams-results.service';

@Component({
  selector: 'app-exams-results',
  templateUrl: './exams-results.component.html',
  styleUrls: ['./exams-results.component.css']
})
export class ExamsResultsComponent implements OnInit {
  examsResults!: ExamResultGridModel[];

  configurations = [
    { displayName: "Exam name", key: "examName", width: "w-50/100" },
    { displayName: "Status", key: "status", width: "w-15/100" },
    { displayName: "Points", key: "score", width: "w-15/100" },
    { displayName: "Ending date", key: "endingDate", width: "w-15/100" }
  ];

  constructor(
    private examsResultsService: ExamsResultsService
  ) { }

  ngOnInit(): void {
    this.examsResultsService.fetch().subscribe((response: ExamResultGridModel[]) => {
      this.examsResults = response;
    });
  }
}
