import { Injectable } from '@angular/core';
import { FormGroup, FormControl, Validators } from "@angular/forms";
import * as _ from 'lodash';
// import { DatePipe } from '@angular/common';
import { HttpClient } from '@angular/common/http'
import { environment } from 'src/environments/environment';
import {User} from '../../models/user.model'
import { Observable } from 'rxjs';
import { min } from 'lodash';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  baseApiUrl:string = environment.baseApiUrl;
  constructor(private http: HttpClient) { }

  UserList: User[] = [];

  form: FormGroup = new FormGroup({
    $key: new FormControl(null),
    fullName: new FormControl('', Validators.required),
    email: new FormControl('', Validators.email),
    mobile: new FormControl('', [Validators.required, Validators.minLength(8)]),
    city: new FormControl(''),
    gender: new FormControl('1'),
    userType: new FormControl(0),
    hireDate: new FormControl(''),
    isPermanent: new FormControl(false)
  });

  initializeFormGroup() {
    this.form.setValue({
      $key: null,
      fullName: '',
      email: '',
      mobile: '',
      city: '',
      gender: '1',
      userType: 0,
      hireDate: '',
      isPermanent: false
    });
  }


  getUsers() : Observable<User[]> {
    return this.http.get<User[]>(this.baseApiUrl + 'user');
  }

  insertUser(user : User): Observable<User> {
    return this.http.post<User>(this.baseApiUrl + 'users', user);

    // return this.UserList.push({
    //   $key:user.$key,
    //   fullName: user.fullName,
    //   email: user.email,
    //   mobile: user.mobile,
    //   city: user.city,
    //   gender: user.gender,
    //   department: user.department,
    //   hireDate: user.hireDate == "" ? "" : this.datePipe.transform(user.hireDate, 'yyyy-MM-dd'),
    //   isPermanent: user.isPermanent
    // });
  }

  updateUser(user : User) : Observable<User> {
    return this.http.put<User>(this.baseApiUrl + 'users', user);

    // return this.UserList.update(user.$key,
    //   {
    //     fullName: user.fullName,
    //     email: user.email,
    //     mobile: user.mobile,
    //     city: user.city,
    //     gender: user.gender,
    //     department: user.department,
    //      hireDate: user.hireDate == "" ? "" : this.datePipe.transform(user.hireDate, 'yyyy-MM-dd'),
    //     isPermanent: user.isPermanent
    //   });
  }

  deleteUser(id: string) {
    return this.http.delete<User>(this.baseApiUrl + 'users/'+id);

    // this.UserList.remove($key);
  }

  populateForm(user : any) {
    // // this.form.setValue(_.omit(user,'departmentName'));
  }
}
