import api from './apiService';
import type { IAuthResponse } from '@/types/AuthTypes';

export async function handleLogin(username: string, password: string): Promise<IAuthResponse> {
    const route = "/auth/login";
    const requestBody = { username, password };

    const response = await api.post<IAuthResponse>(route, requestBody);
    return response.data;
}

export async function handleRegister(name: string, username: string, password: string): Promise<IAuthResponse> {
    const route = "/auth/register";
    const requestBody = { name, username, password };

    const response = await api.post<IAuthResponse>(route, requestBody);
    return response.data;
}