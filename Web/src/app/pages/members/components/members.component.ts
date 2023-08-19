import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Observable, map } from 'rxjs';
import { DialogService } from 'src/app/shared/services/dialog.service';
import { MemberType } from '../enums/memberType.enum';
import { GroupGridModel } from '../models/groups/group.model';
import { Student } from '../models/students/student.model';
import { GroupService } from '../services/groups/group.service';
import { GroupMapperService } from '../services/groups/groupMapper.service';
import { StudentService } from '../services/students/student.services';
import { StudentMapperService } from '../services/students/studentMapper.service';
import { ADD_GROUP_DIALOG_OVERLAY_REF, AddGroupComponent } from './groups/dialogs/add-group/add-group.component';
import { ADD_STUDENT_DIALOG_OVERLAY_REF, AddStudentComponent } from './students/dialogs/add-student/add-student.component';
import { StudentResponse } from '../models/students/studentResponse.model';
import { ConfigurationItemType } from 'src/app/shared/utils/model/enums/configurationItemType.enum';
import { ActionConfiguration } from 'src/app/shared/utils/model/actionConfiguration.model';
import { GridItemConfiguration } from 'src/app/shared/utils/model/GridConfiguration.model';

@Component({
  selector: 'app-members',
  templateUrl: './members.component.html',
  styleUrls: ['./members.component.css']
})

export class MembersComponent implements OnInit {
  currentMemberType: MemberType = MemberType.Student;
  memberType: typeof MemberType = MemberType;

  students$: Observable<Student[]>;
  groups$: Observable<GroupGridModel[]>;

  studentsGridConfigurations: GridItemConfiguration[] = [
    { displayName: "", key: "", styles: "w-5/100" },
    { displayName: "First name", key: "firstName", styles: "w-1/5" },
    { displayName: "Last name", key: "lastName", styles: "w-1/5" },
    { displayName: "E-mail", key: "email", styles: "w-35/100" },
    { displayName: "Group", type: ConfigurationItemType.object, key: "group", nestedKey:"name", styles: "w-15/100" }
  ];

  studentsSearchConfiguration: ActionConfiguration = { propertyName: 'lastName' };
  studentsFilterConfiguration: ActionConfiguration = {
    propertyName: 'group',
    type: ConfigurationItemType.object,
    nestedPropertyName: 'name',
  };

  groupGridConfigurations = [
    { displayName: "Name", key: "name", styles: "w-3/5" },
    { displayName: "Students", key: "allStudents", styles: "w-35/100" },
  ];

  groupsSearchConfiguration: ActionConfiguration = { propertyName: 'name' };
  groupsFilterConfiguration: ActionConfiguration = { display: false };

  constructor(
    public dialog: MatDialog,
    private studentService: StudentService,
    private studentMapperService: StudentMapperService,
    private groupService: GroupService,
    private groupMapperService: GroupMapperService,
    private dialogService: DialogService,
    ) { }

  ngOnInit(): void {
    this.students$ = this.getStudents();
  }

  addNewMember() {
    this.currentMemberType == MemberType.Student
      ? this.openAddStudentDialog()
      : this.openAddGroupDialog();
  }

  openAddStudentDialog(): void {
    const componentRef = this.dialogService.openDialog<AddStudentComponent>(
      AddStudentComponent,
      ADD_STUDENT_DIALOG_OVERLAY_REF
    );

    componentRef.setInput("groups", this.getGroups());
    componentRef.onDestroy(() => this.students$ = this.getStudents());
  }

  openAddGroupDialog(): void {
    this.dialogService.openDialog<AddGroupComponent>(
      AddGroupComponent,
      ADD_GROUP_DIALOG_OVERLAY_REF
    ).onDestroy(() => this.groups$ = this.getGroups());
  }

  getStudents(): Observable<Student[]> {
    return this.studentService.getStudents().pipe(
      map((students) =>
        students.map((student: StudentResponse) =>
          this.studentMapperService.mapStudentResponseToStudent(student)))
    );
  }

  getGroups(): Observable<any[]> {
    return this.groupService.getGroups().pipe(
      map((groups) =>
       groups.map((group) =>
        this.groupMapperService.mapGroupResponseToGroup(group)))
    );
  }

  changeMemberType(type: MemberType) {
    this.currentMemberType = type;

    switch(type) {
      case MemberType.Group:
        this.groups$ = this.getGroups();
        break;
      case MemberType.Student:
        this.students$ = this.getStudents();
        break;
    }
  }
}

