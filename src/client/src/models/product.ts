import { faker } from '@faker-js/faker'
import { Guid } from 'guid-typescript'
import { randomInt } from '../util';

export interface Product {
    id: string;
    name: string;
    price: number;
    amountInStock: number;
    description: string;
}

export class ProductFactory {
    static create(amount: number) {
        const products: Product[] = [];

        for (let i = 0; i < amount; i++) {
            const product: Product = {
                id: Guid.create().toString(),
                name: faker.commerce.product(),
                price: randomInt(100, 10000),
                amountInStock: randomInt(10000, 500000),
                description: faker.commerce.productDescription()
            }

            products.push(product);
        }

        return products;
    }
}