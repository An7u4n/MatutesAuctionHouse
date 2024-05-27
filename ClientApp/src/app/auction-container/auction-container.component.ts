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
  constructor(private apiservice: ApiService, private signalRService: SignalRService) {
    this.apiservice.getAuctions().subscribe(result => {
      this.auctions = result;
    }, err => console.error(err));
  }

  ngOnInit() {
    // Escuchar los mensajes entrantes
    this.signalRService.messages$.subscribe(message => {
      console.log('New bid:', message);
      let modAuction = this.auctions.find(auction => auction.auction_id === message.auctionId);
      if (modAuction) {
        modAuction.price = message.newPrice;
      }
    });
  }
}
