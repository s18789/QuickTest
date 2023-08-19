import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { DashboardComponent } from './components/dashboard.component';
import { AdvertisementComponent } from './components/ui/advertisement/advertisement.component';
import { CalendarComponent } from './components/ui/calendar/calendar.component';
import { CreateExamComponent } from './components/ui/create-exam/create-exam.component';
import { LatestExamsComponent } from './components/ui/latest-exams/latest-exams.component';
import { RecentExamComponent } from './components/ui/recent-exam/recent-exam.component';
import { ScheduledExamsComponent } from './components/ui/scheduled-exams/scheduled-exams.component';
import { ScheduledExamCardComponent } from './components/ui/scheduled-exams/ui/scheduled-exam-card/scheduled-exam-card.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { TranslateModule } from '@ngx-translate/core';

@NgModule({
  declarations: [
    DashboardComponent,
    AdvertisementComponent,
    CalendarComponent,
    CreateExamComponent,
    LatestExamsComponent,
    RecentExamComponent,
    ScheduledExamsComponent,
    ScheduledExamCardComponent
  ],
  imports: [
    CommonModule,
    FontAwesomeModule,
    TranslateModule,
    RouterModule.forChild([
      {
        path: "",
        component: DashboardComponent,
      },
    ]),
  ],
})

export class DashboardModule { }
