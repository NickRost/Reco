import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Comment } from '../models/comment/comment';
import { NewComment } from '../models/comment/new-comment';
import { ResourceService } from './resource.service';

@Injectable({ providedIn: 'root' })
export class CommentService extends ResourceService<Comment> {
  getResourceUrl(): string {
    return '/comment';
  }

  constructor(override httpClient: HttpClient, private router: Router) {
    super(httpClient);
  }

  public createComment(comment: NewComment) {
    return this.add(comment);
  }

  public editComment(comment: Comment) {
    return this.update(comment);
  }

  public deleteComment(commentId: number) {
    return this.delete(commentId);
  }

  public getAllComments() {
    return this.getFullRequest(this.getResourceUrl());
  }
}
