import { Component, OnInit, Input } from '@angular/core';

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
                backgroundColor: 'rgba(51, 255, 51, 0.2)',
                data: winsLeft,
                pointRadius: 6,
                pointHoverRadius: 4,
              }
            ]
          },
          options: {
            tooltips: {
              footerFontSize: 11,
              footerFontStyle: '',
              titleFontColor: 'rgba(51, 255, 51, 1)',
              borderWidth: 2,
              borderColor: 'rgba(51, 255, 51, 0.5)',
              cornerRadius: 5,
              callbacks: {
                title: function(tooltipItem) {
                  let item = tooltipItem[0];
                  let title = `WinRatio: ${item.label}\nWins left: ${item.value}`
                  return title;
                },
                label: function() {
                  return '';
                },
                footer: function(tooltipItem) {
                  let item = tooltipItem[0];
                  let text = `Number of wins to get to the desired\n` + 
                    `percent with ${item.label} WinRatio is ${item.value}`
                  return text;
                }
              }
            },
            responsive: true,
            maintainAspectRatio: false,
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
                ticks: {
                  beginAtZero: true,
                },
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
