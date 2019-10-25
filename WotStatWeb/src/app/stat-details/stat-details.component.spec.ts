import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientModule } from '@angular/common/http';

import { StatDetailsComponent } from './stat-details.component';
import { BadgePipe } from '../pipes/badge.pipe';
import { IStat } from '../models/stat.model';

describe('StatDetailsComponent', () => {
  let component: StatDetailsComponent;
  let fixture: ComponentFixture<StatDetailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [
        StatDetailsComponent,
        BadgePipe,
      ],
      imports: [
        HttpClientModule,
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StatDetailsComponent);
    component = fixture.componentInstance;
    component.stat = { name: "T1", winRatio: 50, battleCount: 100, winCount: 50, winsToDesiredPercent: 0, badge: 1 };
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should generate correct graph element id', () => {
    let res = component.getGraphId('example  with spaces_-and!spec signs   ');

    expect(res).toEqual('graph-example__with_spaces_-and!spec_signs___');
  });
});
