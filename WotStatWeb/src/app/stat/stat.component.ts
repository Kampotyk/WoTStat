import { Component, OnInit } from '@angular/core';
import { finalize } from 'rxjs/operators';
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
  error: string;
  loading: boolean;

  constructor(
    private statsProvider: StatProviderService
  ) { }

  ngOnInit() {
  }

  onSubmit() {
    this.error = '';
    this.loading = true;
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

}
