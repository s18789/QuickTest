import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { AuthService } from 'src/app/core/main/services/auth.service';
import { Student } from '../../models/student.model';
import { StudentFormGroup } from '../../models/studentFormGroup';
import { StudentsService } from '../../services/students.services';

@Component({
  selector: 'app-add-student',
  templateUrl: './add-student.component.html',
  styleUrls: ['./add-student.component.css']
})
export class AddStudentComponent implements OnInit {
  studentForm!: FormGroup<StudentFormGroup>;

  constructor(
    private authService: AuthService,
    private studentsService: StudentsService,
    public dialogRef: MatDialogRef<AddStudentComponent>,
    @Inject(MAT_DIALOG_DATA) public data: Student
  ) {
  }

  ngOnInit(): void {
    this.createForm();
  }

  createForm() {
    this.studentForm = new FormGroup<StudentFormGroup>({
      firstName: new FormControl(this.data?.firstName, [Validators.required, Validators.minLength(2), Validators.maxLength(100)]),
      lastName: new FormControl(this.data?.lastName, [Validators.required, Validators.minLength(2), Validators.maxLength(100)]),
      email: new FormControl(this.data?.email, [Validators.required, Validators.email, Validators.maxLength(100)]),
      groupId: new FormControl(this.data?.groupId, [Validators.required])
    });
  }

  onSubmit(form: any) {
    if (!form.valid || !this.authService.access) {
      return;
    }

    if (!!this.data.id) {
      this.studentsService.update({
        id: this.data.id,
        firstName: form.value.firstName,
        lastName: form.value.lastName,
        email: form.value.email,
        groupId: form.value.groupId,
      }).subscribe();
    } else {
      this.studentsService.add(form.value).subscribe();
    }

    this.dialogRef.close();
  }
}
