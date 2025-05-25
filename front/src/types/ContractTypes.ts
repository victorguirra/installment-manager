import type { IInstallment } from "./InstallmentTypes"

export interface IContract {
    id: number,
    description: string,
    installments: IInstallment[],
    userId: number,
    createdAt: Date,
    updatedAt: Date | null
}