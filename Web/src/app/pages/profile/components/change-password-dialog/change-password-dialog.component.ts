import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { atLeastOneNumberValidator, atLeastOneSpecialCharacterValidator, atLeastOneUpperCaseLetterValidator, atLeastOnelowerCaseLetterValidator } from '../../validators/password.validators';

@Component({
  selector: 'app-change-password-dialog',
  templateUrl: './change-password-dialog.component.html',
  styleUrls: ['./change-password-dialog.component.css']
})
export class ChangePasswordDialogComponent implements OnInit {
  submitted: boolean = false;
  passwordFormGroup: FormGroup;
  
  constructor(
    public dialogRef: MatDialogRef<ChangePasswordDialogComponent>,
    private formBuilder: FormBuilder,
    ) {}

  ngOnInit(): void {
    this.fightWithMaterialDialog();
    this.passwordFormGroup = this.formBuilder.group({
      password: new FormControl('', [Validators.required, Validators.maxLength(69), Validators.minLength(7), atLeastOneUpperCaseLetterValidator(), atLeastOnelowerCaseLetterValidator(), atLeastOneNumberValidator(), atLeastOneSpecialCharacterValidator()]),
      confirmationPassword: new FormControl('', [Validators.required, Validators.maxLength(69), Validators.minLength(7), atLeastOneUpperCaseLetterValidator(), atLeastOnelowerCaseLetterValidator(), atLeastOneNumberValidator(), atLeastOneSpecialCharacterValidator()])
    })
  }

  closeDialog(): void {
    this.dialogRef.close();
  }

  onSubmit(form: FormGroup) {
    if (!form.controls['password'].valid) {
      return;
    }

    this.submitted = true;

    if (form.controls['password']?.value != form.controls['confirmationPassword']?.value) {
      return;
    }

    this.dialogRef.close(form.value.password);
  }

  fightWithMaterialDialog(): void {
    for (let i = 0 ; i < 10; i++){
      var matDialogElement = document.getElementById('mat-dialog-'+i);

      if(!matDialogElement) {
        continue;
      }

      matDialogElement.style.padding = "0";
    }
  }
}
