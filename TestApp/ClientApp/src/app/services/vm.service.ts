
import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Ec2instance } from '../models/ec2instance';
import { catchError } from 'rxjs/operators';
import { ErrorService } from './error.service';
import { Observable } from 'rxjs';

@Injectable()
export class VMService {

  constructor(public http: HttpClient, public errorService: ErrorService, @Inject('BASE_URL') private baseUrl: string) {
  }

  public getAllVMs() {
    return this.http.get<Ec2instance[]>(this.baseUrl + 'aws/instances').pipe(
      catchError(this.errorService.handleError));
  }
/*
  public StartInstance(id) {
    return this.http.put<any>(this.baseUrl + 'aws/vm/start/' + id, null).pipe(
      catchError(this.errorService.handleError));
  }

  public StopInstance(id) {
    return this.http.put<any>(this.baseUrl + '/aws/vm/stop/' + id, null).pipe(
      catchError(this.errorService.handleError));
  }*/

  public CreateWindowsAD() {
    return this.http.post<any>(this.baseUrl + '/stack', null).pipe(
      catchError(this.errorService.handleError));
  }


}
