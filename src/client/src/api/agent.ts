import axios, { AxiosResponse } from'axios';

const urls = {
    dotnet: import.meta.env.VITE_API_DOTNET,
    laravel: import.meta.env.VITE_API_LARAVEL
};

const responseBody = (response: AxiosResponse) => response.data;

const dotnetRequests = {
    get: <T>(url: string) => axios.get<T>(`${urls.dotnet}/${url}`).then(responseBody),
    post: <T>(url: string, body: {}) => axios.post<T>(`${urls.dotnet}/${url}`, body).then(responseBody),
    put: <T>(url: string, body: {}) => axios.put<T>(`${urls.dotnet}/${url}`, body).then(responseBody),
    delete: <T>(url: string) => axios.delete<T>(`${urls.dotnet}/${url}`).then(responseBody),
}

const laravelRequests = {
    get: <T>(url: string) => axios.get<T>(`${urls.laravel}/${url}`).then(responseBody),
    post: <T>(url: string, body: {}) => axios.post<T>(`${urls.laravel}/${url}`, body).then(responseBody),
    put: <T>(url: string, body: {}) => axios.put<T>(`${urls.laravel}/${url}`, body).then(responseBody),
    delete: <T>(url: string) => axios.delete<T>(`${urls.laravel}/${url}`).then(responseBody),
}