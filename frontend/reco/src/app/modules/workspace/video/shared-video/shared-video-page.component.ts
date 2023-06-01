import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { SnackBarService } from 'src/app/services/snack-bar.service';
import { VideoService } from 'src/app/services/video.service';
import { AccessForLinkService } from 'src/app/services/access-for-video-link.service';
import { RegistrationService } from 'src/app/services/registration.service';

@Component({
  selector: 'app-shared-video-page',
  templateUrl: './shared-video-page.component.html',
  styleUrls: ['./shared-video-page.component.scss'],
})
export class SharedVideoPageComponent {
  public isLoading = true;
  public viewsNumber: number;
  public videoId: number;
  public checked = true;
  public isPrivate?: boolean;
  private userId?: number;
  constructor(
    private activateRoute: ActivatedRoute,
    private snackBarService: SnackBarService,
    private videoService: VideoService,
    private accessForLinkService: AccessForLinkService,
    private registrationService: RegistrationService
  ) {
    this.viewsNumber = 10;
    this.videoId = parseInt(activateRoute.snapshot.params['id']);
    this.getUserId();
  }

  public getUserId() {
    this.registrationService.getUser().subscribe((resp) => {
      this.userId = resp.id;
      this.getVideo();
    });
  }

  public getVideo() {
    this.videoService.getVideoById(this.videoId).subscribe((resp) => {
      if (resp.body) {
        this.isPrivate = resp.body.isPrivate;
        this.checkAccess();
      }
    });
  }

  public checkAccess() {
    if (this.userId) {
      this.accessForLinkService
        .CheckAccessedUser(this.videoId, this.userId)
        .subscribe((resp) => {
          if (resp.body || this.isPrivate) {
            this.checked = false;
          }
          setTimeout(() => (this.isLoading = false), 1000);
        });
    }
  }

  public openSnackBar() {
    this.snackBarService.openSnackBar('Link was successfully copied!');
  }
}
