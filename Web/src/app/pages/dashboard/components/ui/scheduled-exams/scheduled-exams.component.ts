import { Component, OnInit } from '@angular/core';
import { Observable, map } from 'rxjs';
import { ExamsService } from 'src/app/pages/exams/services/exams.service';
import { GroupScheduleExams, ScheduleExamResponse } from './models/scheduleExam.model';
import { ExamScheduleMapperService } from './services/scheduleExamMapper.service';
import { ExamsResultsService } from 'src/app/pages/exams-results/services/exams-results.service';
import { AuthService } from 'src/app/core/main/services/auth.service';
import { UserRole } from 'src/app/shared/enums/userRole.enum';

@Component({
  selector: 'app-scheduled-exams',
  templateUrl: './scheduled-exams.component.html',
  styleUrls: ['./scheduled-exams.component.css']
})
export class ScheduledExamsComponent implements OnInit {
  groupScheduleExams$: Observable<GroupScheduleExams[]>;

  constructor(
    private authService: AuthService,
    private examsService: ExamsService,
    private examsResultsService: ExamsResultsService,
    private examScheduleMapperService: ExamScheduleMapperService
  ) { }

  ngOnInit(): void {
    this.groupScheduleExams$ = this.getScheduleExams();
  }

  getScheduleExams(): Observable<GroupScheduleExams[]> {
    const scheduleExams = this.authService.getUserRole() == UserRole.Student
     ? this.examsResultsService.getScheduleExams()
     : this.examsService.getScheduleExams();

    return scheduleExams.pipe(
      map((scheduleExamsResponse: ScheduleExamResponse[]) => 
          this.examScheduleMapperService.mapScheduleExamsResponseToGroupScheduleExams(scheduleExamsResponse))
    );
  }
}