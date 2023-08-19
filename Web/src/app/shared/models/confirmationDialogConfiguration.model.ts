import { ConfirmationDialogType } from "../enums/confirmationDialogType.enum"

export interface ConfirmationDialogConfiguration {
    title: string,
    description: string,
    dialogType: ConfirmationDialogType,
    cancelButtonText: string,
    confirmButtonText: string,
    dialogCount: number;
}