import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { User, Item, Auction } from '../models/models';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  url: string = 'https://localhost:7268/api/'

  constructor(private _http: HttpClient) { }

  // Items Methods
  getItem(item_id: number) {
    return this._http.get<any>(this.url + 'Items/' + item_id);
  }

  getItems(): Observable<Item[]> {
    return this._http.get<Item[]>(this.url + 'Items');
  }

  postItems(item_name: string, item_description: string, user_id: number): Observable<any> {
    return this._http.post<any>(this.url + 'Items', {
      item_name: item_name,
      item_description: item_description,
      user_id: user_id,
    })
  }

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

}
