import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import {
  cannotContainSpace,
  passwordMatchValidator,
} from 'src/app/core/validators/customValidators';
import { UserDto } from 'src/app/models/user/user-dto';
import jwt_decode from 'jwt-decode';
import { UserService } from 'src/app/services/user.service';
import { LoginService } from 'src/app/services/login.service';

@Component({
  selector: 'app-reset-done',
  templateUrl: './reset-done-page.component.html',
  styleUrls: ['./reset-done-page.component.scss'],
})
export class ResetDonePageComponent implements OnInit {
  public registerForm: FormGroup = {} as FormGroup;
  public email: string = '-';
  public token: string = '';

  public hidePass = true;
  public hideConfirmPass = true;
  public currentUser: UserDto = {} as UserDto;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private formBuilder: FormBuilder,
    private userService: UserService,
    private loginService: LoginService
  ) {
    this.validateForm();
  }

  ngOnInit() {
    this.route.queryParams.subscribe((params) => {
      this.token = params['token'];
      let decoded: { name: string; email: string; id: string } = jwt_decode(
        this.token
      );
      this.email = decoded.email;
    });
  }

  private validateForm() {
    this.registerForm = this.formBuilder.group(
      {
        confirmPassword: [, [Validators.required]],
        password: [
          ,
          [
            Validators.required,
            Validators.minLength(8),
            Validators.maxLength(20),
            Validators.pattern('^(?=.*[0-9])(?=.*[a-zA-Z])([a-zA-Z0-9]+)$'),
            cannotContainSpace,
          ],
        ],
      },
      {
        validator: passwordMatchValidator,
      }
    );
  }

  public registerUser() {
    this.userService
      .resetPasswordFinish(this.email, this.registerForm.value.password)
      .subscribe((data) => {
        this.loginService.setTokens(data.body!.token);
        this.router.navigate(['/personal']);
      });
  }
}
