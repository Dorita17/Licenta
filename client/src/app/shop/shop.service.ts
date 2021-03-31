import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IMealType } from '../shared/models/mealType';
import { IPagination } from '../shared/models/pagination';
import { map } from 'rxjs/operators';
import { ShopParams } from '../shared/models/shopParams';
import { IMeal } from '../shared/models/meal';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  baseUrl = 'https://localhost:5001/api/';

  constructor(private http: HttpClient) { }

  getMeals(shopParams: ShopParams) {
    let params = new HttpParams();

    if (shopParams.typeId !== 0) {
      params = params.append('typeId', shopParams.typeId.toString());
    }

    if (shopParams.search) {
      params = params.append('search', shopParams.search);
    }

    params = params.append('sort', shopParams.sort);
    params = params.append('pageIndex', shopParams.pageNumber.toString());
    params = params.append('pageSize', shopParams.pageSize.toString());

    return this.http.get<IPagination>(this.baseUrl + 'meals', {observe: 'response', params})
      .pipe(
        map(response => {
          return response.body;
        })
      );
  }

  getTypes() {
    return this.http.get<IMealType[]>(this.baseUrl + 'meals/types');
  }

  getMeal(id: number) {
    return this.http.get<IMeal>(this.baseUrl + 'meals/' + id);
  }
}
