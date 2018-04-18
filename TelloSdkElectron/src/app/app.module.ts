import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { HttpModule } from '@angular/http';
import { NgModule } from '@angular/core';
import { ActionService } from './tellosdk/services/action.service';
import { TelloApiService } from './tellosdk/services/telloapi.service';

import { AppComponent } from './app.component';


@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    HttpModule
  ],
  providers: [
    TelloApiService,
    ActionService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
