import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ExamsComponent } from './components/exam-list/exams.component';
import { ExamComponent } from './components/exam/exam.component';
import { AddExamComponent } from './components/add-exam/add-exam.component';
import { StudentDialogComponent } from './components/add-exam/student-dialog/student-dialog.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { AngularSvgIconModule } from 'angular-svg-icon';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from 'src/app/shared/shared.module';
import { CreatedExamPreviewComponent } from './components/created-exam-preview/created-exam-preview.component';



@NgModule({
  declarations: [
    StudentDialogComponent,
    AddExamComponent,
    ExamComponent,
    ExamsComponent,
    CreatedExamPreviewComponent,
  ],
  imports: [
    CommonModule,
    FontAwesomeModule,
    ReactiveFormsModule,
    SharedModule,
    FormsModule,
    AngularSvgIconModule.forRoot(),
    RouterModule.forChild([
      {
        path: "exams",
        children: [
          {
            path: "",
            pathMatch: "full",
            component: ExamsComponent,
          },
          {
            path: ":id",
            children: [
              {
                path: "",
                component: ExamComponent
              },
              {
                path: "preview",
                component: CreatedExamPreviewComponent
              }
            ]
          },
        ]
      },
      {
        path: "add-exam",
        component: AddExamComponent,
      }
    ]),
  ]
})
export class ExamsModule { }
