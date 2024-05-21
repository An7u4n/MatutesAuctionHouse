import { Component, OnInit } from '@angular/core';
import { Apollo, gql } from 'apollo-angular';
import { Observable, Subscription } from 'rxjs';

const PRICE_SUBSCRIPTION = gql`
  subscription OnPriceChange {
    onPriceChange {
      price
    }
  }
`;

const UPDATE_PRICE_MUTATION = gql`
  mutation UpdatePrice($newPrice: Int!) {
    updatePrice(newPrice: $newPrice)
  }
`;

@Component({
  selector: 'app-bid-demo',
  templateUrl: './bid-demo.component.html',
  styleUrls: ['../../styles.css'],
})

export class BidDemoComponent implements OnInit{
  price$: any;
  private subscription: Subscription;

  constructor(private apollo: Apollo) {
    this.subscription = new Subscription;
  }

  updatePrice(): void {
    const newPrice = Math.floor(Math.random() * 100); // Genera un nuevo precio aleatorio
    this.apollo.mutate({
      mutation: UPDATE_PRICE_MUTATION,
      variables: { newPrice },
    }).subscribe();
  }

  ngOnInit(): void {
    this.subscription = this.apollo
      .subscribe({
        query: PRICE_SUBSCRIPTION,
      })
      .subscribe((result: any) => {
        this.price$ = result.data.onPriceChange.price;
      });
  }
}
