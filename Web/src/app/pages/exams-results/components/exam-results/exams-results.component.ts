import { Component, OnInit } from '@angular/core';
import { ExamsResultsService } from '../../services/exams-results.service';
import { ActivatedRoute } from '@angular/router';
import { Observable, map, tap } from 'rxjs';
import { ExamResultGridModel } from '../../models/examResult.model';
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

  examsResults: Observable<ExamResultGridModel[]>;

  configurations: GridItemConfiguration[] = [
    { displayName: "Exam name", key: "examName", styles: "w-50/100" },
    { displayName: "Status", key: "status", type: ConfigurationItemType.enum, enum: ExamResultStatus, styles: "w-15/100" },
    { displayName: "Points", key: "score", styles: "w-15/100" },
    { displayName: "Ending date", key: "endingDate", styles: "w-15/100" }
  ];

  searchConfiguration: ActionConfiguration = { propertyName: 'examName' };
  filterConfiguration: ActionConfiguration = { propertyName: 'examName' };

  constructor(
    private route: ActivatedRoute,
    private examsResultsService: ExamsResultsService,
    private examResultMapperService: ExamResultMapperService,
    private loaderService: LoaderService,
  ) { }

  ngOnInit(): void {
    this.examsResults = this.getExamsResults();
  }

  getExamsResults(): Observable<ExamResultGridModel[]> {
    this.loaderService.show();
    return this.examsResultsService.getExamsResults(this.studentId).pipe(
      map((examResults) =>
        examResults.map((examResult) =>
          this.examResultMapperService.mapExamResultGridModelResponseToExamResultGridModel(examResult))),
      tap(() => this.loaderService.hide()),
    );
  }
}
