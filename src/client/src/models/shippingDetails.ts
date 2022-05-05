import faker from "@faker-js/faker";
import { Guid } from "guid-typescript";
import { randomInt } from "../util";
import { Address, AddressFactory, AddressSnake } from "./address";

export interface ShippingDetails {
    id: string;
    carrier: string;
    shippedAt?: Date | string | null;
    deliveredAt?: Date | string | null;
    shippingAddressId?: string;
    shippingAddress?: Address | null;
}

export interface ShippingDetailsSnake {
    id: string;
    carrier: string;
    shipped_at?: Date | string | null;
    delivered_at?: Date | string | null;
    shipping_address_id?: string;
    shipping_address?: AddressSnake | null;
}

export class ShippingDetailsFactory {
    static create(amount: number) {
        const shippingDetails: ShippingDetails[] = [];

        for (let i = 0; i < amount; i++) {
            const shippingDetail: ShippingDetails = {
                id: Guid.create().toString(),
                carrier: faker.random.word(),
                shippedAt: faker.date.past(),
                deliveredAt: randomInt(0, 10) > 8 ? faker.date.future() : null,
                shippingAddress: AddressFactory.create(1)[0],
            }

            shippingDetails.push(shippingDetail);
        }

        return shippingDetails;
    }

    static createSnake(amount: number) {
        const shippingDetails: ShippingDetailsSnake[] = [];

        for (let i = 0; i < amount; i++) {
            const shippingDetail: ShippingDetailsSnake = {
                id: Guid.create().toString(),
                carrier: faker.random.word(),
                shipped_at: faker.date.past(),
                delivered_at: randomInt(0, 10) > 8 ? faker.date.future() : null,
                shipping_address: AddressFactory.createSnake(1)[0],
            }

            shippingDetails.push(shippingDetail);
        }

        return shippingDetails;
    }
}