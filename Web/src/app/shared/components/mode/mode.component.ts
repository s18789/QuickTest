import { Component, OnInit } from '@angular/core';
import { ModeType } from '../../enums/mode.enum';

@Component({
  selector: 'app-mode',
  templateUrl: './mode.component.html',
  styleUrls: ['./mode.component.css']
})
export class ModeComponent implements OnInit {
  mode: ModeType = ModeType.Light;

  constructor() { }

  ngOnInit(): void {
  }

  changeMode() {
    if (this.mode == ModeType.Light) {
      this.mode = ModeType.Dark;
      document.documentElement.classList.add('dark');
    } else {
      this.mode = ModeType.Light;
      document.documentElement.classList.remove('dark');
    }
  }
}
