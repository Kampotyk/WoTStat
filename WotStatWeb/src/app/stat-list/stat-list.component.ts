import { Component, OnInit, Input } from '@angular/core';

import { IStat } from '../models/stat.model';

@Component({
  selector: 'app-stat-list',
  templateUrl: './stat-list.component.html',
  styleUrls: ['./stat-list.component.css']
})
export class StatListComponent implements OnInit {

  @Input()
  statList: IStat[];

  constructor( ) { }

  ngOnInit() {
  }

}

