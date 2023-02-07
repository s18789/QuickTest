import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable, switchMap, tap, map } from 'rxjs';
import { Group } from '../models/group.model';
import { GroupGridModel } from '../models/groupGridModel';
import { Student } from '../models/student.model';
import { GroupsService } from '../services/groups.service';
import { StudentsService } from '../services/students.services';
import { AddGroupComponent } from './add-group/add-group.component';
import { AddStudentComponent } from './add-student/add-student.component';

@Component({
  selector: 'app-members',
  templateUrl: './members.component.html',
  styleUrls: ['./members.component.css']
})

export class MembersComponent implements OnInit {
  currentMemberType!: string;
  students!: Student[];
  groups!: GroupGridModel[];

  constructor(
    public dialog: MatDialog,
    private route: ActivatedRoute,
    private studentsService: StudentsService,
    private groupsService: GroupsService,
    private router: Router,
    ) { }

  ngOnInit(): void {
    this.currentMemberType = "student";
    this.getStudents().subscribe((response) => {
      this.students = response;
    });

    this.route.queryParams.subscribe(params => {
      if(!params['id']) {
        return;
      }

      if (this.currentMemberType == 'student') {
        this.studentsService.get(params['id']).subscribe(response => {
          this.addNewStudent(response);
        })
        return;
      } else {
        this.groupsService.get(params['id']).subscribe(response => {
          this.addNewGroup(response);
        })
        return;
      }
    });
  }

  addNewGroup(group?: Group) {
    let data: Group = {};

    if(!!group){
      data.id = group.id,
      data.name = group.name
    }

    const dialogRef = this.dialog.open(AddGroupComponent, {
      width: '700px',
      height: '656px',
      data: data
    });

    dialogRef.afterClosed().pipe(
      tap((x) => this.router.navigate([])),
      switchMap(r => this.getGroups())
    ).subscribe((response) => {
      this.groups = response;
    });
  }

  addNewStudent(student?: Student) {
    let data: Student = {};

    if(!!student){
      data.id = student.id;
      data.firstName = student.firstName;
      data.lastName = student.lastName;
      data.email = student.email;
      data.groupId = student.groupId;
    }

    this.groupsService.fetch().subscribe(response => {
      data.groups = response;
    })

    const dialogRef = this.dialog.open(AddStudentComponent, {
      width: '700px',
      height: '656px',
      data: data
    });

    dialogRef.afterClosed().pipe(
      tap((x) => this.router.navigate([])),
      switchMap(r => this.getStudents())
    ).subscribe((response) => {
      this.students = response;
    });
  }

  getStudents(): Observable<Student[]> {
    return this.studentsService.fetch().pipe(
      map((response: any[]) => response.map(x => {
          var student: Student = {
            id: x.id,
            firstName: x.firstName,
            lastName: x.lastName,
            email: x.email,
            groupName: x.groupDto.name
          };
          return student;
        })
      )
    );
  }

  getGroups(): Observable<GroupGridModel[]> {
    return this.groupsService.fetch();
  }

  showGroups() {
    this.currentMemberType = 'group';
    this.getGroups().subscribe((response) => {
      this.groups = response;
    });
  }

  showStudents() {
    this.currentMemberType = 'student';
    this.getStudents().subscribe((response) => {
      this.students = response;
    });
  }
}

