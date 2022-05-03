import axios, { AxiosResponse } from'axios';

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

const Orders = {
    
}