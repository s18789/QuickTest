import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { TeacherFormGroup } from '../../../models/teacherFormGroup.model';

@Component({
  selector: 'app-add-teacher-form',
  templateUrl: './add-teacher-form.component.html',
  styleUrls: ['./add-teacher-form.component.css']
})
export class AddTeacherFormComponent implements OnInit {
  @Output() submittedForm = new EventEmitter<any>();
  @Output() formError = new EventEmitter<void>();

  studentForm: FormGroup<TeacherFormGroup>;

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
    });
  }


}
