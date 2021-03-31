import { IAddress } from './address';

export interface IOrderToCreate {
    basketId: string;
    deliveryMethodId: number;
    deliverToAddress: IAddress;
}

export interface IOrder {
  id: number;
  buyerEmail: string;
  orderDate: string;
  deliverToAddress: IAddress;
  deliveryMethod: string;
  deliveryPrice: number;
  orderItems: IOrderItem[];
  subtotal: number;
  total: number;
  status: string;
}

export interface IOrderItem {
  mealId: number;
  mealName: string;
  price: number;
  pictureUrl: string;
  quantity: number;
  grams: number;
  calories: number;
  proteins: number;
  carbohydrates: number;
  fats: number;
}
