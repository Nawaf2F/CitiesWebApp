import { Component, OnInit } from '@angular/core';
import { CityService } from '../../services/city.service';
import { City } from '../../models/city';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-city-detail',
  templateUrl: './city-detail.component.html',
  styleUrls: ['./city-detail.component.css']
})
export class CityDetailComponent implements OnInit{
   
    //city!: City; // Initialize the cities property
    //Id!: number;
    city: any;
    includePOI: boolean = true;
    constructor(private cityService: CityService, private route:ActivatedRoute) {}
  
    ngOnInit(): void {
     
      this.route.params.subscribe(params => {
        const id = params['id'];
        this.getCityDetails(+id, this.includePOI);
      });


    }
getCityDetails(id: number, includePOI: boolean = true){
  let cityData = this.cityService.getCityById(id, this.includePOI);
      
      cityData.subscribe({
        next: (data) => {
        this.city = data
        console.log(data)},
        error: (err) => {console.error('There was an error!', err)}
    });
  }
}