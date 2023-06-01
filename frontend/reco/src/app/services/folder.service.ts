import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { ResourceService } from './resource.service';
import { FolderDto } from '../models/folder/folder-dto';
import { NewFolderDto } from '../models/folder/new-folder-dto';

@Injectable({
  providedIn: 'root',
})
export class FolderService extends ResourceService<NewFolderDto> {
  private folder :FolderDto = {} as FolderDto;

  constructor(override httpClient: HttpClient) {
    super(httpClient);
  }

  getResourceUrl(): string {
    return '/folders'
  };

  public create(folder : NewFolderDto) {
    return this.handleResponse(this.add<NewFolderDto,FolderDto>(folder));
  }

  public updateFolder(folder: FolderDto) {
    return this.handleResponse(this.update<FolderDto, FolderDto>(folder));
  }

  public deleteFolder(id: number) {
    return this.delete(id);
  }

  public getAllFoldersByUserId(id: number) {
    return this.getFullRequest<FolderDto[]>(`folders/${id}`).pipe(
      map((resp) => {
        return resp.body as unknown as FolderDto[];
      })
    );
  }

  private handleResponse(observable: Observable<HttpResponse<FolderDto>>) {
    return observable.pipe(
        map((resp) => {
            this.folder = resp.body as FolderDto;
            return this.folder;
        })
    );
  }
}
