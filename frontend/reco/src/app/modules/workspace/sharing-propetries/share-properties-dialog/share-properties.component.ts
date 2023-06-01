/* eslint-disable @typescript-eslint/no-explicit-any */
import { Component, OnInit, Inject, OnDestroy } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Subject } from 'rxjs';
import { VideoDto } from 'src/app/models/video/video-dto';
import { SnackBarService } from 'src/app/services/snack-bar.service';
import { VideoService } from 'src/app/services/video.service';

@Component({
  selector: 'app-share-properties',
  templateUrl: './share-properties.component.html',
  styleUrls: ['./share-properties.component.scss'],
})
export class SharePropertiesComponent implements OnInit, OnDestroy {
  public link = {} as string;
  public checked = false;
  public title: string;
  public videoId = {} as number;
  public currentVideo = {} as VideoDto;

  private unsubscribe$ = new Subject<void>();

  constructor(
    private snackBarService: SnackBarService,
    private dialogRef: MatDialogRef<SharePropertiesComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private videoService: VideoService
  ) {
    this.title = 'Get link';
  }

  public ngOnInit() {
    this.link = this.data.link;
    this.checked = this.data.checked;
    this.videoId = this.data.videoId;
    this.updateVideo();
  }

  public ngOnDestroy() {
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }

  public close() {
    this.dialogRef.close(false);
  }

  public openSnackBar() {
    this.snackBarService.openSnackBar('Link was successfully copied!');
  }

  public changeProperty() {
    this.checked = !this.checked;
    this.currentVideo.isPrivate = this.checked;
    this.videoService.updateVideo(this.currentVideo).subscribe();
    this.updateVideo();
    if (this.checked) {
      this.snackBarService.openSnackBar('This video is public now!');
    } else {
      this.snackBarService.openSnackBar('This video is private now!');
    }
  }

  public updateVideo() {
    this.videoService.getVideoById(this.videoId).subscribe((resp) => {
      if (resp.body != null) {
        this.currentVideo = resp.body;
      }
    });
  }
}
