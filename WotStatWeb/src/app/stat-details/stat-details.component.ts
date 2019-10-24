import { Component, OnInit, Input } from '@angular/core';

import { Chart } from 'chart.js';

import { IStat } from '../models/stat.model';
import { StatProviderService } from '../services/stat-provider.service';

@Component({
  selector: 'app-stat-details',
  templateUrl: './stat-details.component.html',
  styleUrls: ['./stat-details.component.css']
})
export class StatDetailsComponent implements OnInit {
  
  @Input()
  stat: IStat;
  
  chart = [];
  
  constructor(
    private statProvider: StatProviderService
  ) { }
    
  ngOnInit() {
    this.loadGraph()
  }
      
  loadGraph() {
    this.chart = [];
    const elementId = this.getGraphId(this.stat.name);

    this.loadGraphData(this.stat, elementId);
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

}
