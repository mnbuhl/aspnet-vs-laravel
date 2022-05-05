import { Guid } from "guid-typescript";
import { randomInt } from "../util";
import { Address, AddressFactory, AddressSnake } from "./address";
import { OrderLine, OrderLineFactory, OrderLineSnake } from "./orderLine";
import { Product, ProductSnake } from "./product";
import { ShippingDetails, ShippingDetailsFactory, ShippingDetailsSnake } from "./shippingDetails";
import { User, UserSnake } from "./user";

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

export interface OrderSnake {
    id: string;
    total?: number;
    date: Date | string;
    user_id?: string;
    user?: UserSnake;
    billing_address_id?: string;
    billing_address?: AddressSnake;
    shipping_details_id?: string;
    shipping_details?: ShippingDetailsSnake;
    order_lines: Array<OrderLineSnake>
}

export class OrderFactory {
    static create(amount: number, users: User[], products: Product[]) {
        const orders: Order[] = [];

        for (let i = 0; i < amount; i++) {
            const orderLines = randomInt(1, 3);
            const user = users[randomInt(0, users.length - 1)];

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

    static createSnake(amount: number, users: UserSnake[], products: ProductSnake[]) {
        const orders: OrderSnake[] = [];

        for (let i = 0; i < amount; i++) {
            const orderLines = randomInt(1, 3);
            const user = users[randomInt(0, users.length - 1)];

            const order: OrderSnake = {
                id: Guid.create().toString(),
                date: new Date(),
                user_id: user.id,
                billing_address: AddressFactory.createSnake(1)[0],
                shipping_details: ShippingDetailsFactory.createSnake(1)[0],
                order_lines: OrderLineFactory.createSnake(orderLines, products)
            }

            orders.push(order);
        }

        return orders;
    }
}