import { Component, Input } from '@angular/core';
import { Student } from '../../../models/students/student.model';
import { GridItemConfiguration } from 'src/app/shared/utils/model/GridConfiguration.model';
import { ConfigurationItemType } from 'src/app/shared/utils/model/enums/configurationItemType.enum';


@Component({
  selector: 'app-students',
  templateUrl: './students.component.html',
  styleUrls: ['./students.component.css']
})

export class StudentsComponent {
  @Input() students!: Student[];

  configurations: GridItemConfiguration[] = [
    { displayName: "First name", key: "firstName", styles: "w-1/5" },
    { displayName: "Last name", key: "lastName", styles: "w-1/5" },
    { displayName: "E-mail", key: "email", styles: "w-4/10" },
    { displayName: "Group", type: ConfigurationItemType.object, nestedKey: "name", key: "group", styles: "w-15/100" }
  ];

  constructor() { }
}
