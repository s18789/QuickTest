import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TeachersComponent } from './components/teachers.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { ReactiveFormsModule } from '@angular/forms';
import { AngularSvgIconModule } from 'angular-svg-icon';
import { RouterModule } from '@angular/router';
import { AddTeacherComponent } from './components/dialogs/add-teacher/add-teacher.component';
import { AddTeacherFormComponent } from './components/dialogs/add-teacher-form/add-teacher-form.component';



@NgModule({
  declarations: [
    TeachersComponent,
    AddTeacherComponent,
    AddTeacherFormComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    FontAwesomeModule,
    ReactiveFormsModule,
    AngularSvgIconModule.forRoot(),
    RouterModule.forChild([
      {
        path: "",
        children: [
          {
            path: "",
            component: TeachersComponent,
          },
        ]
      },
    ]),
  ]
})
export class TeachersModule { }
