import { Component, OnInit, Input, ViewChild, ElementRef } from '@angular/core';

import { Chart } from 'chart.js';

import { IStat } from '../models/stat.model';
import { StatProviderService } from '../services/stat-provider.service';

@Component({
  selector: 'app-stat-list',
  templateUrl: './stat-list.component.html',
  styleUrls: ['./stat-list.component.css']
})
export class StatListComponent implements OnInit {

  @Input()
  statList: IStat[];
  chart = [];

  constructor(
    private statProvider: StatProviderService
  ) { }

  onStatOpened(statTankName: string) {
    this.chart = [];
    const elementId = this.getGraphId(statTankName);

    // 0 because we are assuming that we can only have one stat per tank
    const stat = this.statList.filter(s => s.name == statTankName)[0];
    this.loadGraphData(stat, elementId);
  }

  getGraphId(statTankName: string): string {
    return `graph-${statTankName.split(' ').join('_')}`;
  }

  loadGraphData(stat: IStat, elementId: string) {
    this.statProvider.getEstimationGraphData(stat.battleCount, stat.winCount)
      .subscribe(data => {
        let winRatios = data.map(val => val.winRatio);
        let winsLeft = data.map(val => val.winsLeft);

        this.chart = new Chart(elementId, {
          type: 'line',
          data: {
            labels: winRatios,
            datasets: [
              {
                label: 'Wins to get to desired percent',
                backgroundColor: 'rgba(179, 255, 217, 0.2)',
                data: winsLeft
              }
            ]
          },
          options: {
            responsive: true,
            scales: {
              xAxes: [{
                display: true,
                scaleLabel: {
                  display: true,
                  labelString: 'Win Ratio'
                }
              }],
              yAxes: [{
                display: true,
                scaleLabel: {
                  display: true,
                  labelString: 'Wins Left'
                }
              }]
            }
          }
        });        
      });
  }

  ngOnInit() {
  }

}
