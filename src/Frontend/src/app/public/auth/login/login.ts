import { Component } from '@angular/core';
import { UserService } from '../../../services/user-service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserModel } from '../../../models/user.model';

@Component({
  selector: 'app-login',
  standalone: false,
  templateUrl: './login.html',
  styleUrl: './login.scss'
})
export class Login {
  loginForm: FormGroup;
  userModel: UserModel = new UserModel();
  
  constructor(private fb: FormBuilder, private userService: UserService) {
    this.loginForm = this.fb.group({
      email: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

  ngOnInit(): void {
    // Listen for changes
    this.loginForm.valueChanges.subscribe(form => {
      this.userModel.email = form.email;
      this.userModel.password = form.password;
    });
  }

  login(): void {
    if (this.loginForm.invalid) {
      console.log('invalid');
      return;
    }

    this.userService
      .login(this.userModel.email, this.userModel.password)
      .subscribe({
        next: (res) => {
          console.log('Login success:', res);
        },
        error: (err) => {
          console.error('Login failed:', err);
        }
      });
  }
}
