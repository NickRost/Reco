import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { NavbarComponent } from './navbar/navbar.component';
import { SidebarComponent } from './sidebar/sidebar.component';
import { PersonalComponent } from './personal/personal.component';
import { BaseComponent } from './base/base.component';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatMenuModule } from '@angular/material/menu';
import { MatCardModule } from '@angular/material/card';
import { MatListModule } from '@angular/material/list';
import { MatTableModule } from '@angular/material/table';
import { BaseRoutingModule } from './workspace-routing.module';
import { MatDividerModule } from '@angular/material/divider';
import { VideoPageComponent } from './video/video-page/video-page.component';
import { VideoPlayerComponent } from './video/video-player/video-player.component';
import { VideoReactionsComponent } from './video/video-reactions/video-reactions.component';
import { VideoDescriptionComponent } from './video/video-description/video-description.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { VimeModule } from '@vime/angular';
import { VideoCommentsComponent } from './video/video-comments/video-comments.component';
import { VideoReactionService } from 'src/app/services/video-reactions.service';
import { CommentReactionService } from 'src/app/services/comment-reaction.service';
import { CommentService } from 'src/app/services/comment.service';
import { VideoService } from 'src/app/services/video.service';
import { CommentReactionsComponent } from './video/comment-reactions/comment-reactions.component';
import { SettingsComponent } from './settings/settings.component';
import { MatBadgeModule } from '@angular/material/badge';
import { NgxDropzoneModule } from 'ngx-dropzone';
import { MatDialogModule } from '@angular/material/dialog';
import { FolderComponent } from './folder/folder.component';
import { DialogComponent } from './dialog/dialog.component';
import { ShareByEmailDialogComponent }
  from './sharing-propetries/share-by-email-dialog/share-by-email-dialog.component';
import { SharePropertiesComponent } from './sharing-propetries/share-properties-dialog/share-properties.component';
import { SharedVideoPageComponent } from './video/shared-video/shared-video-page.component';
import { SharingVideoDialogComponent } from './sharing-propetries/sharing-video/sharing-video-dialog.component';
import { DialogDeleteComponent } from './dialog-delete/dialog-delete.component';
import { ToastrModule } from 'ngx-toastr';
import { ClipboardModule } from '@angular/cdk/clipboard';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { TeamInviteComponent } from './team-invite/team-invite.component';
import { InviteFinishComponent } from './invite-finish/invite-finish.component';
import { UpdateVideoDialogComponent } from './video/update-video-dialog/update-video-dialog.component';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

@NgModule({
  declarations: [
    NavbarComponent,
    SidebarComponent,
    PersonalComponent,
    BaseComponent,
    VideoPageComponent,
    VideoPlayerComponent,
    VideoReactionsComponent,
    VideoDescriptionComponent,
    VideoCommentsComponent,
    CommentReactionsComponent,
    SettingsComponent,
    FolderComponent,
    DialogComponent,
    ShareByEmailDialogComponent,
    SharePropertiesComponent,
    SharedVideoPageComponent,
    SharingVideoDialogComponent,
    DialogDeleteComponent,
    TeamInviteComponent,
    InviteFinishComponent,
    UpdateVideoDialogComponent,
  ],
  imports: [
    NgxDropzoneModule,
    CommonModule,
    BaseRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    MatButtonToggleModule,
    MatDialogModule,
    MatToolbarModule,
    MatSidenavModule,
    MatIconModule,
    MatButtonModule,
    MatMenuModule,
    MatDividerModule,
    MatCardModule,
    MatInputModule,
    MatListModule,
    MatTableModule,
    SharedModule,
    VimeModule,
    MatBadgeModule,
    FormsModule,
    MatDialogModule,
    ToastrModule.forRoot(),
    ClipboardModule,
    MatFormFieldModule,
    MatDialogModule,
    MatSelectModule,
    FormsModule,
    ReactiveFormsModule,
    MatProgressSpinnerModule,
  ],
  providers: [
    VideoReactionService,
    CommentReactionService,
    CommentService,
    VideoService,
    MatDialogModule,
    ClipboardModule,
    MatFormFieldModule,
    MatSelectModule,
    FormsModule,
    ReactiveFormsModule,
  ],
})
export class WorkspaceModule {}
