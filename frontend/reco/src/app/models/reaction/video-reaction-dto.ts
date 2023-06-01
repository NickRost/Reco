import { ReactionType } from '../common/reaction-type';

export interface VideoReactionDTO {
  id: number;
  userId: number;
  videoId: number;
  reaction: ReactionType;
}
