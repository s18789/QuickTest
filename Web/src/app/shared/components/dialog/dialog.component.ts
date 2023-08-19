import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-dialog',
  templateUrl: './dialog.component.html',
  styleUrls: ['./dialog.component.scss']
})

export class DialogComponent {
  @Output() closeEvent = new EventEmitter();
  @Output() saveEvent = new EventEmitter();
  @Input() MemberTypeName: string;

  constructor() { }

  closeHandler(): void {
    this.closeEvent.emit();
  }

  saveHandler(): void {
    this.saveEvent.emit();
  }
}

