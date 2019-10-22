import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IStat } from '../models/stat.model';
import { Observable, of } from 'rxjs';
import { IEstimationGraphPoint } from '../models/estimation-graph-point.model';

@Injectable({
  providedIn: 'root'
})
export class StatProviderService {

  private apiUrl: string = "http://localhost:5000/api";
  private apiVer: string = "v1.0"
  private tankStatEndPoint: string = "stat/tanks";
  private graphDataEndpoint: string = "stat/estimation-graph";

  constructor(
    private http: HttpClient
  ) { }

  private baseApiUrl(endpoint?: string) {
    return `${this.apiUrl}/${this.apiVer}/${endpoint}`;
  }

  getStats(username: string): Observable<IStat[]> {
    return this.http.get<IStat[]>(`${this.baseApiUrl(this.tankStatEndPoint)}/?userName=${username}`);
  }

  getEstimationGraphData(battlecount: number, winCount: number): Observable<IEstimationGraphPoint[]> {
    return this.http.get<IEstimationGraphPoint[]>(`${this.baseApiUrl(this.graphDataEndpoint)}/?battleCount=${battlecount}&winCount=${winCount}`);
  }
}
