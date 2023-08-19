import { Component, OnInit } from '@angular/core';
import { Observable, map } from 'rxjs';
import { ExamsService } from 'src/app/pages/exams/services/exams.service';
import { ExamListItem } from '../../models/exam.model';
import { ExamMapperService } from '../../services/examMapper.service';
import { ActionConfiguration } from '../../../../shared/utils/model/actionConfiguration.model';
import { GridItemConfiguration } from '../../../../shared/utils/model/GridConfiguration.model';

@Component({
  selector: 'app-exams',
  templateUrl: './exams.component.html',
  styleUrls: ['./exams.component.css']
})

export class ExamsComponent implements OnInit {
  exams$!: Observable<ExamListItem[]>;

  configurations: GridItemConfiguration[] = [
    { displayName: "Exam name", key: "title", styles: "w-35/100" },
    { displayName: "Status", key: "status", styles: "w-15/100" },
    { displayName: "Class", key: "class", styles: "w-15/100" },
    { displayName: "Completed exams", key: "completedExams", styles: "w-15/100" },
    { displayName: "Ending date", key: "endingDate", styles: "w-15/100" }
  ];

  searchConfiguration: ActionConfiguration = { propertyName: 'title' };
  filterConfiguration: ActionConfiguration = { propertyName: 'class' };

  constructor(
    private examsService: ExamsService,
    private examMapperService: ExamMapperService
  ) { }

  ngOnInit(): void {
    this.exams$ = this.getExams();
  }

  getExams(): Observable<ExamListItem[]> {
    return this.examsService.getExams().pipe(
      map((exams) =>
        exams.map((exam) =>
          this.examMapperService.mapExamListItemResponseToExamListItem(exam)
          )
      ),
    )
  }
}
