import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import 'rxjs/add/operator/map';
import { DroneAction } from '../model/actions/droneaction';

@Injectable()
export class TelloApiService {
  readonly apiPath = 'http://localhost:54635/api/tello/';
  constructor (
    private http: Http
  ) {}

  send(action:DroneAction){
    return this.http.get(`${this.apiPath}${action.command}`)
    .map((res:Response) => res.json());
  }

}