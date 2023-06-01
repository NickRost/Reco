import { HttpParams } from '@angular/common/http';
import { Component, Input, ViewChild } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { MatMenuTrigger } from '@angular/material/menu';
import { ActivatedRoute } from '@angular/router';
import { map } from 'rxjs';
import { UserDto } from 'src/app/models/user/user-dto';
import { UpdateVideoDto } from 'src/app/models/video/update-video-dto';
import { VideoDto } from 'src/app/models/video/video-dto';
import { SnackBarService } from 'src/app/services/snack-bar.service';
import { TimeService } from 'src/app/services/time.service';
import { VideoService } from 'src/app/services/video.service';
import { environment } from 'src/environments/environment';
import { UpdateVideoDialogComponent } from '../video/update-video-dialog/update-video-dialog.component';

@Component({
  selector: 'app-folder',
  templateUrl: './folder.component.html',
  styleUrls: ['./folder.component.scss'],
})
export class FolderComponent {
  @ViewChild(MatMenuTrigger) menuTrigger: MatMenuTrigger = {} as MatMenuTrigger;
  @Input() groupValue: string = 'grid';
  @Input() currentUser: UserDto = {} as UserDto;

  public videos: VideoDto[] = [];
  public folderId: number = 0;
  displayedColumns: string[] = ['name', 'owner', 'details'];

  constructor(
    private route: ActivatedRoute,
    private videoService: VideoService,
    private timeService: TimeService,
    private snackBarService: SnackBarService,
    public dialog: MatDialog
  ) {
    route.params.pipe(map((p) => p['id'])).subscribe((id) => {
      this.getVideos(id);
      this.folderId = id;
    });
  }

  public calculateTimeDifference(oldDate: Date) {
    return this.timeService.calculateTimeDifference(oldDate);
  }

  public onMenuTriggered() {
    this.menuTrigger?.menu.focusFirstItem('mouse');
    this.menuTrigger?.openMenu();
  }

  public deleteVideo(id: number) {
    if (confirm('Are you sure you want to delete the video ?')) {
      const url = environment.blobApiUrl + '/blob';
      const params = new HttpParams().set('id', id);

      this.videoService.deleteVideo(url, params).subscribe((response) => {
        if (response.status === 204) {
          this.getVideos(this.currentUser.id);
          this.snackBarService.openSnackBar('Video deleted successfully');
        } else {
          this.snackBarService.openSnackBar('Error');
        }
      });
    }
  }

  private getVideos(id: number) {
    return this.videoService
      .getAllVideosByFolderId(id)
      .subscribe((res) => (this.videos = res));
  }

  updateVideo(videoDto: UpdateVideoDto) {
    this.videoService.updateVideo(videoDto).subscribe(() => {
      this.getVideos(this.folderId);
    });
  }

  showVideoUpdateDialog(video: VideoDto) {
    const dialogConfig = new MatDialogConfig();

    dialogConfig.autoFocus = true;
    dialogConfig.data = video.name;

    const dialogRef = this.dialog.open(
      UpdateVideoDialogComponent,
      dialogConfig
    );

    dialogRef.afterClosed().subscribe((videoName) => {
      let videoUpdated: UpdateVideoDto = {
        id: video.id,
        name: videoName,
        isPrivate: false,
      };
      this.updateVideo(videoUpdated);
    });
  }
}
