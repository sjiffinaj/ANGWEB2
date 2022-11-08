import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { UserType } from 'src/app/models/user-type.model';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserTypeService {

  baseApiUrl:string = environment.baseApiUrl;
  public userTypes: UserType[] = [];
  array = [];

  constructor(private http: HttpClient) {
    this.getUsers().subscribe({
      next:(user=>{
        console.log(user);
        //  user.map(item => {
        //   return {
        //     $key: item.$key,
        //     // : item.code,
        //     // $key: item.$key,
        //     // ...item.payload.val()
        //   };
        // });
      }),
      error:(response)=>{
        console.log(response);
      }
    }) 
   }


   getUsers() : Observable<UserType[]> {
    return this.http.get<UserType[]>(this.baseApiUrl + 'usertype');
  }

   getDepartmentName($key:string) {
    // if ($key == "0")
      return "";
    // else{
    //   return _.find(this.array, (obj) => { return obj.$key == $key; })['name'];
    }
  }

