import { Component, OnInit } from '@angular/core';
import { ApiService } from '../services/api.service';
import { Auction } from '../models/models';
import { SignalRService } from '../services/signalr.service';

@Component({
  selector: 'app-auction-container',
  templateUrl: './auction-container.component.html',
  styleUrls: ['./auction-container.component.css'],
})
export class AuctionContainerComponent implements OnInit {
  public auctions: Auction[] = [];
  public bidAmount: number = 0;
  constructor(private apiservice: ApiService, private signalRService: SignalRService) {
    this.apiservice.getAuctions().subscribe(result => {
      this.auctions = result;
      this.auctions.forEach(auction => {
        // set the ended and started properties
        let auctionStartDate = new Date(auction.auction_start_date);
        let auctionEndDate = new Date(auctionStartDate.getTime() + 2 * 60 * 60 * 1000);
        auction.endded = new Date() > auctionEndDate;
        auction.started = new Date() > auctionStartDate;
      });
      for (let auction of this.auctions) {
        // get prices for each auction
        this.apiservice.getAuctionPrice(auction.auction_id).subscribe(
          result => {
            auction.price = result.price;
          },
          err => console.error(err)
        );
      }
    }, err => console.error(err));
  }

  ngOnInit() {
    this.signalRService.messages$.subscribe(message => {
      let modAuction = this.auctions.find(auction => auction.auction_id === message.auctionId);
      if (modAuction) {
        modAuction.price = message.newPrice;
        /*this.apiservice.getUserById(message.userId).subscribe(user => {
          if(modAuction)
            modAuction.lastBidUserName = user.user_name
        }, err => console.error(err))*/
      }
    });
  }

  submitBid(auctionId: number, bidAmount: number) {
    const user = localStorage.getItem('user');
    if (user) {
      this.apiservice.sendBid(auctionId, bidAmount).subscribe(res => console.log(res), err => console.error(err));
    } else console.log("No user logged");
  }

}
