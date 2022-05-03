import { Address } from "./address";

export interface ShippingDetails {
    id: string;
    carrier: string;
    shippedAt?: Date|string|null;
    deliveredAt?: Date|string|null;
    shippingAddressId?: string;
    shippingAddress?: Address|null;
}