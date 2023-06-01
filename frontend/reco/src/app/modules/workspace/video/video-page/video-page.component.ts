import { Component } from '@angular/core';
import { SendDialogService } from 'src/app/services/send-dialog.service';
import { environment } from 'src/environments/environment';
import { VideoDto } from 'src/app/models/video/video-dto';
import { CommentService } from 'src/app/services/comment.service';
import { NewComment } from 'src/app/models/comment/new-comment';
import { ActivatedRoute } from '@angular/router';
import { SnackBarService } from 'src/app/services/snack-bar.service';
import { VideoService } from 'src/app/services/video.service';
import { RegistrationService } from 'src/app/services/registration.service';
import { Subject, takeUntil } from 'rxjs';

@Component({
  selector: 'app-video-page',
  templateUrl: './video-page.component.html',
  styleUrls: ['./video-page.component.scss'],
})
export class VideoPageComponent {
  public userWorkspaceName = {} as string;
  public viewsNumber: number;
  public videoId: number;
  public link: string;
  public checked: boolean = false;
  private unsubscribe$ = new Subject<void>();
  public currentVideo: VideoDto;
  public newComment = {} as NewComment;
  public isLoading = true;

  constructor(
    private activateRoute: ActivatedRoute,
    private sendDialogService: SendDialogService,
    private snackBarService: SnackBarService,
    private videoService: VideoService,
    private commentService: CommentService,
    private registrationService: RegistrationService
  ) {
    this.viewsNumber = 1;
    this.videoId = activateRoute.snapshot.params['id'];
    this.link = `${environment.appUrl}/shared/${this.videoId}`;
    this.updateVideo();
    this.videoService.getVideoById(this.videoId).subscribe((resp) => {
      if (resp.body) {
        this.checked = resp.body.isPrivate;
      }
    });
    this.registrationService
      .getUser()
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe((user) => {
        this.userWorkspaceName = user.workspaceName;
      });
  }

  public openSendDialog() {
    this.sendDialogService.openSendDialog(
      this.link,
      this.videoId,
      this.checked,
      this.userWorkspaceName
    );
  }

  public deleteComment(commentId: number) {
    this.commentService
      .deleteComment(commentId)
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe(() => {
        if (this.currentVideo != null) {
          this.currentVideo.comments = this.currentVideo.comments.filter(
            (comment) => comment.id !== commentId
          );
        }
      });
  }

  public sendComment(comment: NewComment) {
    if (this.currentVideo != null) {
      this.newComment.body = comment.body;
      this.newComment.authorId = this.currentVideo.authorId;
      this.newComment.videoId = this.currentVideo.id;
    }
    this.commentService
      .createComment(this.newComment)
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe((resp) => {
        if (resp && this.currentVideo != null) {
          this.newComment.body = '';
          this.updateVideo();
        }
      });
  }

  public updateVideo() {
    this.videoService
      .getVideoById(this.videoId)
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe((resp) => {
        if (resp.body != null) {
          this.currentVideo = resp.body;
        }
      });
  }

  public openSnackBar() {
    this.snackBarService.openSnackBar('Link was successfully copied!');
  }
}
