import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { AngularSvgIconModule } from 'angular-svg-icon';
import { SharedModule } from 'src/app/shared/shared.module';
import { ExamResultComponent } from './components/exam-result/exam-result.component';
import { ExamsResultsComponent } from './components/exam-results/exams-results.component';
import { AverageStatsComponent } from './components/exam-results/ui/average-stats/average-stats.component';
import { LatestExamComponent } from './components/exam-results/ui/latest-exam/latest-exam.component';
import { OthersStatsComponent } from './components/exam-results/ui/others-stats/others-stats.component';
import { ThersStatsCardComponent } from './components/exam-results/ui/others-stats/ui/thers-stats-card/thers-stats-card.component';
import { ExamResultPreviewComponent } from './components/exam-result-preview/exam-result-preview.component';

@NgModule({
  declarations: [
    ExamResultComponent,
    ExamsResultsComponent,
    AverageStatsComponent,
    LatestExamComponent,
    OthersStatsComponent,
    ThersStatsCardComponent,
    ExamResultPreviewComponent,
  ],
  imports: [
    CommonModule,
    SharedModule,
    FontAwesomeModule,
    AngularSvgIconModule.forRoot(),
    RouterModule.forChild([
      {
        path: "",
        children: [
          {
            path: "",
            component: ExamsResultsComponent,
          },
          {
            path: ":id",
            component: ExamsResultsComponent,
          },
          {
            path: "examResult",
            children: [
              {
                path: ":id",
                children: [
                  {
                    path: "",
                    component: ExamResultComponent
                  },
                  {
                    path: "preview",
                    component: ExamResultPreviewComponent
                  }
                ]
              }
            ]
          }
        ]
      },
    ]),
  ]
})
export class ExamsResultsModule { }
