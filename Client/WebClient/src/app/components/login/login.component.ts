import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  type :string = "password"
  istext :boolean = false
  eyeIcon :string = "fa-eye-slash"
  loginForm!:FormGroup; 


  constructor(private fb : FormBuilder) { }

  ngOnInit(): void {
    this.loginForm = this.fb.group({
      username:['', Validators.required],
      password:['', Validators.required]

    })
  }

  showPass(){
    this.istext = !this.istext;
    this.istext ? this.type="text" : this.type="password";
    this.istext ? this.eyeIcon="fa-eye-slash" : this.eyeIcon="fa-eye";

  }

  OnSubmit(){
    if(this.loginForm.valid){
      console.log(this.loginForm.value);
      console.log("Login is success");

    }else{
      this.validateAllFormFields(this.loginForm);
      alert("Invalid Form");
    }
  }

  private validateAllFormFields(formGroup : FormGroup){
    Object.keys(formGroup.controls).forEach(field =>{
      const control = formGroup.get(field);
      if(control instanceof FormControl)
      {
        control.markAsDirty({onlySelf:true});
      }else if(control instanceof FormGroup){
        this.validateAllFormFields(control);
      }

    });
  }
}
