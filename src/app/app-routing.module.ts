import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CitiesComponent } from './pages/cities/cities.component';
import { LoginComponent } from './pages/login/login.component';
import { CityDetailComponent } from './pages/city-detail/city-detail.component';
import { PointsOfInterestComponent } from './pages/points-of-interest/points-of-interest.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'cities', component: CitiesComponent },
  { path: 'cities/:id', component: CityDetailComponent },
  { path: 'cities/:cityId/points-of-interest', component: PointsOfInterestComponent },
  { path: '', redirectTo: '/home', pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
