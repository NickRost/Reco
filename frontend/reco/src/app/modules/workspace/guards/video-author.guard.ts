import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router } from '@angular/router';
import { UserDto } from 'src/app/models/user/user-dto';
import { VideoDto } from 'src/app/models/video/video-dto';
import { RegistrationService } from 'src/app/services/registration.service';
import { VideoService } from 'src/app/services/video.service';

@Injectable({
  providedIn: 'root',
})
export class VideoAuthorGuard implements CanActivate {
  public currentUser = {} as UserDto;
  public currentVideo = {} as VideoDto;
  constructor(
    private registrationService: RegistrationService,
    private videoService: VideoService,
    private router: Router
  ) {}
  canActivate(route: ActivatedRouteSnapshot): boolean {
    this.videoService
      .getVideoById(route.queryParams['id'])
      .subscribe((resp) => {
        if (resp.body) {
          this.currentVideo = resp.body;
        }
      });
    this.registrationService.getUser().subscribe((resp) => {
      this.currentUser = resp;
    });
    if (this.currentUser.id != this.currentVideo.authorId) {
      this.router.navigate(['/personal']);
    }
    return true;
  }
}
