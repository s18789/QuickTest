import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { GroupFormGroup } from 'src/app/pages/members/models/groups/groupFormGroup.model';

@Component({
  selector: 'app-add-group-form',
  templateUrl: './add-group-form.component.html',
  styleUrls: ['./add-group-form.component.css']
})
export class AddGroupFormComponent implements OnInit {
  @Output() submittedForm = new EventEmitter<any>();
  @Output() formError = new EventEmitter<void>();

  groupForm: FormGroup<GroupFormGroup>;

  constructor(private readonly fb: FormBuilder) { }

  ngOnInit(): void {
    this.initForm();
  }

  submit(): void {
    if (this.groupForm.invalid) {
      this.groupForm.markAllAsTouched();
      this.formError.emit();
      return;
    }

    this.submittedForm.emit(this.groupForm.value);
  }

  private initForm(): void {
    this.groupForm = this.fb.group(
    {
      name: new FormControl('', Validators.required)
    });
  }

}
