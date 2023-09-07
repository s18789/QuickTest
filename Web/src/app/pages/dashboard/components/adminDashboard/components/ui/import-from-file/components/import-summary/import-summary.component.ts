import { Component, OnInit } from '@angular/core';
import { ImportSchoolDataService } from '../../services/import-school-data.service';
import { ImportSchoolDataResponse, ImportSummary } from '../../models/importSchoolDataResponse.model';
import { LoaderService } from 'src/app/shared/services/loaderService.service';
import { ImportIdService } from '../../services/import-id.service';
import { ActivatedRoute } from '@angular/router';
import { AdminService } from 'src/app/pages/dashboard/components/adminDashboard/services/admin.service'
import { Observable } from 'rxjs';
import { Router } from '@angular/router';
import { BulkImportRequest } from '../../models/bulkImportRequest.model';
import { CreatedAccountsSummaryService } from '../../services/created-accounts-summary.service'; 


@Component({
  selector: 'app-import-summary',
  templateUrl: './import-summary.component.html',
  styleUrls: ['./import-summary.component.css']
})
export class ImportSummaryComponent implements OnInit {
  importSummary$: Observable<any>;
  importSummary: ImportSummary;
  importId: string;

  constructor(
    private importSchoolDataService: ImportSchoolDataService,
    private loaderService: LoaderService,
    private importIdService: ImportIdService,
    private route: ActivatedRoute,
    private adminService: AdminService, 
    private router: Router,
    private summaryService: CreatedAccountsSummaryService
  ) { }

  ngOnInit(): void {
    const importId = this.route.snapshot.paramMap.get('importId');
    if (importId) {
      this.getImportSummary(importId);
    }
  }
  calculateSuccessRate(successful: number, failed: number): number {
    if (successful === undefined || failed === undefined) {
      return 0;
    }
  
    const total = successful + failed;
  
    if (total === 0) {
      return 0;
    }
  
    return total;
  }

  getImportSummary(importId: string): void {
    this.loaderService.show();
    this.importSummary$ = this.adminService.getImportSummary(importId);
    
    //debugger;
    this.importSummary$.subscribe(
      (data) => {
        this.loaderService.hide();
        this.importSummary = data;
      },
      (error) => {
        console.error('Error fetching import summary:', error);
        this.loaderService.hide();
      }
    );
  }
  onFileUploadSuccess(response: any): void {
    this.importIdService.setImportId(response.ImportId);
  }

  createAccounts(): void {
    const schoolIdString = localStorage.getItem('schoolId');
    const schoolId = schoolIdString ? parseInt(schoolIdString, 10) : null;
    console.log(schoolId);
    //debugger;
    
    const bulkImportRequest: BulkImportRequest = {
      ImportSummary: {
        ExistingTeachers: this.importSummary.existingTeachers, 
        ExistingStudents: this.importSummary.existingStudents, 
        RecordsSummary: this.importSummary.recordsSummary, 
        ImportedGroupsFromFile: this.importSummary.importedGroupsFromFile 
      },
      SchoolId: schoolId 
    };
    
    this.adminService.bulkImport(bulkImportRequest).subscribe(
      (response: any) => {
        if (response.IsSuccess) {
          console.log('Bulk import successful:', response);
          this.summaryService.setSummary(response); 
          this.router.navigate(['create-accounts-summary/create-accounts-summary']); 
        } else {
          console.error('Bulk import failed:', response.ErrorList);
          this.summaryService.setSummary(response); 
          this.router.navigate(['create-accounts-summary/create-accounts-summary']); 
        }
      },
      (error: any) => {
        console.error('An error occurred during bulk import:', error);
      }
    );
  }

  goBackToDashboard(): void {
   // this.adminService.clearCache(importId).subscribe(() => {
      this.router.navigate(['/dashboard']);
  //});
  }
}