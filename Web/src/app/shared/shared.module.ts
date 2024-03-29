import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DialogComponent } from './components/dialog/dialog.component';
import { GridComponent } from './components/grid/grid.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { RouterModule } from '@angular/router';
import { PaginationComponent } from './components/pagination/pagination.component';
import { AngularSvgIconModule } from 'angular-svg-icon';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ModeComponent } from './components/mode/mode.component';
import { LanguageComponent } from './components/language/language.component';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { HttpClient } from '@angular/common/http';
import { ConfirmationDialogComponent } from './components/confirmation-dialog/confirmation-dialog.component';
import { ExamPreviewComponent } from './components/exam-preview/exam-preview.component';
import { QuestionComponent } from './components/exam-preview/ui/question/question.component';
import { DashboardCalendarComponent } from './components/dashboard-calendar/dashboard-calendar.component';
import { SpinnerComponent } from './components/spinner/spinner.component';
import { NotificationComponent } from './components/notification/notification.component';
import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';
import { TimerComponent } from './components/timer/timer.component';

export const createTranslateLoader = (http: HttpClient) => {
  return new TranslateHttpLoader(http);
};

@NgModule({
  declarations: [
    DialogComponent,
    GridComponent,
    PaginationComponent,
    ModeComponent,
    LanguageComponent,
    ConfirmationDialogComponent,
    ExamPreviewComponent,
    QuestionComponent,
    DashboardCalendarComponent,
    SpinnerComponent,
    NotificationComponent,
    PageNotFoundComponent,
    TimerComponent,
  ],
  imports: [
    CommonModule,
    FontAwesomeModule,
    RouterModule,
    FormsModule,
    ReactiveFormsModule,
    AngularSvgIconModule.forRoot(),
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: createTranslateLoader,
        deps: [HttpClient]
      }
    })
  ],
  exports: [
    DialogComponent,
    GridComponent,
    PaginationComponent,
    ModeComponent,
    LanguageComponent,
    ExamPreviewComponent,
    DashboardCalendarComponent,
    SpinnerComponent,
    NotificationComponent,
  ]
})

export class SharedModule { }
