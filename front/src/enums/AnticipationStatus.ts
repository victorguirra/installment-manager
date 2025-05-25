export const AnticipationStatusEnum = {
    Pending: 1,
    Approved: 2,
    Rejected: 3
} as const;

export type AnticipationStatusEnum = (typeof AnticipationStatusEnum)[keyof typeof AnticipationStatusEnum];
