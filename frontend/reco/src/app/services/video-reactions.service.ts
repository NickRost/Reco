import { HttpClient } from '@angular/common/http';
import { EventEmitter, Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { ReactionType } from '../models/common/reaction-type';
import { NewVideoReactionDTO } from '../models/reaction/new-video-reaction';
import { VideoDto } from '../models/video/video-dto';
import { ResourceService } from './resource.service';

@Injectable({
  providedIn: 'root',
})
export class VideoReactionService extends ResourceService<VideoDto> {
  constructor(override httpClient: HttpClient, private router: Router) {
    super(httpClient);
  }

  getResourceUrl(): string {
    return '/video/react';
  }

  public reactVideo(
    currentVideo: VideoDto,
    reactionType: ReactionType,
    userId: number,
    eventEmitter: EventEmitter<boolean>
  ) {
    this.add(
      this.addNewReaction(userId, currentVideo.id, reactionType)
    ).subscribe(() => {
      eventEmitter.emit(true);
    });
  }

  public addNewReaction(
    userId: number,
    videoId: number,
    reactionType: ReactionType
  ) {
    const newReaction: NewVideoReactionDTO = {
      videoId: videoId,
      userId: userId,
      reaction: reactionType,
    };
    return newReaction;
  }
}
