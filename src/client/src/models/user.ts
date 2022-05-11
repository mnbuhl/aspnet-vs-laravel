import faker from '@faker-js/faker';
import { Guid } from 'guid-typescript';
import { Order, OrderSnake } from './order';

export interface User {
  id: string;
  name: string;
  email: string;
  phone: string;
  orders?: Array<Order>;
}

export interface UserSnake {
  id: string;
  name: string;
  email: string;
  phone: string;
  orders?: Array<OrderSnake>;
}

export class UserFactory {
  static create(amount: number) {
    const users: User[] = [];

    for (let i = 0; i < amount; i++) {
      const firstName = faker.name.firstName();
      const lastName = faker.name.lastName();

      const user: User = {
        id: Guid.create().toString(),
        name: firstName + ' ' + lastName,
        email: faker.internet.email(firstName, lastName, 'compare.rocks'),
        phone: faker.phone.phoneNumber('+45########')
      };

      users.push(user);
    }

    return users;
  }

  static createSnake(amount: number) {
    const users: UserSnake[] = [];

    for (let i = 0; i < amount; i++) {
      const firstName = faker.name.firstName();
      const lastName = faker.name.lastName();

      const user: UserSnake = {
        id: Guid.create().toString(),
        name: firstName + ' ' + lastName,
        email: faker.internet.email(firstName, lastName, 'compare.rocks'),
        phone: faker.phone.phoneNumber('+45########')
      };

      users.push(user);
    }

    return users;
  }
}
