import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/core/main/services/auth.service';
import { UserRole } from 'src/app/shared/enums/userRole.enum';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  userRole: string;
  UserRole = UserRole;

  constructor(
    private authService: AuthService
  ) { }

  ngOnInit(): void {
    this.userRole = this.authService.getUserRole();
  }

}
