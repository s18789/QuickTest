import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/core/main/services/auth.service';
import { ExamsService } from 'src/app/pages/exams/services/exams.service';
import { examDTO } from 'src/app/shared/utils/models/examDTO';
import { examResponseDTO } from 'src/app/shared/utils/models/examResponseDTO';

@Component({
  selector: 'app-exams',
  templateUrl: './exams.component.html',
  styleUrls: ['./exams.component.css']
})

export class ExamsComponent implements OnInit {
  exams!: examDTO[];

  configurations = [
    { displayName: "Exam name", key: "examName", width: "w-35/100" },
    { displayName: "Status", key: "status", width: "w-15/100" },
    { displayName: "Class", key: "class", width: "w-15/100" },
    { displayName: "Completed exams", key: "completedExams", width: "w-15/100" },
    { displayName: "Ending date", key: "endingDate", width: "w-15/100" }
  ];

  constructor(
    private examsService: ExamsService
  ) { }

  ngOnInit(): void {
    this.examsService.fetch().subscribe((response: examResponseDTO[]) => {
      this.exams = response.map(x => {
        var exam: examDTO = {
          id: x.id,
          class: x.class,
          completedExams: `${x.completedExams}/${x.allExams}`,
          endingDate: !x.availableTo
            ? "undefined"
            : new Date(x.availableTo).toLocaleDateString("pl-pl"),
          examName: x.title,
          status: x.status
        };
        return exam;
       });
    });
  }
}
