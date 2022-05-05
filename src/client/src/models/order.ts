import { Guid } from "guid-typescript";
import { randomInt } from "../util";
import { Address, AddressFactory } from "./address";
import { OrderLine, OrderLineFactory } from "./orderLine";
import { Product } from "./product";
import { ShippingDetails, ShippingDetailsFactory } from "./shippingDetails";
import { User } from "./user";

export interface Order {
    id: string;
    total?: number;
    date: Date | string;
    userId?: string;
    user?: User;
    billingAddressId?: string;
    billingAddress?: Address;
    shippingDetailsId?: string;
    shippingDetails?: ShippingDetails;
    orderLines: Array<OrderLine>
}

export class OrderFactory {
    static create(amount: number, user: User, products: Product[]) {
        const orders: Order[] = [];

        for (let i = 0; i < amount; i++) {
            const orderLines = randomInt(1, 5);

            const order: Order = {
                id: Guid.create().toString(),
                date: new Date(),
                userId: user.id,
                billingAddress: AddressFactory.create(1)[0],
                shippingDetails: ShippingDetailsFactory.create(1)[0],
                orderLines: OrderLineFactory.create(orderLines, products)
            }

            orders.push(order);
        }

        return orders;
    }
}