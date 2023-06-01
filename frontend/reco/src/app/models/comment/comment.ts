import { CommentReactionDTO } from '../reaction/comment-reaction';

export interface Comment {
  id: number;
  createdAt: Date;
  videoID: number;
  authorId: number;
  body: string;
  reactions: CommentReactionDTO[];
}
