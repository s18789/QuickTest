import { Injectable } from "@angular/core";
import { Observable, Subject } from "rxjs";
import { NotificationType } from "../enums/notificationType.enum";
import { Notification } from "../models/notification.model";

@Injectable()
export class NotificationService {

    private _subject = new Subject<Notification>();
    private _idx = 0;

    constructor() { }

    getObservable(): Observable<Notification> {
        return this._subject.asObservable();
    }

    showSuccess(message: string, timeout = 5000) {
        this._subject.next(new Notification(this._idx++, NotificationType.success, message, timeout));
    }

    showError(message: string, timeout = 5000) {
        this._subject.next(new Notification(this._idx++, NotificationType.error, message, timeout));
    }
}