import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { SignIn } from '../../models/sign-in';
import { SignInService } from '../../services/sign-in.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.css'],
})
export class SignInComponent implements OnInit {
  signin: SignIn = new SignIn();
  isSignUp: boolean = false;
  passwordFieldType: string = 'password';
  errorMessage: string = '';

  loginForm = new FormGroup({
    userName: new FormControl('', Validators.required),
    password: new FormControl('', [
      Validators.required,
      Validators.minLength(6),
      Validators.pattern(/^(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z0-9]).+$/),
    ]),
  });

  constructor(
    private router: Router,
    private signInService: SignInService,
    private toastrService: ToastrService,
    private route: ActivatedRoute
  ) {}

  ngOnInit() {
    this.route.url.subscribe((url) => {
      this.isSignUp = url[0].path === 'signup';
    });
    this.loginForm.get('userName')?.valueChanges.subscribe(() => {
      this.errorMessage = '';
    });
    this.loginForm.get('password')?.valueChanges.subscribe(() => {
      this.errorMessage = '';
    });
  }

  toggleAuthMode() {
    this.isSignUp = !this.isSignUp;
    const newPath = this.isSignUp ? 'signup' : 'signin';
    this.router.navigate(['/unauth', newPath]);
  }

  togglePasswordVisibility() {
    this.passwordFieldType =
      this.passwordFieldType === 'password' ? 'text' : 'password';
  }

  onSubmit() {
    if (this.loginForm.invalid) {
      this.loginForm.markAllAsTouched();
      return;
    }
    this.signin.userName = this.loginForm.get('userName')?.value as string;
    this.signin.password = this.loginForm.get('password')?.value as string;
    if (this.isSignUp) {
      this.signInService.signUp(this.signin).subscribe({
        next: (res) => {
          this.isSignUp = false;
          this.loginForm.reset();
          this.router.navigate(['/unauth/signin']);
          this.toastrService.success('User created successfully');
        },
        error: (error) => {
          if (error.error[0].code === 'DuplicateUserName') {
            this.errorMessage = 'Username already exists.';
          } else {
            this.errorMessage =
              'An error occurred during signup. Please try again.';
          }
        },
      });
    } else {
      this.signInService.signIn(this.signin).subscribe({
        next: (data) => {
          this.signInService.storeToken(data);
          this.router.navigate(['/auth']);
          this.toastrService.success('User Logged in successfully');
        },
        error: (error) => {
          console.log(error);
          this.errorMessage = 'Invalid username or password.';
        },
      });
    }
  }
}
