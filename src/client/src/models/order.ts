import { Address } from "./address";
import { OrderLine } from "./orderLine";
import { ShippingDetails } from "./shippingDetails";
import { User } from "./user";

export interface Order {
    total: number;
    date: Date|string;
    userId?: string;
    user?: User;
    billingAddressId?: string;
    billingAddress?: Address;
    shippingDetailsId?: string;
    shippingDetails?: ShippingDetails;
    orderLines: Array<OrderLine>
}