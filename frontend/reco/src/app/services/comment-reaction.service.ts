import { HttpClient } from '@angular/common/http';
import { EventEmitter, Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { ReactionType } from '../models/common/reaction-type';
import { UserDto } from '../models/user/user-dto';
import { ResourceService } from './resource.service';
import { Comment } from '../models/comment/comment';
import { CommentReactionDTO } from '../models/reaction/comment-reaction';
import { NewCommentReactionDTO } from '../models/reaction/new-comment-reaction';

@Injectable({
  providedIn: 'root',
})
export class CommentReactionService extends ResourceService<Comment> {
  constructor(override httpClient: HttpClient, private router: Router) {
    super(httpClient);
  }

  getResourceUrl(): string {
    return '/comment/react';
  }

  private checkHasReaction(
    reactions: CommentReactionDTO[],
    currentUser: UserDto,
    reactionType: ReactionType
  ) {
    const hasReaction = reactions.some((x) => x.userId === currentUser.id);
    const hasSuchReaction = reactions.some(
      (x) => x.userId === currentUser.id && x.reaction === reactionType
    );
    return [hasReaction, hasSuchReaction];
  }

  public reactComment(
    currentComment: Comment,
    reactionType: ReactionType,
    userId: number,
    eventEmitter: EventEmitter<boolean>
  ) {
    this.add(
      this.addNewReaction(userId, currentComment.id, reactionType)
    ).subscribe(() => {
      eventEmitter.emit(true);
    });
  }

  public addNewReaction(
    userId: number,
    commentId: number,
    reactionType: ReactionType
  ) {
    const newReaction: NewCommentReactionDTO = {
      commentId: commentId,
      userId: userId,
      reaction: reactionType,
    };
    return newReaction;
  }

  public deleteReaction(currentUser: UserDto, currentComment: Comment) {
    const foundReaction = currentComment.reactions.find(
      (reaction) =>
        reaction.userId === currentUser.id &&
        reaction.commentId === currentComment.id
    );
    currentComment.reactions.filter((reaction) => reaction != foundReaction);
    return foundReaction;
  }
}
