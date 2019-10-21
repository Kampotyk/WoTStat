import { Injectable } from '@angular/core';
import { of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MockStatProviderService {

  constructor() { }

  getStats(username: string) {
    return of([
      {
        "name": "M40/M43",
        "battleCount": 901,
        "winCount": 436,
        "winRatio": 48.39,
        "winsToDesiredPercent": 29,
        "badge": 4
      },
      {
        "name": "T67",
        "battleCount": 765,
        "winCount": 371,
        "winRatio": 48.5,
        "winsToDesiredPercent": 23,
        "badge": 4
      },
      {
        "name": "B-C 25 t",
        "battleCount": 735,
        "winCount": 335,
        "winRatio": 45.58,
        "winsToDesiredPercent": 65,
        "badge": 4
      },
    ])
  }
}
