import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AccessForUnregisteredUsers } from '../models/access/access-for-unregistered-users';
import { ResourceService } from './resource.service';

@Injectable({
  providedIn: 'root',
})
export class AccessForUnregisteredUsersService extends ResourceService<AccessForUnregisteredUsers> {
  private isAccessed: boolean = false;
  getResourceUrl(): string {
    return '/access';
  }
  constructor(override httpClient: HttpClient) {
    super(httpClient);
  }

  public addNewAccess(email: string, videoId: number) {
    const accessUnregisteredUser = {
      videoId: videoId,
      email: email,
    } as AccessForUnregisteredUsers;
    this.update(accessUnregisteredUser).subscribe();
  }
}
