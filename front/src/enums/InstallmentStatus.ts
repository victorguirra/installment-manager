export const InstallmentStatusEnum = {
  Open: 1,
  Paid: 2,
} as const;

export type InstallmentStatusEnum = (typeof InstallmentStatusEnum)[keyof typeof InstallmentStatusEnum];
