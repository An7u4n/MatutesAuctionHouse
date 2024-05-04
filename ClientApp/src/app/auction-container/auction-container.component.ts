import { Component } from '@angular/core';
import { ApiService } from '../services/api.service';
import { Auction } from '../models/models';

@Component({
  selector: 'app-auction-container',
  templateUrl: './auction-container.component.html',
  styleUrls: ['./auction-container.component.css'],
})
export class AuctionContainerComponent {
  public auctions: Auction[] = [];
  //constructor(private apiservice: ApiService) {
  //  this.apiservice.getAuctions().subscribe(result => {
  //    this.auctions = result;
  //  }, err => console.error(err));
  //}
}
