import { HttpClient, HttpParams, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RequestService {

  constructor(protected httpClient: HttpClient) { }

  public getFullRequest<TRequest>(
    url: string,
    httpParams?: HttpParams
  ): Observable<HttpResponse<TRequest>> {
    return this.httpClient.get<TRequest>(`${url}`, {
      observe: 'response',
      params: httpParams,
    });
  }

  public delete(url: string,
    httpParams?: HttpParams) {
    return this.httpClient.delete(`${url}`, {
      observe: 'response',
      params: httpParams,
    });
  }
}
