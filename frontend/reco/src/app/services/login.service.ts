import { HttpClient, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { map, Observable } from 'rxjs';
import { AuthUserDto } from '../models/auth/auth-user-dto';
import { UserLoginDto } from '../models/auth/user-login-dto';
import { TokenDto } from '../models/token/token-dto';
import { UserDto } from '../models/user/user-dto';
import { ResourceService } from './resource.service';

@Injectable({
  providedIn: 'root',
})
export class LoginService extends ResourceService<UserLoginDto> {
  private user: UserDto = {} as UserDto;

  constructor(override httpClient: HttpClient, private router: Router) {
    super(httpClient);
  }

  getResourceUrl(): string {
    return '/login';
  }

  public login(user: UserLoginDto) {
    return this.handleAuthResponse(this.add<UserLoginDto, AuthUserDto>(user));
  }

  public areTokensExist(): boolean {
    return (
      !!localStorage.getItem('accessToken') &&
      !!localStorage.getItem('refreshToken')
    );
  }

  private handleAuthResponse(
    observable: Observable<HttpResponse<AuthUserDto>>
  ) {
    return observable.pipe(
      map((resp) => {
        this.setTokens(resp.body?.token as TokenDto);
        this.user = resp.body?.user as UserDto;
        return this.user;
      })
    );
  }

  public setTokens(tokens: TokenDto) {
    if (tokens && tokens.accessToken && tokens.refreshToken) {
      localStorage.setItem('accessToken', JSON.stringify(tokens.accessToken));
      localStorage.setItem('refreshToken', JSON.stringify(tokens.refreshToken));
    }
  }

  public logOut() {
    localStorage.removeItem('accessToken');
    localStorage.removeItem('refreshToken');
    this.router.navigate(['/login']);
  }
}
