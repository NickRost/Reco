import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AccessForRegisteredUsers } from '../models/access/access-for-registered-users';
import { ResourceService } from './resource.service';

@Injectable({
  providedIn: 'root',
})
export class AccessForLinkService extends ResourceService<AccessForRegisteredUsers> {
  private isAccessed: boolean = false;
  getResourceUrl(): string {
    return '/access';
  }
  constructor(override httpClient: HttpClient) {
    super(httpClient);
  }

  public CheckAccessedUser(videoId: number, userId: number) {
    return this.getFullRequestWithParams<boolean>('access/check', {
      videoId: videoId,
      userId: userId,
    });
  }
}
