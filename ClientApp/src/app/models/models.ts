export interface Users {
  user_id: number;
  user_name: string;
}

export interface Items {
  item_id: number;
  item_name: string;
  item_description: string;
  user_id: number;
}

export interface Auctions {
  auction_id: number;
  item_id: number;
  auction_start_date: Date;
}
