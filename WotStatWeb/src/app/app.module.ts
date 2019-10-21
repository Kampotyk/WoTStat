import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule } from '@angular/forms';
import { MatExpansionModule } from '@angular/material/expansion';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { StatComponent } from './stat/stat.component';
import { AppRoutingmodule } from './app-routing.module';
import { AboutComponent } from './about/about.component';
import { HomeComponent } from './home/home.component';
import { StatListComponent } from './stat-list/stat-list.component';
import { BadgePipe } from './pipes/badge.pipe';

@NgModule({
  declarations: [
    AppComponent,
    StatComponent,
    AboutComponent,
    HomeComponent,
    StatListComponent,
    BadgePipe,
  ],
  imports: [
    BrowserModule,
    AppRoutingmodule,
    BrowserAnimationsModule,
    FormsModule,
    MatExpansionModule,
    HttpClientModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
