import { TestBed, async } from '@angular/core/testing';
import { AppComponent } from './app.component';
import { AppRoutingmodule } from './app-routing.module';
import { StatComponent } from './stat/stat.component';
import { HomeComponent } from './home/home.component';
import { AboutComponent } from './about/about.component';
import { FooterComponent } from './footer/footer.component';
import { FormsModule } from '@angular/forms';
import { StatListComponent } from './stat-list/stat-list.component';
import { StatDetailsComponent } from './stat-details/stat-details.component';
import { MatExpansionModule } from '@angular/material/expansion';
import { BadgePipe } from './pipes/badge.pipe';

describe('AppComponent', () => {
  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [
        AppComponent,
        StatComponent,
        HomeComponent,
        AboutComponent,
        FooterComponent,
        StatListComponent,
        StatDetailsComponent,
        BadgePipe,
      ],
      imports: [
        AppRoutingmodule,
        FormsModule,
        MatExpansionModule,
      ]
    }).compileComponents();
  }));

  it('should create the app', () => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.debugElement.componentInstance;
    expect(app).toBeTruthy();
  });

  it(`should have as title 'WotStatWeb'`, () => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.debugElement.componentInstance;
    expect(app.title).toEqual('WotStatWeb');
  });
});
