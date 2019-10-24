import { Component, OnInit } from '@angular/core';
import { finalize } from 'rxjs/operators';
import { IStat } from '../models/stat.model';
import { StatProviderService } from '../services/stat-provider.service';
import { FilterTypes } from '../models/filter-types.enum';

@Component({
  selector: 'app-stat',
  templateUrl: './stat.component.html',
  styleUrls: ['./stat.component.css']
})
export class StatComponent implements OnInit {

  username: string;
  loadedStats: IStat[];
  error: string;
  loading: boolean;

  filters = [
    {
      type: FilterTypes.Battles,
      display: 'Battles'
    },
    {
      type: FilterTypes.Name,
      display: 'Name'
    },
    {
      type: FilterTypes.WinRate,
      display: 'Win Rate'
    },
  ]

  constructor(
    private statsProvider: StatProviderService
  ) { }

  ngOnInit() {
  }

  onSubmit() {
    this.error = '';
    this.loading = true;
    this.loadedStats = [];
    
    this.statsProvider.getStats(this.username)
      .pipe(
        finalize(() => this.loading = false)
      )
      .subscribe(
        res => this.loadedStats = res,
        err => this.error = err.error,
      )
  }

  onClear() {
    this.error = '';
    this.loading = false;
    this.loadedStats = [];
  }

  sort(type) {
    if(this.loadedStats)
    {
      switch (+type)
      {
        case FilterTypes.Name:
          {
            this.loadedStats.sort((a, b) => a.name >= b.name ? 1 : -1);
            break;
          }
        case FilterTypes.Battles:
          {
            this.loadedStats.sort((a, b) => b.battleCount - a.battleCount);
            break;
          }
        case FilterTypes.WinRate:
          {
            this.loadedStats.sort((a, b) => b.winRatio - a.winRatio);
            break;
          }
      }
    }
  }

}
