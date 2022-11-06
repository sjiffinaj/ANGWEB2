import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './dashboard/dashboard.component';
import { MainComponent } from './main.component';
import { ProductComponent } from './product/product.component';
import { StockComponent } from './stock/stock.component';
import { UsersComponent } from './users/users.component';

const routes: Routes = [
  {
    path:'', 
    component:MainComponent,
    children:[
      {
        path:'dashboard', 
        component:DashboardComponent
      },
      {
        path:'product', 
        component:ProductComponent
      },
      {
        path:'stock', 
        component:StockComponent
      },
      {
        path:'user', 
        component:UsersComponent
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MainRoutingModule { }
