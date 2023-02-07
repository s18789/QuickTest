import { Component, Input, OnInit } from '@angular/core';
import { Student } from '../../models/student.model';

@Component({
  selector: 'app-students',
  templateUrl: './students.component.html',
  styleUrls: ['./students.component.css']
})

export class StudentsComponent implements OnInit {
  @Input()
  students!: Student[];

  configurations = [
    { displayName: "", key: "", width: "w-5/100" },
    { displayName: "First name", key: "firstName", width: "w-1/5" },
    { displayName: "Last name", key: "lastName", width: "w-1/5" },
    { displayName: "E-mail", key: "email", width: "w-35/100" },
    { displayName: "Group", key: "groupName", width: "w-15/100" }
  ];

  constructor() { }

  ngOnInit(): void {
  }
}
