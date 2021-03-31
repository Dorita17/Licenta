import {v4 as uuidv4} from 'uuid';

export interface IBasket {
    id: string;
    items: IBasketItem[];
    clientSecret?: string;
    paymentIntentId?: string;
    deliveryMethodId?: number;
    deliveryPrice?: number;
}

export interface IBasketItem {
    id: number;
    mealName: string;
    price: number;
    quantity: number;
    grams: number;
    calories: number;
    proteins: number;
    carbohydrates: number;
    fats: number;
    pictureUrl: string;
    type:string;
}

export class Basket implements IBasket {
    id = uuidv4();
    items: IBasketItem[] = [];

}

export interface IBasketTotals {
    delivery: number;
    subtotal: number;
    total: number;
}