import { Component, OnInit } from '@angular/core';
import { navigationElement } from 'src/app/shared/utils/models/navigationElement';
import { AuthService } from '../../services/auth.service';
import { UserRole } from 'src/app/shared/enums/userRole.enum';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.css']
})

export class NavigationComponent implements OnInit {
  showFullNavigation: boolean = true;

  navigationElements: navigationElement[] = [
    { routingPageName: "dashboard", displayName: "Dashboard", svgFileName: "dashboard.svg", shouldBeShown: true },
    { routingPageName: "exams", displayName: "Exams", svgFileName: "exams.svg", shouldBeShown: this.authService.getUserRole() == UserRole.Teacher },
    { routingPageName: "examsResults", displayName: "Exams", svgFileName: "exams.svg", shouldBeShown: this.authService.getUserRole() == UserRole.Student },
    { routingPageName: "members", displayName: "Members", svgFileName: "students.svg", shouldBeShown: this.authService.getUserRole() == UserRole.Teacher },
    { routingPageName: "members", displayName: "Students", svgFileName: "studentCap.svg", shouldBeShown: this.authService.getUserRole() == UserRole.Admin },
    { routingPageName: "teachers", displayName: "Teachers", svgFileName: "students.svg", shouldBeShown: this.authService.getUserRole() == UserRole.Admin },
    { routingPageName: "calendar", displayName: "Calendar", svgFileName: "calendar.svg", shouldBeShown: this.authService.getUserRole() == UserRole.Teacher },

  ];

  constructor(
    private authService: AuthService
  ) { }

  ngOnInit(): void {
  }

}
