import { Component } from '@angular/core';
import { ApiService } from '../services/api.service';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-add-auction',
  templateUrl: './add-auction.component.html',
  styleUrls: ['./add-auction.component.css']
})
export class AddAuctionComponent {
  items: any;
  userId: number;
  selectedFile: File | null = null;
  constructor(private apiservice: ApiService, private _http: HttpClient) {
    this.apiservice.getItemsByOwner().subscribe(res => {
      this.items = res;
      console.log(this.items);
    }, err => console.error(err));

    // Get the user id from the local storage
    const user = localStorage.getItem('user');
    if (user) {
      const userObject = JSON.parse(user);
      this.userId = userObject.user_id;
    } else this.userId = 0;
  }

  onFileSelected(event: Event) {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files.length > 0) {
      this.selectedFile = input.files[0];
    }
  }

  onSubmitItem(itemForm: any) {
    const formData = new FormData();

    formData.append('item_name', itemForm.value.itemName);
    formData.append('item_description', itemForm.value.itemDescription);
    formData.append('user_id', this.userId.toString());
    console.log(itemForm.value.itemName);

    if (this.selectedFile) {
      formData.append('image', this.selectedFile);
    }

    this._http.post<any>('https://localhost:7268/api/Items', formData, {
      headers: {
        'Accept': 'multipart/form-data'
      }
    }).subscribe(
      res => console.log(res),
      err => console.error(err)
    );
  }

  onSubmitAuction(auctionForm: any) {
    if (new Date((auctionForm.value.datePicker)) < new Date()) alert("Auction date must be in the future");
    else {
      this.apiservice.postAuction(auctionForm.value.selectedItem, auctionForm.value.datePicker).subscribe(res => { console.log(res) }, err => console.error(err));
    }
  }

}
