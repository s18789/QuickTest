import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-scheduled-exams',
  templateUrl: './scheduled-exams.component.html',
  styleUrls: ['./scheduled-exams.component.css']
})
export class ScheduledExamsComponent implements OnInit {
  exams: any[];
  constructor() { }

  ngOnInit(): void {
    this.exams = [
      {
        date: new Date(2023, 0, 5),
        exams: [
          {
            date: new Date(2023, 0, 5, 9, 0),
            title: "The homologous series of the alkanes",
            class: "3 Biol-Chem"
          },
          {
            date: new Date(2023, 0, 5, 12, 0),
            title: "Concentrations of solutions",
            class: "2 Mat-Chem"
          },
        ]
      },
      {
        date: new Date(2023, 0, 16),
        exams: [
          {
            date: new Date(2023, 0, 16, 8, 0),
            title: "Factors affecting rate of reactions",
            class: "1 Biol-Ger"
          },
        ],
      }
    ]
  }

}
