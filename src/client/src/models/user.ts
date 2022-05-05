import faker from "@faker-js/faker";
import { Guid } from "guid-typescript";
import { randomInt } from "../util";
import { Order } from "./order";

export interface User {
    id: string;
    name: string;
    email: string;
    phone: string;
    orders?: Array<Order>
}

export class UserFactory {
    static create(amount: number) {
        const users: User[] = [];

        for (let i = 0; i < amount; i++) {
            const user: User = {
                id: Guid.create().toString(),
                name: faker.name.firstName() + faker.name.lastName(),
                email: faker.internet.email(),
                phone: faker.phone.phoneNumber('+45 #### ####'),
            }

            users.push(user);
        }

        return users;
    }
}