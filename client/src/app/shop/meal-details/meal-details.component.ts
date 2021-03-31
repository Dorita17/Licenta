import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BasketService } from 'src/app/basket/basket.service';
import { IMeal } from 'src/app/shared/models/meal';
import { ShopService } from '../shop.service';

@Component({
  selector: 'app-meal-details',
  templateUrl: './meal-details.component.html',
  styleUrls: ['./meal-details.component.scss']
})
export class MealDetailsComponent implements OnInit {
  meal: IMeal;
  quantity = 1;

  constructor(private shopService: ShopService, private route: ActivatedRoute, private basketSerivce: BasketService) { }

  ngOnInit(): void {
    this.loadMeal();
  }
  
  loadMeal() {
    this.shopService.getMeal(+this.route.snapshot.paramMap.get('id')).subscribe(meal => {
      this.meal = meal;
    }, error => {
      console.log(error);
    });
  }

  addItemToBasket() {
    this.basketSerivce.addItemtoBasket(this.meal, this.quantity);
  }

  incrementQuantity() {
    this.quantity++;
  }

  decrementQuantity() {
    if (this.quantity > 1) {
      this.quantity--;
    }
  }

}
