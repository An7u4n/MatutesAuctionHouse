import { Component, Input, OnInit } from '@angular/core';
import { Auction, Item } from '../models/models';
import { ApiService } from '../services/api.service';

@Component({
  selector: 'app-auction',
  templateUrl: './auction.component.html',
  styleUrls: ['./auction.component.css'],
})
export class AuctionComponent implements OnInit {
  @Input() auction: any;
  public item: any;
  public started: boolean = false;
  constructor(private apiservice: ApiService) {

  }

  ngOnInit() {
    if (new Date(this.auction.auction_start_date) < new Date()) this.started = true;
    else this.started = false;

    this.apiservice.getItem(this.auction.item_id).subscribe(result => {
      this.item = result;
    }, err => console.error(err));

    this.apiservice.getAuctionPrice(this.auction.auction_id).subscribe(result => {
      this.auction.price = result.price;
    }, err => console.error(err));
  }
}
