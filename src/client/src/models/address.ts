import faker from "@faker-js/faker";
import { Guid } from "guid-typescript";

export interface Address {
    id: string;
    addressLine: string;
    zipCode: string | number;
    city: string;
    country: string;
}

export interface AddressSnake {
    id: string;
    address_line: string;
    zip_code: string | number;
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
                zipCode: faker.address.zipCode('####'),
                city: faker.address.city(),
                country: faker.address.country()
            }

            addresses.push(address);
        }

        return addresses;
    }

    static createSnake(amount: number) {
        const addresses: AddressSnake[] = [];

        for (let i = 0; i < amount; i++) {
            const address: AddressSnake = {
                id: Guid.create().toString(),
                address_line: faker.address.streetAddress(),
                zip_code: faker.address.zipCode('####'),
                city: faker.address.city(),
                country: faker.address.country()
            }

            addresses.push(address);
        }

        return addresses;
    }
}