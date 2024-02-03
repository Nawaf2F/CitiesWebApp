import { Component, OnInit } from '@angular/core';
import { CityService } from '../../services/city.service';
import { City } from '../../models/city';

@Component({
  selector: 'app-cities',
  templateUrl: './cities.component.html',
  styleUrls: ['./cities.component.css']
})
export class CitiesComponent implements OnInit {
  cities: City[] = []; // Initialize the cities property

  constructor(private cityService: CityService) {}

  ngOnInit(): void {
    let cityData = this.cityService.getCities();
    
    cityData.subscribe({
      next:(data: City[]) => 
      {
        this.cities = data
        console.log(data);
      },
      error: (error: any) => console.error('There was an error!', error)
  });
  }
}


