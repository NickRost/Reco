import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginPageComponent } from './login-page/login-page.component';

import { RegisterPageComponent } from './register-page/register-page.component';
import { ResetDonePageComponent } from './reset-done-page/reset-done-page.component';
import { ResetPassPageComponent } from './reset-pass-page/reset-pass-page.component';


const routes: Routes = [
  {
    path: 'register',
    component: RegisterPageComponent,
  },
  {
    path: 'login',
    component: LoginPageComponent,
  },
  {
    path: 'reset-password',
    component: ResetPassPageComponent,
  },
  {
    path: 'reset-finish',
    component: ResetDonePageComponent,
  },
  {
    path: '',
    redirectTo: 'login',
    pathMatch: 'full',
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AuthRoutingModule {}
