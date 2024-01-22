import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { City } from '../models/city';

@Injectable({
  providedIn: 'root'
})
export class CityService {
  private apiUrl = '/api/cities'; // This URL will work with the Angular proxy to your backend

  constructor(private http: HttpClient) { }

  // Get all cities from the API
  getCities(): Observable<City[]> {
    return this.http.get<City[]>(this.apiUrl);
  }

  // Get a single city by its ID
  getCityById(id: number): Observable<City> {
    return this.http.get<City>(`${this.apiUrl}/${id}`);
  }

  // Add other methods for POST, PUT, DELETE as needed for your API
  postCity(city: City): Observable<City> {
    return this.http.post<City>(this.apiUrl, city);
  }
}
