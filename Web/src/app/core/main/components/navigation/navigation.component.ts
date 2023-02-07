import { Component, OnInit } from '@angular/core';
import { navigationElement } from 'src/app/shared/utils/models/navigationElement';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.css']
})

export class NavigationComponent implements OnInit {
  shownFullNavigation: boolean = true;

  navigationElements: navigationElement[] = [
    { routingPageName: "dashboard", displayName: "Dashboard", svgFileName: "dashboard.svg", shouldBeShown: true },
    { routingPageName: "exams", displayName: "Exams", svgFileName: "exams.svg", shouldBeShown: this.authService.isUserTeacher() },
    { routingPageName: "exams-results", displayName: "Exams", svgFileName: "exams.svg", shouldBeShown: !this.authService.isUserTeacher() },
    { routingPageName: "reports", displayName: "Reports", svgFileName: "reports.svg", shouldBeShown: this.authService.isUserTeacher() },
    { routingPageName: "members", displayName: "Members", svgFileName: "students.svg", shouldBeShown: this.authService.isUserTeacher() },
    { routingPageName: "calendar", displayName: "Calendar", svgFileName: "calendar.svg", shouldBeShown: this.authService.isUserTeacher() }
  ];

  constructor(
    private authService: AuthService
  ) { }

  ngOnInit(): void {
  }

}
