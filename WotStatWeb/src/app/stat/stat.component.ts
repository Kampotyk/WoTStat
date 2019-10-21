import { Component, OnInit } from '@angular/core';
import { IStat } from '../models/stat.model';

@Component({
  selector: 'app-stat',
  templateUrl: './stat.component.html',
  styleUrls: ['./stat.component.css']
})
export class StatComponent implements OnInit {

  username: string;
  loadedStats: IStat[];

  constructor() { }

  ngOnInit() {    
    this.loadedStats = [
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
    ]
  }

  onSubmit() {
    this.loadedStats = [];
  }

}
