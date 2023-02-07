import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ExamsService } from '../../services/exams.service';
import { Exam } from './models/exam.model';

@Component({
  selector: 'app-exam',
  templateUrl: './exam.component.html',
  styleUrls: ['./exam.component.css']
})
export class ExamComponent implements OnInit {
  examId!: number;
  exam!: Exam;
  completedExams!: number;
  configurations = [
    { displayName: "Full name", key: "fullName", width: "w-1/5" },
    { displayName: "E-mail", key: "email", width: "w-1/2" },
    { displayName: "Status", key: "status", width: "w-1/5" },
 // { displayName: "Finish time", key: "finishTime", width: "w-1/5" },
    { displayName: "Score", key: "score", width: "w-1/10" }
  ];

  constructor(
    private route: ActivatedRoute,
    private examsService: ExamsService
    ) {
      this.route.queryParams.subscribe(params => {
        this.examId = params['id'];
    });
     }

  ngOnInit(): void {
    this.examsService.get(this.examId.toString()).subscribe((response: Exam) => {
      this.exam = response;
      this.completedExams = this.exam.examResults.filter(x => x.status == 'Completed').length;
    });
  }

}
