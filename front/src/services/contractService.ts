import api from './apiService';
import type { IContract } from '@/types/ContractTypes';

export async function getContracts(): Promise<IContract[]> {
    const route = "/contract";
    const response = await api.get<IContract[]>(route);
    
    return response.data;
}

export async function createContract(description: string, totalAmount: number, installmentAmounts: number): Promise<void> {
    const route = "/contract";
    const requestBody = { description, totalAmount, installmentAmounts };

    await api.post(route, requestBody);
}

export async function deleteContract(id: number): Promise<void> {
    const route = `/contract/${id}`;
    await api.delete(route);
}