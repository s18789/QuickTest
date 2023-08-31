import { OverlayRef } from '@angular/cdk/overlay';
import { Component, EventEmitter, Inject, InjectionToken, Input, Output, ViewChild } from '@angular/core';
import { Observable, Subject, takeUntil, tap } from 'rxjs';
import { GroupService } from 'src/app/pages/members/services/groups/group.service';
import { DialogComponent } from 'src/app/shared/components/dialog/dialog.component';
import { LoaderService } from 'src/app/shared/services/loaderService.service';
import { NotificationService } from 'src/app/shared/services/notification.service';

export const ADD_GROUP_DIALOG_OVERLAY_REF = new InjectionToken<OverlayRef>(
  "ADD_GROUP_DIALOG_OVERLAY_REF",
);

@Component({
  selector: 'app-add-group',
  templateUrl: './add-group.component.html',
  styleUrls: ['./add-group.component.css']
})

export class AddGroupComponent {
    @ViewChild("dialog") dialog: DialogComponent;

    @Output() closeEvent = new EventEmitter();
    @Input() groups: Observable<any>;

    private destroyed$ = new Subject<void>();

    constructor(
      private notificationService: NotificationService,
      private loaderService: LoaderService,
      private groupService: GroupService,
      @Inject(ADD_GROUP_DIALOG_OVERLAY_REF) public overlayRef: OverlayRef,
    ) { }

    submit(group: any): void {
      this.loaderService.show();

      this.groupService.add(group).pipe(
        takeUntil(this.destroyed$),
        tap(() => {
          this.loaderService.hide();
          this.notificationService.showSuccess("Successfully added new group.");
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
