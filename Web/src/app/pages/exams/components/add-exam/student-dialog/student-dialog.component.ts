import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MemberType } from 'src/app/pages/members/enums/memberType.enum';
import { SelectableGroupDialog, StudentDialog } from 'src/app/pages/members/models/students/studentDialog.model';

@Component({
  selector: 'app-student-dialog',
  templateUrl: './student-dialog.component.html',
  styleUrls: ['./student-dialog.component.css']
})

export class StudentDialogComponent implements OnInit {
  currentMemberType: MemberType = MemberType.Student;
  MemberType = MemberType;

  filters: any = { search: "" };
  students: StudentDialog[];
  groups: SelectableGroupDialog[];


  constructor(
    public dialogRef: MatDialogRef<StudentDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: StudentDialog[]
  ) {}

  ngOnInit(): void {
    this.fightWithMaterialDialog();

    this.students = this.getStudents();
    this.groups = this.getGroups();
  }

  getStudents(): StudentDialog[] {
    return this.data;
  }

  getGroups(): SelectableGroupDialog[] {
    const groupedKeys = this.data.reduce((group: {[key: string]: any[]}, item) => {
      if (!group[item.group.name]) {
       group[item.group.name] = [];
      }
      group[item.group.name].push(item);
      return group;
     }, {});

    return Object.keys(groupedKeys).map((key, index) => {
      let group = {
        isSelected: Object.values(groupedKeys).at(index).every(x => x.isSelected == true),
        name: key,
        students: Object.values(groupedKeys).at(index).length
      }
      
      return group;
    });
  }

  studentChange(index: number): void {
    const selectedStudent = this.students.at(index);
    selectedStudent.isSelected = !selectedStudent.isSelected;

    this.groups.find(x => x.name == selectedStudent.group.name).isSelected = this.data
      .filter(x => x.group.name == selectedStudent.group.name)
      .every(x => x.isSelected == true)
  }

  groupChange(index: number): void {
    const selectedGroup = this.groups.at(index);
    selectedGroup.isSelected = !selectedGroup.isSelected;

    this.students.filter(x => x.group.name == selectedGroup.name).forEach(x => x.isSelected = selectedGroup.isSelected);
  }

  searchValueChange(): void {
    this.students = this.data
      .filter(x => `${x.firstName} ${x.lastName}`.toLowerCase()
      .includes(this.filters.search.toLowerCase()));
  }

  closeDialog(): void {
    var selectedStudents = this.students
      .filter(x => x.isSelected == true)
      .map(x => x.id);

    this.dialogRef.close(selectedStudents);
  }

  fightWithMaterialDialog(): void {
    for (let i =0 ; i < 10; i++){
      var matDialogElement = document.getElementById('mat-dialog-'+i);

      if(!matDialogElement) {
        continue;
      }

      matDialogElement.style.padding = "0";
    }
  }
}
