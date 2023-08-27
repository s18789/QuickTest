import { Component, OnInit } from '@angular/core';
import { Observable, map, tap } from 'rxjs';
import { ExamsService } from 'src/app/pages/exams/services/exams.service';
import { ExamListItem } from '../../models/exam.model';
import { ExamMapperService } from '../../services/examMapper.service';
import { ActionConfiguration } from '../../../../shared/utils/model/actionConfiguration.model';
import { GridItemConfiguration } from 'src/app/shared/utils/model/GridConfiguration.model';
import { ExamStatus } from '../../enums/examStatus.enum';
import { ConfigurationItemType } from 'src/app/shared/utils/model/enums/configurationItemType.enum';
import { LoaderService } from 'src/app/shared/services/loaderService.service';

@Component({
  selector: 'app-exams',
  templateUrl: './exams.component.html',
  styleUrls: ['./exams.component.css']
})

export class ExamsComponent implements OnInit {
  exams$!: Observable<ExamListItem[]>;

  configurations: GridItemConfiguration[] = [
    { displayName: "Exam name", key: "title", styles: "w-35/100" },
    { displayName: "Status", key: "status", type: ConfigurationItemType.enum, enum: ExamStatus, styles: "w-15/100" },
    { displayName: "Class", key: "class", styles: "w-15/100" },
    { displayName: "Completed exams", key: "completedExams", styles: "w-15/100" },
    { displayName: "Ending date", key: "endingDate", styles: "w-15/100" }
  ];

  searchConfiguration: ActionConfiguration = { propertyName: 'title' };
  filterConfiguration: ActionConfiguration = { propertyName: 'class' };

  constructor(
    private examsService: ExamsService,
    private examMapperService: ExamMapperService,
    private loaderService: LoaderService,
  ) { }

  ngOnInit(): void {
    this.exams$ = this.getExams();
  }

  getExams(): Observable<ExamListItem[]> {
    this.loaderService.show();
    return this.examsService.getExams().pipe(
      map((exams) =>
        exams.map((exam) =>
          this.examMapperService.mapExamListItemResponseToExamListItem(exam))
      ),
      tap(() => this.loaderService.hide())
    )
  }
}
