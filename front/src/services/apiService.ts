import axios from 'axios';

const api = axios.create({
  baseURL: 'https://localhost:7167',
});

api.interceptors.request.use((config) => {
    const token = localStorage.getItem('ACCESS_TOKEN');

    if (token) {
        config.headers.Authorization = `Bearer ${token}`;
    }
  
    return config;
});

api.interceptors.response.use(
    (response) => response,
    (error) => {
        if (error.response?.status === 401) {
            localStorage.removeItem('ACCESS_TOKEN');
            window.location.href = '/login';
        }

        return Promise.reject(error);
    }
);

export default api;