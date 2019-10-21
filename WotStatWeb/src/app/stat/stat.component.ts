import { Component, OnInit } from '@angular/core';
import { IStat } from '../models/stat.model';
import { StatProviderService } from '../services/stat-provider.service';

@Component({
  selector: 'app-stat',
  templateUrl: './stat.component.html',
  styleUrls: ['./stat.component.css']
})
export class StatComponent implements OnInit {

  username: string;
  loadedStats: IStat[];

  constructor(
    private statsProvider: StatProviderService
  ) { }

  ngOnInit() {
  }

  onSubmit() {
    this.statsProvider.getStats(this.username)
      .subscribe((stats) => this.loadedStats = stats);
  }

}
