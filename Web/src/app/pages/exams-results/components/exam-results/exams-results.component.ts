import { Component, OnInit } from '@angular/core';
import { ExamsResultsService } from '../../services/exams-results.service';
import { ActivatedRoute } from '@angular/router';
import { Observable, map, tap } from 'rxjs';
import { ExamResultGridModel, ExamsResults } from '../../models/examResult.model';
import { ExamResultMapperService } from '../../services/examResultMapper.service';
import { ActionConfiguration } from '../../../../shared/utils/model/actionConfiguration.model';
import { GridItemConfiguration } from '../../../../shared/utils/model/GridConfiguration.model';
import { ConfigurationItemType } from 'src/app/shared/utils/model/enums/configurationItemType.enum';
import { ExamResultStatus } from '../../enums/examResultStatus.enum';
import { LoaderService } from 'src/app/shared/services/loaderService.service';

@Component({
  selector: 'app-exams-results',
  templateUrl: './exams-results.component.html',
  styleUrls: ['./exams-results.component.css']
})
export class ExamsResultsComponent implements OnInit {
  private readonly studentId: string = this.route.snapshot.params["id"];

  examsResults: Observable<ExamsResults>;

  configurations: GridItemConfiguration[] = [
    { displayName: "Exam name", key: "examName", styles: "w-50/100" },
    { displayName: "Status", key: "status", type: ConfigurationItemType.enum, enum: ExamResultStatus, styles: "w-15/100" },
    { displayName: "Score", key: "score", type: ConfigurationItemType.score, styles: "w-15/100" },
    { displayName: "Valid to", key: "endingDate", type: ConfigurationItemType.date, styles: "w-15/100" }
  ];

  searchConfiguration: ActionConfiguration = { propertyName: 'examName' };
  filterConfiguration: ActionConfiguration = { propertyName: 'status', type: ConfigurationItemType.enum, enumType: ExamResultStatus, };

  constructor(
    private route: ActivatedRoute,
    private examsResultsService: ExamsResultsService,
    private examResultMapperService: ExamResultMapperService,
    private loaderService: LoaderService,
  ) { }

  ngOnInit(): void {
    this.examsResults = this.getExamsResults();
  }

  getExamsResults(): Observable<ExamsResults> {
    this.loaderService.show();
    return this.examsResultsService.getExamsResults(this.studentId).pipe(
      map((examResults) =>
          this.examResultMapperService.mapExamResultGridModelResponseToExamResultGridModel(examResults)),
      tap(() => this.loaderService.hide()),
    );
  }
}
