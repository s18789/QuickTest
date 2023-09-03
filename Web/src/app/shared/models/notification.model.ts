import { NotificationType } from "../enums/notificationType.enum";

export class Notification {
    constructor(
        public id: number,
        public type: NotificationType,
        public message: string,
        public timeout: number,
    ) { }
}