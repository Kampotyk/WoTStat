import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IStat } from '../models/stat.model';
import { Observable, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class StatProviderService {

  apiUrl: string = "http://localhost:5000/api/stat/tanks";

  constructor(
    private http: HttpClient
  ) { }

  getStats(username: string): Observable<IStat[]> {
    return this.http.get<IStat[]>(`${this.apiUrl}?userName=${username}`);
  }
}
