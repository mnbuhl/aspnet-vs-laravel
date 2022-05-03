import { Order } from "./order";

export interface User {
    id: string;
    name: string;
    email: string;
    phone: string;
    orders: Array<Order>
}