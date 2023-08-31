import { Component, OnDestroy, OnInit } from '@angular/core';
import { Notification } from '../../models/notification.model';
import { Subscription } from 'rxjs';
import { NotificationService } from '../../services/notification.service';
import { NotificationType } from '../../enums/notificationType.enum';
import { animate, style, transition, trigger } from "@angular/animations";

@Component({
  selector: 'app-notification',
  templateUrl: './notification.component.html',
  styleUrls: ['./notification.component.css'],
  animations: [
    trigger("showNotification", [
      transition(":enter", [
        style({ transform: "translateY(-200%)" }),
        animate(
          "200ms cubic-bezier(.46,.33,.5,1.68)",
          style({ transform: "translateY(0%)" })
        ),
      ]),
      transition(":leave", [
        animate(
          "200ms cubic-bezier(.38,-0.59,.72,.88)",
          style({ transform: "translateY(-200%)" })
        ),
      ]),
    ]),
  ],
})
export class NotificationComponent implements OnInit, OnDestroy {
  notifications: Notification[] = [];
  private subscription: Subscription;

  constructor(private notificationService: NotificationService) {}

  private addNotification(notification: Notification) {
    this.notifications.push(notification);
    if (notification.timeout !== 0) {
      setTimeout(() => this.close(notification), notification.timeout);
    }
  }

  ngOnInit() {
    this.subscription = this.notificationService
      .getObservable()
      .subscribe((notification) => this.addNotification(notification));
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

  close(notification: Notification) {
    this.notifications = this.notifications.filter(
      (x) => x.id !== notification.id
    );
  }

  className(notification: Notification): string {
    let style: string;

    switch (notification.type) {
      case NotificationType.success:
        style = "bg-green-400";
        break;

      case NotificationType.error:
        style = "bg-red-400";
        break;
    }

    return style;
  }
}
