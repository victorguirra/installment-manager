import type { AnticipationStatusEnum } from "@/enums/AnticipationStatus";
import type { InstallmentStatusEnum } from "@/enums/InstallmentStatus";

export interface IInstallment {
    id: number,
    code: string,
    status: InstallmentStatusEnum,
    amount: number,
    dueDate: Date,
    anticipated: boolean,
    contractId: number,
    createdAt: Date,
    updatedAt: Date | null
}

export interface IInstallmentAnticipation {
    id: number;
    installmentId: number;
    installment: IInstallment;
    status: AnticipationStatusEnum;
    createdAt: Date,
    updatedAt: Date | null
};