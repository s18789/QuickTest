import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable, concatMap, map, tap } from 'rxjs';
import { ExamMapperService } from '../../services/examMapper.service';
import { ExamsService } from '../../services/exams.service';
import { Exam } from '../../models/exam.model';
import { GridItemConfiguration } from '../../../../shared/utils/model/GridConfiguration.model';
import { ActionConfiguration } from '../../../../shared/utils/model/actionConfiguration.model';
import { ConfirmationDialogComponent } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.component';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmationDialogType } from 'src/app/shared/enums/confirmationDialogType.enum';
import { ConfirmationDialogActionType } from 'src/app/shared/enums/confirmationDialogActionType.enum';
import { StudentService } from 'src/app/pages/members/services/students/student.services';
import { StudentDialogComponent } from '../add-exam/student-dialog/student-dialog.component';
import { StudentDialog } from 'src/app/pages/members/models/students/studentDialog.model';
import { StudentResponse } from 'src/app/pages/members/models/students/studentResponse.model';
import { StudentMapperService } from 'src/app/pages/members/services/students/studentMapper.service';
import { ConfigurationItemType } from 'src/app/shared/utils/model/enums/configurationItemType.enum';
import { ExamResultStatus } from 'src/app/pages/exams-results/enums/examResultStatus.enum';
import { LoaderService } from 'src/app/shared/services/loaderService.service';
import { NotificationService } from 'src/app/shared/services/notification.service';
import { ExamStatus } from '../../enums/examStatus.enum';

@Component({
  selector: 'app-exam',
  templateUrl: './exam.component.html',
  styleUrls: ['./exam.component.css']
})
export class ExamComponent implements OnInit {
  private readonly examId: string = this.route.snapshot.params["id"];
  exam$!: Observable<Exam>;

  ExamStatus = ExamStatus;
  finishExamDialogCount: number = 0;

  configurations: GridItemConfiguration[] = [
    { displayName: "Full name", key: "fullName", styles: "w-1/5" },
    { displayName: "E-mail", key: "email", styles: "w-1/2" },
    { displayName: "Status", key: "status", type: ConfigurationItemType.enum, enum: ExamResultStatus, styles: "w-1/5" },
    { displayName: "Score", key: "score", type: ConfigurationItemType.score, styles: "w-1/10" }
  ];

  searchConfiguration: ActionConfiguration = { propertyName: 'fullName' };
  filterConfiguration: ActionConfiguration = { propertyName: 'status', type: ConfigurationItemType.enum, enumType: ExamResultStatus, };

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private examsService: ExamsService,
    private examMapperService: ExamMapperService,
    private studentMapperService: StudentMapperService,
    private dialog: MatDialog,
    private studentService: StudentService,
    private loaderService: LoaderService,
    private notificationService: NotificationService,
    ) { }

  ngOnInit(): void {
    this.exam$ = this.getExam();
  }

  getExam(): Observable<Exam> {
    this.loaderService.show();
    return this.examsService.getExam(this.examId).pipe(
      map((exam) =>
        this.examMapperService.mapExamResponseToExam(exam)
      ),
      tap(() => this.loaderService.hide())
    );
  }

  finishExam() {
    const dialogRef = this.dialog.open(ConfirmationDialogComponent, {
      data: {
        title: "Finish exam",
        description: "Are you sure you want to finish exam? This action cannot be undone.",
        dialogType: ConfirmationDialogType.Error,
        cancelButtonText: "Cancel",
        confirmButtonText: "Ok",
        dialogCount: this.finishExamDialogCount,
      },
    }, );

    dialogRef.afterClosed().pipe(
      tap((actionType:ConfirmationDialogActionType) => {
        if (actionType == ConfirmationDialogActionType.Submit) {
          this.examsService.finishExam(this.examId).pipe(
            tap(() => this.notificationService.showSuccess("Successfully deactivated exam."))
          ).subscribe();
        }

        this.finishExamDialogCount++;
      })
    ).subscribe();
  }

  addNewExamMember() {
    this.studentService.getStudentsForExam(this.examId).pipe(
      map((response: StudentResponse[]) => response.map(studentResponse => this.studentMapperService.mapStudentResponseToStudentDialog(studentResponse))),
      tap((students) => this.openDialog(students))
    ).subscribe();
  }

  openDialog(students: StudentDialog[]) {
    const dialogRef = this.dialog.open(StudentDialogComponent, {
      data: students
    });

    dialogRef.afterClosed().pipe(
      tap((selectedStudentsIndexes: string[] | null) => {
        if (!selectedStudentsIndexes) {
          return;
        }
  
        this.examsService.addExamMembers({examId: this.examId, membersIds: selectedStudentsIndexes}).pipe(
          tap(() => {this.exam$ = this.getExam();})
        ).subscribe();
      })
    ).subscribe();
  }

  openExamPreview() {
    this.router.navigate([`./exams/${this.examId}/preview`]);
  }
}
