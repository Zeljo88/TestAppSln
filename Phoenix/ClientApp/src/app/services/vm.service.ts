
import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { VM } from '../models/vm';
import { Template } from '../models/template';
import { catchError } from 'rxjs/operators';
import { ErrorService } from './error.service';
import { Observable } from 'rxjs';

@Injectable()
export class VMService {

  constructor(public http: HttpClient, public errorService: ErrorService, @Inject('BASE_URL') private baseUrl: string) {
  }

  public getAllVMs() {
    return this.http.get<VM[]>(this.baseUrl + 'vm').pipe(
      catchError(this.errorService.handleError));
  }

  public StartInstance(id) {
    return this.http.put<any>(this.baseUrl + 'vm/start/' + id, null).pipe(
      catchError(this.errorService.handleError));
  }

  public StopInstance(id) {
    return this.http.put<any>(this.baseUrl + 'vm/stop/' + id, null).pipe(
      catchError(this.errorService.handleError));
  }

  public CreateWindowsAD() {
    return this.http.post<any>(this.baseUrl + 'stack', null).pipe(
      catchError(this.errorService.handleError));
  }

  public GetTemplates() {
    return this.http.get<Template[]>(this.baseUrl + 's3/buckets/templates').pipe(
      catchError(this.errorService.handleError));
  }
}
