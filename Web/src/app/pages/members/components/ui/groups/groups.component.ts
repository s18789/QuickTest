import { Component, Input } from '@angular/core';
import { GroupGridModel } from '../../../models/groups/group.model';

@Component({
  selector: 'app-groups',
  templateUrl: './groups.component.html',
  styleUrls: ['./groups.component.css']
})
export class GroupsComponent{
  @Input() groups: GroupGridModel[];

  configurations = [
    { displayName: "Name", key: "name", styles: "w-3/5" },
    { displayName: "Students", key: "allStudents", styles: "w-35/100" },
  ];

  constructor() { }

}
