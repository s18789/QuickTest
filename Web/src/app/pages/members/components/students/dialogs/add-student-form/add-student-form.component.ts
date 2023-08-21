import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { StudentFormGroup } from 'src/app/pages/members/models/students/studentFormGroup.model';

@Component({
  selector: 'app-add-student-form',
  templateUrl: './add-student-form.component.html',
  styleUrls: ['./add-student-form.component.css']
})

export class AddStudentFormComponent implements OnInit {
  @Output() submittedForm = new EventEmitter<any>();
  @Output() formError = new EventEmitter<void>();
  @Input() groups: any[];

  studentForm: FormGroup<StudentFormGroup>;

  constructor(private readonly fb: FormBuilder) { }

  ngOnInit(): void {
    this.initForm();
  }

  submit(): void {
    if (this.studentForm.invalid) {
      this.studentForm.markAllAsTouched();
      this.formError.emit();
      return;
    }

    this.submittedForm.emit(this.studentForm.value);
  }

  private initForm(): void {
    this.studentForm = this.fb.group(
    {
      firstName: new FormControl('', Validators.required),
      lastName: new FormControl('', Validators.required),
      email: new FormControl('', Validators.required),
      group: new FormGroup({
        id: new FormControl('', Validators.required)
      })
    });
  }

}
