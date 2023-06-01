import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';
import { Subject } from 'rxjs';
import { NewComment } from 'src/app/models/comment/new-comment';
import { ReactionType } from 'src/app/models/common/reaction-type';
import { VideoReactionDTO } from 'src/app/models/reaction/video-reaction-dto';
import { UserDto } from 'src/app/models/user/user-dto';
import { VideoDto } from 'src/app/models/video/video-dto';
import { CommentService } from 'src/app/services/comment.service';
import { RegistrationService } from 'src/app/services/registration.service';
import { VideoReactionService } from 'src/app/services/video-reactions.service';

@Component({
  selector: 'app-video-reactions',
  templateUrl: './video-reactions.component.html',
  styleUrls: ['./video-reactions.component.scss'],
})
export class VideoReactionsComponent implements OnInit {
  @Input() video: VideoDto;
  @Output() newReaction = new EventEmitter<boolean>();
  @Output() newCommentCreated = new EventEmitter<NewComment>();
  public currentUser: UserDto;
  public allReactions: VideoReactionDTO[];
  public unsubscribe$ = new Subject<void>();
  public isEditingMode = false;
  public newComment: NewComment;

  constructor(
    private reactionsService: VideoReactionService,
    private commentService: CommentService,
    private registrationService: RegistrationService
  ) {}

  ngOnInit(): void {
    this.allReactions = this.video.reactions;
    this.registrationService.getUser().subscribe((resp) => {
      this.currentUser = resp;
      this.updateNewComment();
    });
  }

  public addReaction(reactionNumber: number) {
    if (this.video == null) {
      return;
    }
    this.reactionsService.reactVideo(
      this.video,
      reactionNumber,
      this.currentUser.id,
      this.newReaction
    );
  }

  public toggleIsEditingMode() {
    this.isEditingMode = !this.isEditingMode;
  }

  public createComment() {
    this.newCommentCreated.emit(this.newComment);
    this.toggleIsEditingMode();
  }

  public GetReactions(reactionNumber: number) {
    this.allReactions = this.video?.reactions;
    switch (reactionNumber) {
      case 1:
        return this.allReactions.filter((x) => x.reaction == ReactionType.Like)
          .length;
      case 2:
        return this.allReactions.filter(
          (x) => x.reaction == ReactionType.Dislike
        ).length;
      case 3:
        return this.allReactions.filter((x) => x.reaction == ReactionType.Love)
          .length;
      case 4:
        return this.allReactions.filter((x) => x.reaction == ReactionType.Fun)
          .length;
      case 5:
        return this.allReactions.filter(
          (x) => x.reaction == ReactionType.Astonishment
        ).length;
      case 6:
        return this.allReactions.filter(
          (x) => x.reaction == ReactionType.Magically
        ).length;
      default:
        return 0;
    }
  }

  public updateNewComment() {
    this.newComment = {
      authorId: this.currentUser.id,
      videoId: this.video.id,
      body: '',
    };
  }
}
