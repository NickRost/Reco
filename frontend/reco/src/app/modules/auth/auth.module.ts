import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RegisterPageComponent } from './register-page/register-page.component';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { ReactiveFormsModule } from '@angular/forms';
import { RegisterFormComponent } from './register-form/register-form.component';
import { AuthRoutingModule } from './auth-routing.module';
import { LoginPageComponent } from './login-page/login-page.component';
import { NgxTrimDirectiveModule } from 'ngx-trim-directive';
import { ResetPassPageComponent } from './reset-pass-page/reset-pass-page.component';
import { ResetDonePageComponent } from './reset-done-page/reset-done-page.component';

@NgModule({
  declarations: [
    RegisterPageComponent,
    RegisterFormComponent,
    LoginPageComponent,
    ResetPassPageComponent,
    ResetDonePageComponent,
  ],
  imports: [
    CommonModule,
    MatInputModule,
    MatFormFieldModule,
    MatIconModule,
    ReactiveFormsModule,
    AuthRoutingModule,
    NgxTrimDirectiveModule,
  ],
})
export class AuthModule {}
