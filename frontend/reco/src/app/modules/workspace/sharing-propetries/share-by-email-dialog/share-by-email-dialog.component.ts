/* eslint-disable @typescript-eslint/no-explicit-any */
import { Component, OnInit, Inject, OnDestroy } from '@angular/core';
import {
  MatDialog,
  MatDialogRef,
  MAT_DIALOG_DATA,
} from '@angular/material/dialog';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { ToastrService } from 'ngx-toastr';
import { SnackBarService } from 'src/app/services/snack-bar.service';
import { VideoService } from 'src/app/services/video.service';
import { VideoDto } from 'src/app/models/video/video-dto';
import { AccessForLinkService } from 'src/app/services/access-for-video-link.service';
import { AccessForUnregisteredUsersService } from 'src/app/services/access-for-unregistered-users.service';

@Component({
  selector: 'app-share-by-email',
  templateUrl: './share-by-email-dialog.component.html',
  styleUrls: ['./share-by-email-dialog.component.scss'],
})
export class ShareByEmailDialogComponent implements OnInit, OnDestroy {
  public email = {} as string;
  public link = {} as string;
  public title: string;
  public videoId = {} as number;
  public currentVideo = {} as VideoDto;
  public workspaceName = {} as string;

  private unsubscribe$ = new Subject<void>();

  constructor(
    private snackBarService: SnackBarService,
    private dialogRef: MatDialogRef<ShareByEmailDialogComponent>,
    private matDialog: MatDialog,
    private videoService: VideoService,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private toastr: ToastrService,
    private accessForRegisteredUsers: AccessForLinkService,
    private accessForUnregisteredUsers: AccessForUnregisteredUsersService
  ) {
    this.title = 'Share video with other users';
    this.email = 'Write an email';
  }

  public ngOnInit() {
    this.link = this.data.link;
    this.videoId = this.data.videoId;
    this.workspaceName = this.data.workspaceName;
  }

  public ngOnDestroy() {
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }

  public close() {
    this.dialogRef.close(false);
  }

  public sendLink() {
    this.videoService
      .sendLink({
        email: this.email,
        link: this.link,
      })
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe(() => {
        this.matDialog.closeAll();
        this.snackBarService.openSnackBar('Email was successfully sent!');
      });
    this.accessForUnregisteredUsers.addNewAccess(this.email, this.videoId);
  }
}
