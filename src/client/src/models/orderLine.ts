import { Guid } from "guid-typescript";
import { randomInt } from "../util";
import { Order } from "./order";
import { Product } from "./product";

export interface OrderLine {
    id: string;
    price: number;
    quantity: number;
    discount: number;
    orderId?: string;
    order?: Order | null;
    productId?: string;
    product?: Product | null;
}

export class OrderLineFactory {
    static create(amount: number, products: Product[]) {
        const orderLines: OrderLine[] = [];

        for (let i = 0; i < amount; i++) {
            const product = products[randomInt(0, products.length - 1)];

            const orderLine: OrderLine = {
                id: Guid.create().toString(),
                price: product.price,
                quantity: randomInt(1, 5),
                discount: randomInt(0, 100),
                productId: product.id,
            }

            orderLines.push(orderLine);
        }

        return orderLines;
    }
}