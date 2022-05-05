import axios from 'axios';
import { Order, OrderSnake } from '../models/order';
import { Product, ProductSnake } from '../models/product';
import { User, UserSnake } from '../models/user';

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
        axios.defaults.headers.common['Content-Type'] = 'text/plain';
    }

    Orders = {
        get: (id: string) => this.requests.get<Order | OrderSnake>('orders/' + id),
        post: (body: Order | OrderSnake) => this.requests.post<Order | OrderSnake>('orders', body),
    };

    Products = {
        get: (id: string) => this.requests.get<Product | ProductSnake>('products/' + id),
        post: (body: Product | ProductSnake) => this.requests.post<Product | ProductSnake>('products', body),
    }

    Users = {
        get: (id: string) => this.requests.get<User | UserSnake>('users/' + id),
        post: (body: User | UserSnake) => this.requests.post<User | UserSnake>('users', body),
    }

    Demo = {
        deleteDb: () => this.requests.post('demo', {}),
    }
}

export default Agent;