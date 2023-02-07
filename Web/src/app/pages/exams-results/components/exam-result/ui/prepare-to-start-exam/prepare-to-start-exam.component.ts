import { Component, OnInit } from '@angular/core';
import { ExamSolveService } from 'src/app/core/main/services/examSolveService.service';

@Component({
  selector: 'app-prepare-to-start-exam',
  templateUrl: './prepare-to-start-exam.component.html',
  styleUrls: ['./prepare-to-start-exam.component.css']
})
export class PrepareToStartExamComponent implements OnInit {

  constructor(
    public examSolveService: ExamSolveService
  ) { }

  ngOnInit(): void {
  }
}
