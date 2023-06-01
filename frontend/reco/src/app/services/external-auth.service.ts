import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { GoogleLoginProvider } from 'angularx-social-login';
import { SocialAuthService, SocialUser } from 'angularx-social-login';
import { environment } from '../../environments/environment';
import { AuthOAuthDto } from '../models/auth/auth-oauth-dto';
import { AuthUserDto } from '../models/auth/auth-user-dto';

@Injectable({
  providedIn: 'root',
})
export class ExternalAuthService {
  private readonly APIUrl = environment.apiUrl;

  constructor(
    private http: HttpClient,
    private googleAuthService: SocialAuthService,
    private router: Router
  ) {}

  public signInWithGoogle = () => {
    return this.googleAuthService
      .signIn(GoogleLoginProvider.PROVIDER_ID)
      .then((res) => {
        const user: SocialUser = { ...res };
        this.validateExternalAuth(user);
      });
  };

  public signOutExternal = () => {
    this.googleAuthService.signOut();
  };

  private validateExternalAuth(user: SocialUser) {
    let auth: AuthOAuthDto = { provider: user.provider, idToken: user.idToken };

    return this.http
      .post<AuthUserDto>(`${this.APIUrl}/GoogleLogin`, auth)
      .subscribe({
        next: (data: AuthUserDto) => {
          localStorage.setItem(
            'accessToken',
            JSON.stringify(data.token.accessToken)
          );
          localStorage.setItem(
            'refreshToken',
            JSON.stringify(data.token.refreshToken)
          );
          this.router.navigate(['/personal']);
        },
        error: () => {
          this.signOutExternal();
        },
      });
  }
}
