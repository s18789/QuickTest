import { Component, Inject, OnInit} from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { ConfirmationDialogConfiguration } from '../../models/confirmationDialogConfiguration.model';
import { ConfirmationDialogActionType } from '../../enums/confirmationDialogActionType.enum';

@Component({
  selector: 'app-confirmation-dialog',
  templateUrl: './confirmation-dialog.component.html',
  styleUrls: ['./confirmation-dialog.component.css']
})
export class ConfirmationDialogComponent implements OnInit {
  ConfirmationDialogActionType = ConfirmationDialogActionType;

  constructor(
    public dialogRef: MatDialogRef<ConfirmationDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public config: ConfirmationDialogConfiguration
    ) {}

  ngOnInit(): void {
    document.getElementById("mat-dialog-" + this.config.dialogCount).style.padding = "0";
  }

  closeDialog(actionType: ConfirmationDialogActionType) {
    this.dialogRef.close(actionType);
  }
}
