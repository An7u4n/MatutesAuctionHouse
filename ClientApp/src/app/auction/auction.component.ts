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
  constructor(private apiservice: ApiService) {

  }

  ngOnInit() {
    //this.apiservice.getItem(this.auction.item_id).subscribe(result => {
    //  this.item = result;
    //}, err => console.error(err));
  }
}
