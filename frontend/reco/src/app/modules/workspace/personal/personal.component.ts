import { Component, OnInit, ViewChild } from '@angular/core';
import { map, Subject, takeUntil } from 'rxjs';
import { UserDto } from 'src/app/models/user/user-dto';
import { RegistrationService } from 'src/app/services/registration.service';
import { FormGroup } from '@angular/forms';
import { FolderDto } from 'src/app/models/folder/folder-dto';
import { NewFolderDto } from 'src/app/models/folder/new-folder-dto';
import { FolderService } from 'src/app/services/folder.service';
import { MatMenuTrigger } from '@angular/material/menu';
import { ActivatedRoute, Router } from '@angular/router';
import { VideoDto } from 'src/app/models/video/video-dto';
import { VideoService } from 'src/app/services/video.service';
import { TimeService } from 'src/app/services/time.service';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { DialogComponent } from '../dialog/dialog.component';
import { environment } from 'src/environments/environment';
import { HttpParams } from '@angular/common/http';
import { SnackBarService } from 'src/app/services/snack-bar.service';
import { UpdateVideoDto } from 'src/app/models/video/update-video-dto';
import { UpdateVideoDialogComponent } from '../video/update-video-dialog/update-video-dialog.component';

@Component({
  selector: 'app-content',
  templateUrl: './personal.component.html',
  styleUrls: ['./personal.component.scss'],
})
export class PersonalComponent implements OnInit {
  public src = '../../assets/icons/test-user-logo.png';

  @ViewChild(MatMenuTrigger) menuTrigger: MatMenuTrigger = {} as MatMenuTrigger;

  public currentUser: UserDto = {} as UserDto;
  public avatarLink: string = '';
  public isFolderRouteActive = false;
  folderForm: FormGroup = {} as FormGroup;
  folder: FolderDto = {} as FolderDto;
  folders: FolderDto[] = [];
  selectedFolderName: string | undefined = '';
  selectedFolderId: number | undefined;

  public videos: VideoDto[] = [];

  private unsubscribe$ = new Subject<void>();

  displayedColumns: string[] = ['name', 'owner', 'details'];

  constructor(
    private registrationService: RegistrationService,
    private folderService: FolderService,
    private route: ActivatedRoute,
    private videoService: VideoService,
    private timeService: TimeService,
    public dialog: MatDialog,
    private router: Router,
    private snackBarService: SnackBarService
  ) {
    route.params.pipe(map((p) => p['id'])).subscribe((id) => {
      this.selectedFolderId = id;
    });
  }

  ngOnInit(): void {
    this.getAutorithedUser();
  }

  private getFolders() {
    return this.folderService
      .getAllFoldersByUserId(this.currentUser.id)
      .subscribe((result) => {
        this.folders = result;
        let name = this.folders.find(
          (f) => f.id == this.selectedFolderId
        )?.name;
        if (name === undefined) {
          this.selectedFolderName = '';
        } else {
          this.selectedFolderName = ' / ' + name;
        }
      });
  }

  private getVideos(id: number) {
    return this.videoService
      .getAllVideosWithoutFolderByUserId(id)
      .subscribe((res) => (this.videos = res));
  }

  private getAutorithedUser() {
    return this.registrationService
      .getUser()
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe((user) => {
        if (user.avatarLink === null) {
          user.avatarLink = '../../assets/icons/test-user-logo.png';
        }
        this.currentUser = user;
        this.getFolders();
        this.getVideos(user.id);
      });
  }

  createFolder(newFolder: NewFolderDto) {
    this.folderService.add(newFolder).subscribe((response) => {
      this.folder = response.body as FolderDto;
      this.getFolders();
    });
  }

  updateFolder(folderDto: FolderDto) {
    this.folderService.updateFolder(folderDto).subscribe(() => {
      this.getFolders();
    });
  }

  updateVideo(videoDto: UpdateVideoDto) {
    this.videoService.updateVideo(videoDto).subscribe(() => {
      this.getVideos(this.currentUser.id);
    });
  }

  showNewFolderForm(folder?: FolderDto) {
    const dialogConfig = new MatDialogConfig();

    dialogConfig.autoFocus = true;
    dialogConfig.data = folder === undefined ? '' : folder.name;

    const dialogRef = this.dialog.open(DialogComponent, dialogConfig);

    dialogRef.afterClosed().subscribe((folderName) => {
      //For creating new folder
      if (folder === undefined) {
        let newfolder: NewFolderDto = {
          name: folderName,
          authorId: this.currentUser.id,
          teamId: undefined,
        };
        this.createFolder(newfolder);
      }
      //For updating folder
      else {
        let folderUpdated: FolderDto = {
          id: folder.id,
          name: folderName,
          authorId: this.currentUser.id,
          author: folder.author,
          teamId: undefined,
        };
        this.updateFolder(folderUpdated);
      }
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

  public folderClick(folder: FolderDto) {
    this.selectedFolderName = ' / ' + folder.name;
    this.isFolderRouteActive = true;
  }

  public onMenuTriggered() {
    this.menuTrigger?.menu.focusFirstItem('mouse');
    this.menuTrigger?.openMenu();
  }

  public deleteFolder(id: number, name: string) {
    if (confirm('Are you sure you want to delete folder ' + name + ' ?')) {
      this.folderService.deleteFolder(id).subscribe(() => {
        this.selectedFolderName = '';
        this.router.navigate(['/personal']);
        this.getFolders();
      });
    }
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

  public calculateTimeDifference(oldDate: Date) {
    return this.timeService.calculateTimeDifference(oldDate);
  }
}
