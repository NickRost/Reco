import { ReactionType } from '../common/reaction-type';

export interface NewVideoReactionDTO {
  userId: number;
  videoId: number;
  reaction: ReactionType;
}
