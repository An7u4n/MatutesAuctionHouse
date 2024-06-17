import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { User, Item, Auction } from '../models/models';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  url: string = 'https://localhost:7268/api/'
  userId: number;
  constructor(private _http: HttpClient) {
    const user = localStorage.getItem('user');
    if (user) {
      const userObject = JSON.parse(user);
      this.userId = userObject.user_id;
    } else this.userId = 0;
  }
  // User Methods

  getUserById(userId: number): Observable<User> {
    return this._http.get<User>(this.url + 'Users/' + userId);
  };

  // Items Methods
  getItem(item_id: number): Observable<Item> {
    return this._http.get<any>(this.url + 'Items/' + item_id);
  }

  getItems(): Observable<Item[]> {
    return this._http.get<Item[]>(this.url + 'Items');
  }

  getItemImage(item_id: number): Observable<any> {
    return this._http.get(this.url + 'Items/' + item_id + '/image', { responseType: 'blob' });
  }

  getItemsByOwner(): Observable<Item[]> {
    return this._http.get<Item[]>(this.url + 'Items/notsold/' + this.userId);
  }

  //postItem(item_name: string, item_description: string, itemPhoto: any): Observable<any> {  }

  deleteItem(item_id: number) {
    return this._http.delete<any>(this.url + 'Items/' + item_id);
  }


  // Auction Methods
  getAuction(auction_id: number) {
    return this._http.get<any>(this.url + 'Auctions/' + auction_id);
  }

  getAuctions(): Observable<Auction[]> {
    return this._http.get<Auction[]>(this.url + 'Auctions');
  }

  postAuction(item_id: number, auction_start_date: string): Observable<any> {
    return this._http.post<any>(this.url + 'Auctions', {
      item_id: item_id,
      auction_start_date: auction_start_date,
    })
  }

  deleteAuction(auction_id: number) {
    return this._http.delete<any>(this.url + 'Auctions/' + auction_id);
  }

  // AuctionPrice
  getAuctionPrice(auction_id: number) {
    return this._http.get<any>(this.url + 'AuctionPrices/' + auction_id);
  }

  sendBid(auction_id: number, price: number) {
    return this._http.post<any>(this.url + 'Auctions/AuctionPrice', { auction_id: auction_id, price: price, user_id: this.userId });
  }

}
