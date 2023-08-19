import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { MembersComponent } from './components/members.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { AddGroupComponent } from './components/groups/dialogs/add-group/add-group.component';
import { AddGroupFormComponent } from './components/groups/dialogs/add-group-form/add-group-form.component';
import { ReactiveFormsModule } from '@angular/forms';
import { AddStudentComponent } from './components/students/dialogs/add-student/add-student.component';
import { AddStudentFormComponent } from './components/students/dialogs/add-student-form/add-student-form.component';
import { GroupsComponent } from './components/ui/groups/groups.component';
import { StudentsComponent } from './components/ui/students/students.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { AngularSvgIconModule } from 'angular-svg-icon';



@NgModule({
  declarations: [
    AddGroupComponent,
    AddGroupFormComponent,
    AddStudentComponent,
    AddStudentFormComponent,
    GroupsComponent,
    StudentsComponent,
    MembersComponent,
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
            component: MembersComponent,
          },
        ]
      },
    ]),
  ]
})
export class MembersModule { }
