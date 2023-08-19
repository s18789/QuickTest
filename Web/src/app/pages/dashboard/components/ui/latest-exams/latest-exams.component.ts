import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-latest-exams',
  templateUrl: './latest-exams.component.html',
  styleUrls: ['./latest-exams.component.css']
})
export class LatestExamsComponent implements OnInit {
  exams: any;
  constructor() { }

  ngOnInit(): void {
    this.exams = [
      {
        id: 1,
        title: "Preparation of salts by precipitation methods",
      },
      {
        id: 2,
        title: "Concentrations of solutions",
      },
      {
        id: 3,
        title: "Factors affecting rate of reactions",
      },
      {
        id: 4,
        title: "The homologous series of the alkanes",
      },
    ]
  }

}
