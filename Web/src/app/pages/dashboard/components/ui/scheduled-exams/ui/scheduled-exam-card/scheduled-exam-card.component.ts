import { Component, Input } from '@angular/core';
import { GroupScheduleExams } from '../../models/scheduleExam.model';
import { AuthService } from 'src/app/core/main/services/auth.service';
import { UserRole } from 'src/app/shared/enums/userRole.enum';
import { Router } from '@angular/router';

@Component({
  selector: 'app-scheduled-exam-card',
  templateUrl: './scheduled-exam-card.component.html',
  styleUrls: ['./scheduled-exam-card.component.css']
})
export class ScheduledExamCardComponent {
  @Input() groupScheduleExams: GroupScheduleExams;

  constructor(
    private AuthService: AuthService,
    private router: Router,
  ) { }

  moveToExam(id: string) {
    this.AuthService.getUserRole() == UserRole.Student
      ? this.router.navigate([`/examsResults`])
      : this.router.navigate([`/exams/${id}`]);
  }
}
