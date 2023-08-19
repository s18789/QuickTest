import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-others-stats',
  templateUrl: './others-stats.component.html',
  styleUrls: ['./others-stats.component.css']
})
export class OthersStatsComponent implements OnInit {
  othersStats: any[];
  constructor() { }

  ngOnInit(): void {
    this.othersStats = [
      {
        title: "the best",
        increase: true,
        kpi: 13.1,
        average: 4.67
      },
      {
        title: "average",
        increase: true,
        kpi: 3.1,
        average: 4.01
      },
      {
        title: "the worste",
        increase: false,
        kpi: 10.9,
        average: 3.14,
      },
    ]
  }

}
