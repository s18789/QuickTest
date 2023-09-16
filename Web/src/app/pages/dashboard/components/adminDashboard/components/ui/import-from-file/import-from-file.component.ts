import { Component,EventEmitter, OnInit, Output } from '@angular/core';
import { AdminService } from '../../../services/admin.service';
import { Api } from "src/app/shared/utils/api";
import { HttpClient, HttpEventType, HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-import-from-file',
  templateUrl: './import-from-file.component.html',
  styleUrls: ['./import-from-file.component.css']
})
export class ImportFromFileComponent implements OnInit {
  private readonly apiUrl = `${Api.LOCAL_URL}admin`;
  progress: number;
  message: string;
  @Output() public onUploadFinished = new EventEmitter();

  constructor(
    private adminService: AdminService,
    private http: HttpClient,
    private router: Router
    ) { }

  ngOnInit(): void {
  }

  
  importFile(event: any): void {
    const file = event.target.files[0];
    if (file.length === 0)
    return;
    const formData: FormData = new FormData();
    formData.append('file', file, file.name);
    if (file) {
      this.http.post(`${this.apiUrl}/import`, formData, {
        reportProgress: true,
        observe: 'events'
      })
        .subscribe({
        next: (httpEvent) => {
        if (httpEvent.type === HttpEventType.UploadProgress)
          this.progress = Math.round(100 * httpEvent.loaded / httpEvent.total);
        else if (httpEvent.type === HttpEventType.Response) {
          this.message = 'Upload success.';
          this.onUploadFinished.emit(httpEvent.body);

          const importId = (httpEvent.body as any)?.importId;
          
        if (importId) {
          this.router.navigate([`/import-summary/${importId}`]); 
        }
          //this.router.navigate(["/import-summary/${importId}"]);
        }
      },
      error: (err: HttpErrorResponse) => console.log(err)
    });
    
    }
  }

}
