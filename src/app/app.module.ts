import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http'; 
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './pages/login/login.component';
import { CitiesComponent } from './pages/cities/cities.component';
import { CityDetailComponent } from './pages/city-detail/city-detail.component';
import { PointsOfInterestComponent } from './pages/points-of-interest/points-of-interest.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    CitiesComponent,
    CityDetailComponent,
    PointsOfInterestComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
