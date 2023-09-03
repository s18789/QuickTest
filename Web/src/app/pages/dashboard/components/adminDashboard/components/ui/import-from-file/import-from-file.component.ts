import { Component, OnInit } from '@angular/core';
import { AdminService } from '../../../services/admin.service';

@Component({
  selector: 'app-import-from-file',
  templateUrl: './import-from-file.component.html',
  styleUrls: ['./import-from-file.component.css']
})
export class ImportFromFileComponent implements OnInit {

  constructor(
    private adminService: AdminService,
    ) { }

  ngOnInit(): void {
  }

  importFile(event: any){
    const file = event.target.files[0];

    let fileToUpload = <File>file;
    const formData = new FormData();
    formData.append('file', fileToUpload, fileToUpload.name);

    this.adminService.import(formData).subscribe();
  }

}
