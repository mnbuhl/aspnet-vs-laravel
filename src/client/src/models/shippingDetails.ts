import faker from "@faker-js/faker";
import { Guid } from "guid-typescript";
import { randomInt } from "../util";
import { Address, AddressFactory } from "./address";

export interface ShippingDetails {
    id: string;
    carrier: string;
    shippedAt?: Date | string | null;
    deliveredAt?: Date | string | null;
    shippingAddressId?: string;
    shippingAddress?: Address | null;
}

export class ShippingDetailsFactory {
    static create(amount: number, address?: Address | null) {
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
}