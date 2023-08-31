import { OverlayRef } from '@angular/cdk/overlay';
import { Component, EventEmitter, Inject, InjectionToken, Input, Output, ViewChild } from '@angular/core';
import { Observable, Subject, takeUntil, tap } from 'rxjs';
import { StudentService } from 'src/app/pages/members/services/students/student.services';
import { DialogComponent } from 'src/app/shared/components/dialog/dialog.component';
import { LoaderService } from 'src/app/shared/services/loaderService.service';
import { NotificationService } from 'src/app/shared/services/notification.service';

export const ADD_STUDENT_DIALOG_OVERLAY_REF = new InjectionToken<OverlayRef>(
  "ADD_STUDENT_DIALOG_OVERLAY_REF",
);

@Component({
  selector: 'app-add-student',
  templateUrl: './add-student.component.html',
  styleUrls: ['./add-student.component.css']
})

export class AddStudentComponent {
  @ViewChild("dialog") dialog: DialogComponent;

  @Output() closeEvent = new EventEmitter();
  @Input() groups: Observable<any>;

  private destroyed$ = new Subject<void>();

  constructor(
    private notificationService: NotificationService,
    private loaderService: LoaderService,
    private studentService: StudentService,
    @Inject(ADD_STUDENT_DIALOG_OVERLAY_REF) public overlayRef: OverlayRef,
  ) { }

  submit(student: any): void {
    this.loaderService.show();

    this.studentService.add(student).pipe(
      takeUntil(this.destroyed$),
      tap(() => {
        this.loaderService.hide();
        this.notificationService.showSuccess("Successfully added new student.");
        this.close();
      })
    ).subscribe();
  }

  ngOnDestroy() {
    this.destroyed$.next();
    this.destroyed$.complete();
  }

  close(): void {
    this.closeEvent.emit();
    this.overlayRef.dispose();
  }

}
