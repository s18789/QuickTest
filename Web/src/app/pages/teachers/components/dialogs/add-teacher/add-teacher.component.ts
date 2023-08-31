import { Component, EventEmitter, Inject, InjectionToken, OnInit, Output, ViewChild } from '@angular/core';
import { Subject, takeUntil, tap } from 'rxjs';
import { DialogComponent } from 'src/app/shared/components/dialog/dialog.component';
import { LoaderService } from 'src/app/shared/services/loaderService.service';
import { TeacherService } from '../../../services/teacher.service';
import { OverlayRef } from '@angular/cdk/overlay';
import { NotificationService } from 'src/app/shared/services/notification.service';

export const ADD_TEACHER_DIALOG_OVERLAY_REF = new InjectionToken<OverlayRef>(
  "ADD_TEACHER_DIALOG_OVERLAY_REF",
);

@Component({
  selector: 'app-add-teacher',
  templateUrl: './add-teacher.component.html',
  styleUrls: ['./add-teacher.component.css']
})
export class AddTeacherComponent {
  @ViewChild("dialog") dialog: DialogComponent;
  @Output() closeEvent = new EventEmitter();

  private destroyed$ = new Subject<void>();

  constructor(
    private notificationService: NotificationService,
    private loaderService: LoaderService,
    private teacherService: TeacherService,
    @Inject(ADD_TEACHER_DIALOG_OVERLAY_REF) public overlayRef: OverlayRef,
  ) { }

  submit(teacher: any): void {
    this.loaderService.show();

    this.teacherService.add(teacher).pipe(
      takeUntil(this.destroyed$),
      tap(() => {
        this.loaderService.hide();
        this.notificationService.showSuccess("Successfully added new teacher.");
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
