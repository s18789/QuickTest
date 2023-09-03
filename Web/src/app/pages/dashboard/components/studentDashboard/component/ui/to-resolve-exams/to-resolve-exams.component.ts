import { Component, Input, OnInit } from '@angular/core';
import { ExamToResolve } from '../../../models/exam.model';
import { ConfirmationDialogComponent } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.component';
import { ConfirmationDialogType } from 'src/app/shared/enums/confirmationDialogType.enum';
import { tap } from 'rxjs';
import { ConfirmationDialogActionType } from 'src/app/shared/enums/confirmationDialogActionType.enum';
import { ExamSolveService } from 'src/app/core/main/services/examSolveService.service';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-to-resolve-exams',
  templateUrl: './to-resolve-exams.component.html',
  styleUrls: ['./to-resolve-exams.component.css']
})
export class ToResolveExamsComponent implements OnInit {
  @Input() examToResolve: ExamToResolve;
  @Input() examsToResolve: ExamToResolve[];

  main: boolean = true;
  startExamDialogCount: number = 0;
  
  constructor(
    private dialog: MatDialog,
    private examSolveService: ExamSolveService,
  ) { }

  ngOnInit(): void {
  }

  openExamStartConfirmationDialog(id: string) {
    const dialogRef = this.dialog.open(ConfirmationDialogComponent, {
      data: {
        title: "Start exam",
        description: "Are you sure you want to start the exam?",
        dialogType: ConfirmationDialogType.Confimation,
        cancelButtonText: "Cancel",
        confirmButtonText: "Start",
        dialogCount: this.startExamDialogCount,
      },
    }, );

    dialogRef.afterClosed().pipe(
      tap((actionType: ConfirmationDialogActionType) => {
        if (actionType == ConfirmationDialogActionType.Submit) {
          this.examSolveService.prepareExam(id);
        }

        this.startExamDialogCount++;
      })
    ).subscribe();
  }

}
