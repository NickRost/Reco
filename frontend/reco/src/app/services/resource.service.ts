import { Injectable } from '@angular/core';
import {
  HttpClient,
  HttpErrorResponse,
  HttpHeaders,
  HttpParams,
  HttpResponse,
} from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export abstract class ResourceService<T> {
  private readonly APIUrl = environment.apiUrl + this.getResourceUrl();
  public headers = new HttpHeaders();

  constructor(protected httpClient: HttpClient) {}

  abstract getResourceUrl(): string;

  getListPagination(page: number, count: number): Observable<T[]> {
    let params = new HttpParams()
      .set('page', page.toString())
      .set('count', count.toString());

    return this.httpClient
      .get<T[]>(`${this.APIUrl}}?${params.toString()}`)
      .pipe(catchError(this.handleError));
  }

  getList(): Observable<HttpResponse<T[]>> {
    return this.httpClient
      .get<T[]>(`${this.APIUrl}`, { observe: 'response' })
      .pipe(catchError(this.handleError));
  }

  get(id: string | number): Observable<HttpResponse<T>> {
    return this.httpClient
      .get<T>(`${this.APIUrl}/${id}`, { observe: 'response' })
      .pipe(catchError(this.handleError));
  }

  add<TRequest, TResponse>(
    resource: TRequest
  ): Observable<HttpResponse<TResponse>> {
    return this.httpClient
      .post<TResponse>(`${this.APIUrl}`, resource, { observe: 'response' })
      .pipe(catchError(this.handleError));
  }

  delete(id: string | number): Observable<HttpResponse<T>> {
    return this.httpClient
      .delete<T>(`${this.APIUrl}/${id}`, { observe: 'response' })
      .pipe(catchError(this.handleError));
  }

  public deleteWithParams<TRequest>(
    url: string,
    params?:
      | HttpParams
      | {
          [param: string]:
            | string
            | number
            | boolean
            | ReadonlyArray<string | number | boolean>;
        }
  ): Observable<HttpResponse<TRequest>> {
    return this.httpClient.delete<TRequest>(`${environment.apiUrl}/${url}`, {
      observe: 'response',
      headers: this.getHeaders(),
      params: params,
    });
  }

  getWithUrl(id: string | number, subUrl: string): Observable<HttpResponse<T>> {
    return this.httpClient
      .get<T>(`${this.APIUrl}/${subUrl}/${id}`, { observe: 'response' })
      .pipe(catchError(this.handleError));
  }

  update<TRequest, TResponse>(
    resource: TRequest
  ): Observable<HttpResponse<TResponse>> {
    return this.httpClient
      .put<TResponse>(`${this.APIUrl}`, resource, { observe: 'response' })
      .pipe(catchError(this.handleError));
  }

  addWithUrl<TRequest, TResponse>(
    subUrl: string,
    resource: TRequest
  ): Observable<HttpResponse<TResponse>> {
    return this.httpClient
      .post<TResponse>(`${this.APIUrl}/${subUrl}`, resource, {
        observe: 'response',
      })

      .pipe(catchError(this.handleError));
  }

  private handleError(error: HttpErrorResponse) {
    return throwError(() => error);
  }

  public getFullRequest<TRequest>(
    url: string,
    httpParams?: HttpParams
  ): Observable<HttpResponse<TRequest>> {
    return this.httpClient.get<TRequest>(`${environment.apiUrl}/${url}`, {
      observe: 'response',
      headers: this.getHeaders(),
      params: httpParams,
    });
  }

  public getFullRequestWithParams<TRequest>(
    url: string,
    params?:
      | HttpParams
      | {
          [param: string]:
            | string
            | number
            | boolean
            | ReadonlyArray<string | number | boolean>;
        }
  ): Observable<HttpResponse<TRequest>> {
    return this.httpClient.get<TRequest>(`${environment.apiUrl}/${url}`, {
      observe: 'response',
      headers: this.getHeaders(),
      params: params,
    });
  }

  private getHeaders() {
    return this.headers;
  }
}