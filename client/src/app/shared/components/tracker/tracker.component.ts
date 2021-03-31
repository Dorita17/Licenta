import { Component, OnDestroy, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { AccountService } from 'src/app/account/account.service';
import { IUser } from '../../models/user';

@Component({
  selector: 'app-tracker',
  templateUrl: './tracker.component.html',
  styleUrls: ['./tracker.component.scss']
})
export class TrackerComponent implements OnInit{
  user: IUser;
  negativeCalories = false;
  negativeProteins = false;
  negativeCarbohydrates = false;
  negativeFats = false;

  constructor(private accountService: AccountService) { }

  ngOnInit(): void {
    this.accountService.currentUser$.subscribe((user: IUser) => {
      this.user = user;
      if (this.user.dailyCalories <= 0) {
        this.negativeCalories = true;
      }
      if (this.user.dailyProteins <= 0) {
        this.negativeProteins = true;
      }
      if (this.user.dailyCarbohydrates <= 0) {
        this.negativeCarbohydrates = true;
      }
      if (this.user.dailyFats <= 0) {
        this.negativeFats = true;
      }
    }, error => {
      console.log(error);
    });
  }
}
