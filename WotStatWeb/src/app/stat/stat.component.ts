import { Component, OnInit } from '@angular/core';
import { IStat } from '../models/stat.model';
import { MockStatProviderService } from '../services/mock-stat-provider.service';

@Component({
  selector: 'app-stat',
  templateUrl: './stat.component.html',
  styleUrls: ['./stat.component.css']
})
export class StatComponent implements OnInit {

  username: string;
  loadedStats: IStat[];

  constructor(
    private statsProvider: MockStatProviderService
  ) { }

  ngOnInit() {
  }

  onSubmit() {
    this.statsProvider.getStats(this.username)
      .subscribe((stats) => this.loadedStats = stats);
  }

}
