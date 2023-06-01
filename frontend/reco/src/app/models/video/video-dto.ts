import { Comment } from '../comment/comment';
import { VideoReactionDTO } from '../reaction/video-reaction-dto';

export interface VideoDto {
  id: number;
  name: string;
  description: string;
  link: string;
  authorId: number;
  folderId: number;
  createdAt: Date;
  isPrivate: boolean;
  sharedEmails: string[];
  reactions: VideoReactionDTO[];
  comments: Comment[];
}
