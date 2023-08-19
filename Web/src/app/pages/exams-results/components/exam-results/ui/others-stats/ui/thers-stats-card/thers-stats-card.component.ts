import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-thers-stats-card',
  templateUrl: './thers-stats-card.component.html',
  styleUrls: ['./thers-stats-card.component.css']
})
export class ThersStatsCardComponent implements OnInit {
  @Input() othersStat: any;
  constructor() { }

  ngOnInit(): void {
  }

}
