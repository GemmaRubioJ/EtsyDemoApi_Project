import { CartItemDto } from './CartItemDto';

export interface CartRequest {
  userId: number;
  products: CartItemDto[];
}
