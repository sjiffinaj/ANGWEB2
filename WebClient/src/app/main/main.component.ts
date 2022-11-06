import { Component, OnInit } from '@angular/core';
import {Router} from '@angular/router'

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss']
})
export class MainComponent implements OnInit {

  opened=false;
  constructor(private router:Router) { }

  ngOnInit(): void {
  }

  // onClick(id:any) : void
  // {
  //   switch(id)
  //   {
  //     case 1:{
  //       this.router.navigate(['product']);
  //       break;
  //       }
  //     default:{
  //       this.router.navigate(['product']);
  //     }
  //   }
  // }
}
