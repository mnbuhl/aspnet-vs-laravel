import axios, { AxiosResponse } from'axios';
import { Order } from '../models/order';
import { PaginatedResult } from '../models/pagination';

const urls = {
    dotnet: import.meta.env.VITE_API_DOTNET,
    laravel: import.meta.env.VITE_API_LARAVEL
};

const responseBody = <T>(response: AxiosResponse<T>) => response.data;

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

const Orders = {
    get: (id: string) => dotnetRequests.get<Order>('orders/' + id)
}

const agent = {
    Orders
}

export default agent;