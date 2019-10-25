import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule } from '@angular/forms';
import { MatExpansionModule } from '@angular/material/expansion';
import { HttpClientModule } from '@angular/common/http';

import { StatComponent } from './stat.component';
import { StatListComponent } from '../stat-list/stat-list.component';
import { BadgePipe } from '../pipes/badge.pipe';
import { StatDetailsComponent } from '../stat-details/stat-details.component';
import { IStat } from '../models/stat.model';
import { FilterTypes } from '../models/filter-types.enum';

describe('StatComponent', () => {
  let component: StatComponent;
  let fixture: ComponentFixture<StatComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [
        StatComponent,
        StatListComponent,
        StatDetailsComponent,
        BadgePipe,
      ],
      imports: [
        FormsModule,
        MatExpansionModule,
        HttpClientModule,
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StatComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should contain filters', () => {
    expect(component.filters.length > 0);
  });

  it('should contain available regions', () => {
    expect(component.regions.length > 0);
  });

  it('should clear data after onClear()', () => {
    component.error = 'some error';
    component.loading = true;
    component.loadedStats = [{name: "", badge: 1, winRatio: 50, battleCount: 2, winsToDesiredPercent: 0, winCount: 1}];
    component.selectedRegion = {name: "Europe", urlSuffix: "eu"};

    component.onClear();

    expect(component.error).toBe('');
    expect(component.loading).toBeFalsy();
    expect(component.loadedStats).toBeNull();
    expect(component.selectedRegion).toBeNull();
  });

  it('should sort by name', () => {
    let sortByName = FilterTypes.Name;
    component.loadedStats = dumbStats;

    component.sort(sortByName);

    expect(isAscending(component.loadedStats.map(stat => stat.name))).toBeTruthy();
  });

  it('should sort by count of battles desc', () => {
    let sortByBattlecount = FilterTypes.Battles;
    component.loadedStats = dumbStats;

    component.sort(sortByBattlecount);

    expect(component.loadedStats.map(stat => stat.battleCount))
      .toEqual(component.loadedStats.map(stat => stat.battleCount).sort((a,b) => b - a));
  });

  it('should sort by count of winrate desc', () => {
    let sortByWinrate = FilterTypes.WinRate;
    component.loadedStats = dumbStats;

    component.sort(sortByWinrate);

    expect(component.loadedStats.map(stat => stat.winRatio))
      .toEqual(component.loadedStats.map(stat => stat.winRatio).sort((a,b) => b - a));
  });
});

function isAscending(array: Array<string>): boolean { 
  let isAscending = false;
  let prev = array[0];
  for (let i = 0; i < array.length; i++) {
      if (i != array.length - 1) {
          if (prev < array[i + 1]) {
              isAscending = true;
          }
          else {
              return false;
          }
      }
  }
  if (isAscending) {
      return true;
  }
}

const dumbStats: IStat[] = [
  {
    name: "A", badge: 1, winRatio: 50, battleCount: 2, winsToDesiredPercent: 0, winCount: 1
  },
  {
    name: "B", badge: 2, winRatio: 75, battleCount: 100, winsToDesiredPercent: 0, winCount: 75
  },
  {
    name: "C", badge: 3, winRatio: 100, battleCount: 1, winsToDesiredPercent: 0, winCount: 1
  },
  {
    name: "D", badge: 4, winRatio: 0, battleCount: 3, winsToDesiredPercent: 3, winCount: 0
  },
]