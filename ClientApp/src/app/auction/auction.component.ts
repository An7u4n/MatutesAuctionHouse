import { Component, Input, OnInit } from '@angular/core';
import { Auction, Item } from '../models/models';
import { DomSanitizer } from '@angular/platform-browser';
import { ApiService } from '../services/api.service';

@Component({
  selector: 'app-auction',
  templateUrl: './auction.component.html',
  styleUrls: ['./auction.component.css'],
})
export class AuctionComponent implements OnInit {
  @Input() auction: any;
  public item: any;
  public imageUrl: any;
  public started: boolean = false;
  public bidAmount: number = 0;
  constructor(private apiservice: ApiService, private sanitizer: DomSanitizer) {

  }

  ngOnInit() {
    if (new Date(this.auction.auction_start_date) < new Date()) this.started = true;
    else this.started = false;

    this.apiservice.getItem(this.auction.item_id).subscribe(result => {
      this.item = result;
      this.imageUrl = this.convertToImageUrl(this.item.itemImage);
    }, err => console.error(err));

    this.apiservice.getAuctionPrice(this.auction.auction_id).subscribe(result => {
      this.auction.price = result.price;
    }, err => console.error(err));
  }

  submitBid(auctionId: number, bidAmount: number) {
    const user = localStorage.getItem('user');
    if (user) {
      this.apiservice.sendBid(auctionId, bidAmount).subscribe(res => console.log(res), err => console.error(err));
    } else console.error("No user logged");
  }

  convertToImageUrl(image: any): any {
    this.apiservice.getItemImage(this.item.item_id).subscribe(blob => {
      let objectURL = URL.createObjectURL(blob);
      this.imageUrl = this.sanitizer.bypassSecurityTrustUrl(objectURL);
    });
  }
}
