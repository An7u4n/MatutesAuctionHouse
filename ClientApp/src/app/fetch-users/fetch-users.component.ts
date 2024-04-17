import { Component } from '@angular/core';
import { Users, Items, Auctions } from '../models/models';
import { ApiService } from '../services/api.service';
@Component({
  selector: 'app-fetch-users',
  templateUrl: './fetch-users.component.html',
  styleUrls: ['../../styles.css'],
})
export class FetchUsersComponent {
  public users: Users[] = [];
  public items: Items[] = [];
  public auctions: Auctions[] = [];
  public item_user_name: any;
  public auction_item_id: any;

  constructor(private apiservice: ApiService) {
    this.apiservice.getUsers().subscribe(result => {
      this.users = result;
    }, err => console.error(err));

    this.apiservice.getItems().subscribe(result => {
      this.items = result;
    }, err => console.error(err));
    this.apiservice.getAuctions().subscribe(result => {
      this.auctions = result;
    }, err => console.error(err));
  }

  postUser(userForm: any) {
    this.apiservice.postUser(userForm.value.user_name).subscribe(res => {
      alert("Usuario Creado");
    }, err => console.error(err));
  }

  deleteUser(user_id: number) {
    this.apiservice.deleteUser(user_id).subscribe(res => {
      alert("Usuario Eliminado");
    }, err => console.error(err));
  }

  postItems(itemsForm: any) {
    this.apiservice.postItems(
      itemsForm.value.item_name,
      itemsForm.value.item_description,
      this.item_user_name,
    ).subscribe(res => {
      alert("Item Agregado");
    }, err => console.error(err));
  }

  deleteItem(item_id: number) {
    this.apiservice.deleteItem(item_id).subscribe(res => {
      alert("Item Deleted");
    }, err => console.error(err));
  }

  postAuction(auctionsForm: any) {
    this.apiservice.postAuction(
    auctionsForm.value.auction_item_id,
      (auctionsForm.value.date + "T" + auctionsForm.value.time + ":00"))
    .subscribe(res => {
      alert("Auction Added");
    }, err => console.error(err));
  }

  deleteAuction(auction_id: number) {
    this.apiservice.deleteAuction(auction_id).subscribe(res => {
      alert("Auction Deleted");
    }, err => console.error(err));
  }

}
