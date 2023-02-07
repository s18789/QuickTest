import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-grid',
  templateUrl: './grid.component.html',
  styleUrls: ['./grid.component.css']
})

export class GridComponent implements OnInit {

  @Input() routerName!: any;
  @Input() configurations!: any[];
  @Input() data!: any[];
  @Output() action: EventEmitter<any> = new EventEmitter();

  constructor() { }

  ngOnInit(): void {
  }
}
