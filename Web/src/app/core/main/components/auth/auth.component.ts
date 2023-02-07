import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.css']
})
export class AuthComponent implements OnInit {
  authFormGroup: FormGroup;
  errorMessage: string | null = null;

  constructor(
    public authService: AuthService,
    private formBuilder: FormBuilder
    ) {
    this.authFormGroup = this.formBuilder.group({
      email: new FormControl('', [Validators.required, Validators.maxLength(69), Validators.email]),
      password: new FormControl('', [Validators.required, Validators.maxLength(69), Validators.minLength(6), Validators.pattern("^(?=.*?[A-Z])(?=(.*[a-z]){1,})(?=(.*[0-9]){1,}).{6,}$")])
    })
  }

  ngOnInit(): void {
  }

  onSubmit(form: FormGroup) {
    if (!form.valid){
      return;
    }

    this.authService.logIn(form.value).subscribe((response) => {
      this.errorMessage = null;
    }, (response) => {
      this.errorMessage = response.error.errorMessage
    })
  }
}
