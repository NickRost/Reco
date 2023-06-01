import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Comment } from 'src/app/models/comment/comment';
import { ReactionType } from 'src/app/models/common/reaction-type';
import { CommentReactionDTO } from 'src/app/models/reaction/comment-reaction';
import { UserDto } from 'src/app/models/user/user-dto';
import { CommentReactionService } from 'src/app/services/comment-reaction.service';
import { RegistrationService } from 'src/app/services/registration.service';

@Component({
  selector: 'app-comment-reactions',
  templateUrl: './comment-reactions.component.html',
  styleUrls: ['./comment-reactions.component.scss'],
})
export class CommentReactionsComponent implements OnInit {
  @Input() public comment: Comment;
  @Input() public user: UserDto;
  @Output() newReaction = new EventEmitter<boolean>();
  public currentUser: UserDto;
  public allReactions: CommentReactionDTO[];
  public isLoading = true;

  constructor(
    private reactionsService: CommentReactionService,
    private registrationService: RegistrationService
  ) {
    this.allReactions = this.comment?.reactions;
    this.isLoading = false;
  }

  ngOnInit(): void {
    this.allReactions = this.comment.reactions;
    this.registrationService.getUser().subscribe((resp) => {
      this.currentUser = resp;
    });
  }

  public addReaction(reactionNumber: number) {
    if (this.comment == null) {
      return;
    }
    this.reactionsService.reactComment(
      this.comment,
      reactionNumber,
      this.currentUser.id,
      this.newReaction
    );
  }

  public GetReactions(reactionNumber: number) {
    this.allReactions = this.comment?.reactions;
    switch (reactionNumber) {
      case 1:
        return this.allReactions.filter((x) => x.reaction == ReactionType.Like)
          .length;
      case 2:
        return this.allReactions.filter(
          (x) => x.reaction == ReactionType.Dislike
        ).length;
      default:
        return 0;
    }
  }
}
