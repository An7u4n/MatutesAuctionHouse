import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-fetch-users',
  templateUrl: './fetch-users.component.html'
})
export class FetchUsersComponent {
  public users: Users[] = [];
  public items: Items[] = [];
  public auctions: Auctions[] = [];

  constructor(http: HttpClient) {
    http.get<Users[]>('https://localhost:7268/api/Users').subscribe(result => {
      this.users = result;
    }, err => console.error(err));
    /*http.get<Items[]>('localhost:7268/api/Users').subscribe(result => {
      this.items = result;
    }, err => console.error(err));
    http.get<Auctions[]>('localhost:7268/api/Users').subscribe(result => {
      this.auctions = result;
    }, err => console.error(err));*/
  }
}
interface Users {
  user_id: number;
  user_name: string;
}

interface Items {
  item_id: number;
  item_name: string;
  item_description: string;
  user_id: number;
}

interface Auctions{
  auction_id: number;
  item_id: number;
  auction_start_date: Date;
}
