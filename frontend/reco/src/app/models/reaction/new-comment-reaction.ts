import { ReactionType } from '../common/reaction-type';

export interface NewCommentReactionDTO {
  userId: number;
  commentId: number;
  reaction: ReactionType;
}
