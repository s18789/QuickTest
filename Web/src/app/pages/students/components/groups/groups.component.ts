import { Component, Input, OnInit } from '@angular/core';
import { GroupGridModel } from '../../models/groupGridModel';

@Component({
  selector: 'app-groups',
  templateUrl: './groups.component.html',
  styleUrls: ['./groups.component.css']
})
export class GroupsComponent implements OnInit {
  @Input()
  groups!: GroupGridModel[];

  configurations = [
    { displayName: "Name", key: "name", width: "w-3/5" },
    { displayName: "Students", key: "allStudents", width: "w-35/100" },
  ];

  constructor() { }

  ngOnInit(): void {
  }

}
