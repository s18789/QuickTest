import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { AuthService } from 'src/app/core/main/services/auth.service';
import { Group } from '../../models/group.model';
import { GroupFormGroup } from '../../models/groupFormGroup';
import { GroupsService } from '../../services/groups.service';

@Component({
  selector: 'app-add-group',
  templateUrl: './add-group.component.html',
  styleUrls: ['./add-group.component.css']
})
export class AddGroupComponent implements OnInit {
  groupForm!: FormGroup<GroupFormGroup>;

  constructor(
    private authService: AuthService,
    private groupsService: GroupsService,
    public dialogRef: MatDialogRef<AddGroupComponent>,
    @Inject(MAT_DIALOG_DATA) public data: Group
  ) { }

  ngOnInit(): void {
    this.groupForm = new FormGroup<GroupFormGroup>({
      name: new FormControl(this.data.name, [Validators.required, Validators.minLength(2), Validators.maxLength(100)]),
    });
  }

  onSubmit(form: any) {
    if (!form.valid || !this.authService.access) {
      return;
    }

    if (!!this.data.id) {
      this.groupsService.update({ id: this.data.id, name: form.value.name }).subscribe();
    } else {
      this.groupsService.add(form.value).subscribe();
    }

    this.dialogRef.close();
  }
}
