import { Component, OnInit } from '@angular/core';
import {MatDialogModule,MatDialogRef} from '@angular/material/dialog'

import { UserTypeService } from '../../services/user-type.service';
import { UserService } from '../../services/user.service';
import { NotifyService } from '../../../shared/notify.service';
import { UserType } from 'src/app/models/user-type.model';


@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.scss']
})
export class UserComponent implements OnInit {

  public userTypes: UserType[]=[];
  constructor(public service: UserService,
    public userTypeService: UserTypeService,
    private notificationService: NotifyService,
    public dialogRef: MatDialogRef<UserComponent>) { }


  ngOnInit() {
    this.service.getUsers()
    .subscribe({
      next: (user) =>{
        console.log(user);
      },
      error: (msg)=> {
        console.log(msg);
      }
    });
  }

  onClear() {
    this.service.form.reset();
    this.service.initializeFormGroup();
    this.notificationService.success(':: Submitted successfully');
  }

  onSubmit() {
    if (this.service.form.valid) {
      if (!this.service.form.get('id')?.value)
        this.service.insertUser(this.service.form.value);
      else
      this.service.updateUser(this.service.form.value);
      this.service.form.reset();
      this.service.initializeFormGroup();
      this.notificationService.success(':: Submitted successfully');
      this.onClose();
    }
  }

  onClose() {
    this.service.form.reset();
    this.service.initializeFormGroup();
    this.dialogRef.close();
  }

}
