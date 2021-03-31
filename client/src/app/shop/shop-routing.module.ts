import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { MealDetailsComponent } from './meal-details/meal-details.component';
import { ShopComponent } from './shop.component';

const routes: Routes = [
  {path: '', component: ShopComponent},
  {path: ':id', component: MealDetailsComponent},
]

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule]
})
export class ShopRoutingModule { }
