import { Component, OnInit } from '@angular/core';
import { Observable, map } from 'rxjs';
import { ADD_STUDENT_DIALOG_OVERLAY_REF, AddStudentComponent } from 'src/app/pages/members/components/students/dialogs/add-student/add-student.component';
import { GroupService } from 'src/app/pages/members/services/groups/group.service';
import { GroupMapperService } from 'src/app/pages/members/services/groups/groupMapper.service';
import { DialogService } from 'src/app/shared/services/dialog.service';

@Component({
  selector: 'app-student',
  templateUrl: './student.component.html',
  styleUrls: ['./student.component.css']
})
export class StudentComponent implements OnInit {

  constructor(
    private dialogService: DialogService,
    private groupService: GroupService,
    private groupMapperService: GroupMapperService,
  ) { }

  ngOnInit(): void {
  }

  openAddStudentDialog(): void {
    const componentRef = this.dialogService.openDialog<AddStudentComponent>(
      AddStudentComponent,
      ADD_STUDENT_DIALOG_OVERLAY_REF
    );

    componentRef.setInput("groups", this.getGroups());
  }

  getGroups(): Observable<any[]> {
    return this.groupService.getGroups().pipe(
      map((groups) =>
       groups.map((group) =>
        this.groupMapperService.mapGroupResponseToGroup(group)))
    );
  }
}
