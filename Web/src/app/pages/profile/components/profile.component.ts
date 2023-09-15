import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ChangePasswordDialogComponent } from './change-password-dialog/change-password-dialog.component';
import { Observable, tap } from 'rxjs';
import { AuthService } from 'src/app/core/main/services/auth.service';
import { NotificationService } from 'src/app/shared/services/notification.service';
import { UserInfo } from 'src/app/core/main/models/authDataModel';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  userInfo: Observable<UserInfo>

  constructor(
    private dialog: MatDialog,
    private authService: AuthService,
    private notificationService: NotificationService,
  ) { }

  ngOnInit(): void {
    this.userInfo = this.getUserInfo();
  }

  getUserInfo(): Observable<UserInfo> {
    return this.authService.getUserInfo().pipe(

    );
  }

  OpenDialogChangePassword() {
    const dialogRef = this.dialog.open(ChangePasswordDialogComponent);

    dialogRef.afterClosed().pipe(
      tap((newPassword: string | null) => {
        if (!newPassword) {
          return;
        }
  
        this.authService.changePassword(newPassword).pipe(
          tap((response: Boolean) => {
            debugger;
            if(response)
              this.notificationService.showSuccess("Password changed successfully.");
          })
        ).subscribe();
      })
    ).subscribe();
  }
}
