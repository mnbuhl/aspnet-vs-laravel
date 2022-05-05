import axios from 'axios';
import { Order } from '../models/order';
import { Product } from '../models/product';
import { User } from '../models/user';

const urls = {
    dotnet: import.meta.env.VITE_API_DOTNET,
    laravel: import.meta.env.VITE_API_LARAVEL
};

const dotnetRequests = {
    get: <T>(url: string) => axios.get<T>(`${urls.dotnet}/${url}`).then(res => res.data),
    post: <T>(url: string, body: {}) => axios.post<T>(`${urls.dotnet}/${url}`, body).then(res => res.data),
    put: <T>(url: string, body: {}) => axios.put<T>(`${urls.dotnet}/${url}`, body).then(res => res.data),
    delete: <T>(url: string) => axios.delete<T>(`${urls.dotnet}/${url}`).then(res => res.data),
}

const laravelRequests = {
    get: <T>(url: string) => axios.get<T>(`${urls.laravel}/${url}`).then(res => res.data),
    post: <T>(url: string, body: {}) => axios.post<T>(`${urls.laravel}/${url}`, body).then(res => res.data),
    put: <T>(url: string, body: {}) => axios.put<T>(`${urls.laravel}/${url}`, body).then(res => res.data),
    delete: <T>(url: string) => axios.delete<T>(`${urls.laravel}/${url}`).then(res => res.data),
}

class Agent {
    framework: string;
    requests: {
        get: <T>(url: string) => Promise<T>
        post: <T>(url: string, body: {}) => Promise<T>
        put: <T>(url: string, body: {}) => Promise<T>
        delete: <T>(url: string) => Promise<T>
    };

    constructor(framework: string) {
        this.framework = framework;
        this.requests = framework === 'laravel' ? laravelRequests : dotnetRequests;
    }

    Orders = {
        get: (id: string) => this.requests.get<Order>('orders/' + id),
        post: (body: Order) => this.requests.post<Order>('orders', body),
    };

    Products = {
        get: (id: string) => this.requests.get<Product>('products/' + id),
        post: (body: Product) => this.requests.post<Product>('products', body),
    }

    Users = {
        get: (id: string) => this.requests.get<User>('users/' + id),
        post: (body: User) => this.requests.post<User>('users', body),
    }

    Demo = {
        deleteDb: () => dotnetRequests.post('demo', {}),
    }
}

export default Agent;