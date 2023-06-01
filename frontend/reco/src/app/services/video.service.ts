import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { map } from 'rxjs';
import { ISendLink } from '../models/mail/send-link';
import { UpdateVideoDto } from '../models/video/update-video-dto';
import { VideoDto } from '../models/video/video-dto';
import { RequestService } from './request.service';
import { ResourceService } from './resource.service';

@Injectable({ providedIn: 'root' })
export class VideoService extends ResourceService<VideoDto> {
  getResourceUrl(): string {
    return '/video';
  }

  constructor(
    override httpClient: HttpClient,
    private router: Router,
    private requestService: RequestService
  ) {
    super(httpClient);
  }

  public getAllVideos() {
    return this.getList();
  }

  public getAllVideosByFolderId(id: number) {
    return this.getFullRequest<VideoDto>(`video/folder/${id}`).pipe(
      map((resp) => {
        return resp.body as unknown as VideoDto[];
      })
    );
  }

  public getAllVideosWithoutFolderByUserId(id: number) {
    return this.getFullRequest<VideoDto>(`video/user/${id}`).pipe(
      map((resp) => {
        return resp.body as unknown as VideoDto[];
      })
    );
  }

  public getVideoById(videoId: number) {
    return this.get(videoId);
  }

  public deleteVideo(url: string, params?: HttpParams) {
    return this.requestService.delete(url, params).pipe(
      map((response) => {
        return response;
      })
    );
  }

  public updateVideo(resource: UpdateVideoDto) {
    return this.update<UpdateVideoDto, UpdateVideoDto>(resource).pipe(
      map((response) => {
        return response;
      })
    );
  }

  public sendLink(sendLinkInfo: ISendLink) {
    return this.addWithUrl<ISendLink, ISendLink>('share', sendLinkInfo);
  }
}
