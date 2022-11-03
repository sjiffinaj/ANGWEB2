import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private baseUrl:string="https://localhost:7233/api/"
  constructor(private http : HttpClient) { }

  login(loginObj:any){
    return this.http.post<any>(`${this.baseUrl}login`, loginObj);
  }
}
