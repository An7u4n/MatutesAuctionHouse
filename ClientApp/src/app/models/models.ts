export interface User {
  user_id: number;
  user_name: string;
  password: string;
  email: string;
}

export interface Item {
  item_id: number;
  item_name: string;
  item_description: string;
  user_id: number;
}

export interface Auction {
  auction_id: number;
  item_id: number;
  auction_start_date: Date;
  price: number;
}

export interface Response {
  success: number;
  message: string;
  data: object;
}
