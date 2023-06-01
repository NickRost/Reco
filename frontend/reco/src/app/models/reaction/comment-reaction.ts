import { ReactionType } from '../common/reaction-type';

export interface CommentReactionDTO {
  id: number;
  userId: number;
  commentId: number;
  reaction: ReactionType;
}
