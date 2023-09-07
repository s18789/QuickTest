import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { DashboardComponent } from './components/dashboard.component';
import { AdvertisementComponent } from './components/ui/advertisement/advertisement.component';
import { CalendarComponent } from './components/ui/calendar/calendar.component';
import { ScheduledExamsComponent } from './components/ui/scheduled-exams/scheduled-exams.component';
import { ScheduledExamCardComponent } from './components/ui/scheduled-exams/ui/scheduled-exam-card/scheduled-exam-card.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { TranslateModule } from '@ngx-translate/core';
import { AdminDashboardComponent } from './components/adminDashboard/components/admin-dashboard.component';
import { CreateExamComponent } from './components/teacherDashboard/components/ui/create-exam/create-exam.component';
import { LatestExamsComponent } from './components/teacherDashboard/components/ui/latest-exams/latest-exams.component';
import { RecentExamComponent } from './components/teacherDashboard/components/ui/recent-exam/recent-exam.component';
import { ImportFromFileComponent } from './components/adminDashboard/components/ui/import-from-file/import-from-file.component';
import { TeacherComponent } from './components/adminDashboard/components/ui/add-teacher/teacher.component';
import { TeacherDashboardComponent } from './components/teacherDashboard/components/teacher-dashboard.component';
import { StudentDashboardComponent } from './components/studentDashboard/component/student-dashboard.component';
import { StudentComponent } from './components/adminDashboard/components/ui/add-student/student.component';
import { ToCheckExamsComponent } from './components/teacherDashboard/components/ui/to-check-exams/to-check-exams.component';
import { ToResolveExamsComponent } from './components/studentDashboard/component/ui/to-resolve-exams/to-resolve-exams.component';
import { CompletedExamsComponent } from './components/studentDashboard/component/ui/completed-exams/completed-exams.component';
import { FeedbackComponent } from './components/studentDashboard/component/ui/feedback/feedback.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { ImportSummaryComponent } from './components/adminDashboard/components/ui/import-from-file/components/import-summary/import-summary.component';
import { CreateAccountsSummaryComponent } from './components/adminDashboard/components/ui/import-from-file/components/import-summary/create-accounts-summary/create-accounts-summary.component';

@NgModule({
  declarations: [
    DashboardComponent,
    CreateAccountsSummaryComponent,
    AdvertisementComponent,
    CalendarComponent,
    CreateExamComponent,
    LatestExamsComponent,
    RecentExamComponent,
    ScheduledExamsComponent,
    ScheduledExamCardComponent,
    ImportFromFileComponent,
    TeacherComponent,
    StudentComponent,
    AdminDashboardComponent,
    TeacherDashboardComponent,
    StudentDashboardComponent,
    ToCheckExamsComponent,
    ToResolveExamsComponent,
    CompletedExamsComponent,
    FeedbackComponent,
    ImportSummaryComponent
  ],
  imports: [
    SharedModule,
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
