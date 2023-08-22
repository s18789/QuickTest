import { Component, OnInit } from '@angular/core';
import { ADD_TEACHER_DIALOG_OVERLAY_REF, AddTeacherComponent } from 'src/app/pages/teachers/components/dialogs/add-teacher/add-teacher.component';
import { DialogService } from 'src/app/shared/services/dialog.service';

@Component({
  selector: 'app-teacher',
  templateUrl: './teacher.component.html',
  styleUrls: ['./teacher.component.css']
})
export class TeacherComponent implements OnInit {

  constructor(
    private dialogService: DialogService,
  ) { }

  ngOnInit(): void {
  }

  openAddTeacherDialog(): void {
    this.dialogService.openDialog<AddTeacherComponent>(
      AddTeacherComponent,
      ADD_TEACHER_DIALOG_OVERLAY_REF
    );
  }

}
