import api from "./apiService";
import type { IInstallment, IInstallmentAnticipation } from "@/types/InstallmentTypes";

export async function getInstallments(contractId: number): Promise<IInstallment[]> {
    const route = `/installment/${contractId}`;
    const response = await api.get<IInstallment[]>(route);
    
    return response.data;
}

export async function getAnticipations(): Promise<IInstallmentAnticipation[]> {
    const route = `/installment/advance-request`;
    const response = await api.get<IInstallmentAnticipation[]>(route);
    
    return response.data;
}

export async function anticipateInstallments(installmentIds: number[]): Promise<void> {
    const route = `/installment/advance-request`;
    const requestBody = { installmentIds };

    const response = await api.post(route, requestBody);
    console.log({response})

    if (response.status !== 200) {
        console.log(response.data)
        throw new Error(response.data);
    }
}

export async function approveAnticipation(anticipationId: number): Promise<void> {
    const route = `/installment/advance-request/${anticipationId}/approve`;
    const response = await api.put(route);

    if (response.status !== 200) {
        throw new Error(response.data);
        console.log(response.data)
    }
}

export async function rejectAnticipation(anticipationId: number): Promise<void> {
    const route = `/installment/advance-request/${anticipationId}/reject`;
    const response = await api.put(route);

    if (response.status !== 200) {
        throw new Error(response.data);
    }
}