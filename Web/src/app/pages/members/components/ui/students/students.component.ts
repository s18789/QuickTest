import { Component, Input } from '@angular/core';
import { Student } from '../../../models/students/student.model';


@Component({
  selector: 'app-students',
  templateUrl: './students.component.html',
  styleUrls: ['./students.component.css']
})

export class StudentsComponent {
  @Input() students!: Student[];

  configurations = [
    { displayName: "", key: "", styles: "w-5/100" },
    { displayName: "First name", key: "firstName", styles: "w-1/5" },
    { displayName: "Last name", key: "lastName", styles: "w-1/5" },
    { displayName: "E-mail", key: "email", styles: "w-35/100" },
    { displayName: "Group", key: "group", styles: "w-15/100" }
  ];

  constructor() { }
}
