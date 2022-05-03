import { Order } from "./order";
import { Product } from "./product";

export interface OrderLine {
    id: string;
    price: number;
    quantity: number;
    discount: number;
    orderId?: string;
    order?: Order|null;
    productId?: string;
    product?: Product|null;
}