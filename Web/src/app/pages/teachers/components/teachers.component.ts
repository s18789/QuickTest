import { Component, OnInit } from '@angular/core';
import { Observable, map } from 'rxjs';
import { GridItemConfiguration } from 'src/app/shared/utils/model/GridConfiguration.model';
import { ActionConfiguration } from 'src/app/shared/utils/model/actionConfiguration.model';
import { ConfigurationItemType } from 'src/app/shared/utils/model/enums/configurationItemType.enum';
import { TeacherService } from '../services/teacher.service';
import { Teacher } from '../models/teacher.model';
import { TeacherResponse } from '../models/teacherResponse.model';
import { DialogService } from 'src/app/shared/services/dialog.service';
import { ADD_TEACHER_DIALOG_OVERLAY_REF, AddTeacherComponent } from './dialogs/add-teacher/add-teacher.component';
import { TeacherMapperService } from '../services/teacherMapper.service';

@Component({
  selector: 'app-teachers',
  templateUrl: './teachers.component.html',
  styleUrls: ['./teachers.component.css']
})
export class TeachersComponent implements OnInit {
  teachers$: Observable<Teacher[]>;

  teachersGridConfigurations: GridItemConfiguration[] = [
    { displayName: "", key: "", styles: "w-5/100" },
    { displayName: "First name", key: "firstName", styles: "w-1/4" },
    { displayName: "Last name", key: "lastName", styles: "w-1/4" },
    { displayName: "E-mail", key: "email", styles: "w-4/10" },
  ];

  teachersSearchConfiguration: ActionConfiguration = { propertyName: 'lastName' };
  teachersFilterConfiguration: ActionConfiguration = {
    display: false
  };

  constructor(
    private teacherService: TeacherService,
    private teacherMapperService: TeacherMapperService,
    private dialogService: DialogService,
  ) { }

  ngOnInit(): void {
    this.teachers$ = this.getTeachers();
  }

  getTeachers(): Observable<Teacher[]> {
    // obsluga backendu
    return this.teacherService.getTeachers().pipe(
      map((teachers) =>
      teachers.map((teacher: TeacherResponse) =>
          this.teacherMapperService.mapTeacherResponseToTeacher(teacher)))
    );
  }

  openAddStudentDialog(): void {
    const componentRef = this.dialogService.openDialog<AddTeacherComponent>(
      AddTeacherComponent,
      ADD_TEACHER_DIALOG_OVERLAY_REF
    );

    componentRef.onDestroy(() => this.teachers$ = this.getTeachers());
  }

  // na koniec spięcie z dashboardem
  // na sam koniec zmianić ikony
}
