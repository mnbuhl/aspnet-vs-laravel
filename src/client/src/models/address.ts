import faker from "@faker-js/faker";
import { Guid } from "guid-typescript";

export interface Address {
    id: string;
    addressLine: string;
    zipCode: string | number;
    city: string;
    country: string;
}

export class AddressFactory {
    static create(amount: number) {
        const addresses: Address[] = [];

        for (let i = 0; i < amount; i++) {
            const address: Address = {
                id: Guid.create().toString(),
                addressLine: faker.address.streetAddress(),
                zipCode: faker.address.zipCode(),
                city: faker.address.city(),
                country: faker.address.country()
            }

            addresses.push(address);
        }

        return addresses;
    }
}