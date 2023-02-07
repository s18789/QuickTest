import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Student } from 'src/app/shared/utils/models/studentModel';

@Component({
  selector: 'app-student-dialog',
  templateUrl: './student-dialog.component.html',
  styleUrls: ['./student-dialog.component.css']
})

export class StudentDialogComponent implements OnInit{
  constructor(
    public dialogRef: MatDialogRef<StudentDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: Student[]
  ) {}

  ngOnInit(): void {
  }

  closeDialog() {
    var selectedStudents = this.data
      .filter(x => x.isSelected == true)
      .map(x => x.id);

    this.dialogRef.close(selectedStudents);
  }
}
