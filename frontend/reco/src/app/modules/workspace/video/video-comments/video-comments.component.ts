import {
  Component,
  Input,
  OnDestroy,
  EventEmitter,
  Output,
  OnInit,
} from '@angular/core';
import { Subject, takeUntil } from 'rxjs';
import { VideoReactionDTO } from 'src/app/models/reaction/video-reaction-dto';
import { UserDto } from 'src/app/models/user/user-dto';
import { CommentReactionService } from 'src/app/services/comment-reaction.service';
import { Comment } from 'src/app/models/comment/comment';
import { CommentService } from 'src/app/services/comment.service';
import { UserService } from 'src/app/services/user.service';
import { RegistrationService } from 'src/app/services/registration.service';

@Component({
  selector: 'app-video-comments',
  templateUrl: './video-comments.component.html',
  styleUrls: ['./video-comments.component.scss'],
})
export class VideoCommentsComponent implements OnDestroy, OnInit {
  @Input() public comment: Comment;
  @Input() public currentUser: UserDto;
  @Output() deletedComment = new EventEmitter<number>();
  @Output() editedComment = new EventEmitter<Comment>();
  @Output() newReaction = new EventEmitter<boolean>();

  public commentAuthor: UserDto;
  public allReactions?: VideoReactionDTO[];
  public isEditingMode = false;
  private unsubscribe$ = new Subject<void>();
  public isCommentAuthor = false;
  public isLoading = true;
  constructor(
    private commentReactionService: CommentReactionService,
    private commentService: CommentService,
    private userService: UserService,
    private registrationService: RegistrationService
  ) {}

  public ngOnInit() {
    this.userService.getUserById(this.comment.authorId).subscribe((resp) => {
      if (resp.body) {
        this.commentAuthor = resp.body;
        if (this.commentAuthor.avatarLink === null) {
          this.commentAuthor.avatarLink =
            '../../assets/icons/test-user-logo.png';
        }
        this.registrationService.getUser().subscribe((resp) => {
          if (resp.id == this.commentAuthor.id) {
            this.isCommentAuthor = true;
          }
        });
        this.isLoading = false;
      }
    });
  }

  public ngOnDestroy() {
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }

  public toggleIsEditingMode() {
    this.isEditingMode = !this.isEditingMode;
  }

  public onDeleteComment() {
    this.deletedComment.emit(this.comment.id);
  }

  public onEditComment() {
    this.commentService
      .editComment({ ...this.comment, body: this.comment.body })
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe();
    this.toggleIsEditingMode();
  }

  public updateReaction() {
    this.newReaction.emit(true);
  }
}
