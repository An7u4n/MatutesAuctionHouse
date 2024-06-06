import { Component } from '@angular/core';
import { ApiService } from '../services/api.service';



@Component({
  selector: 'app-add-auction',
  templateUrl: './add-auction.component.html',
  styleUrls: ['./add-auction.component.css']
})
export class AddAuctionComponent {
  items: any;
  constructor(private apiservice: ApiService) {
    this.apiservice.getItemsByOwner().subscribe(res => {
      this.items = res;
      console.log(this.items);
    }, err => console.error(err));
  }

  onSubmitItem(itemForm: any) {
    this.apiservice.postItem(itemForm.value.itemName, itemForm.value.itemDescription).subscribe(res => { console.log(res) }, err => console.error(err));
  }

  onSubmitAuction(auctionForm: any) {
    if (new Date((auctionForm.value.datePicker)) < new Date()) alert("Auction date must be in the future");
    else {
      this.apiservice.postAuction(auctionForm.value.selectedItem, auctionForm.value.datePicker).subscribe(res => { console.log(res) }, err => console.error(err));
    }
  }

}
