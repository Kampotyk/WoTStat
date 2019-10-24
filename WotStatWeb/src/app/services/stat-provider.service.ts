import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

import { environment } from '../../environments/environment';
import { IStat } from '../models/stat.model';
import { IEstimationGraphPoint } from '../models/estimation-graph-point.model';
import { Region } from '../models/region.model';

@Injectable({
  providedIn: 'root'
})
export class StatProviderService {
  private env = environment;
  private apiVer: string = "v1.0"
  private tankStatEndPoint: string = "stat/tanks";
  private graphDataEndpoint: string = "stat/estimation-graph";

  constructor(
    private http: HttpClient
  ) { }

  private baseApiUrl(endpoint?: string) {
    return `${this.env.apiUrl}/${this.apiVer}/${endpoint}`;
  }

  getStats(region: Region, username: string): Observable<IStat[]> {
    return this.http.post<IStat[]>(`${this.baseApiUrl(this.tankStatEndPoint)}/?userName=${username}`, region);
  }

  getEstimationGraphData(battlecount: number, winCount: number): Observable<IEstimationGraphPoint[]> {
    return this.http.get<IEstimationGraphPoint[]>(`${this.baseApiUrl(this.graphDataEndpoint)}/?battleCount=${battlecount}&winCount=${winCount}`);
  }
}
