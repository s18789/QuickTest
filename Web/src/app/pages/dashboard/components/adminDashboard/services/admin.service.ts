import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Api } from "src/app/shared/utils/api";
import { Observable } from 'rxjs';
import { BulkImportRequest } from "../components/ui/import-from-file/models/bulkImportRequest.model";

@Injectable({
    providedIn: 'root'
  })
  export class AdminService {
    private readonly apiUrl = `${Api.LOCAL_URL}admin`;
  
    constructor(
      private http:HttpClient
    ) { }
  
    importSchoolData(file: File): Observable<any> {
      const formData: FormData = new FormData();
      formData.append('file', file, file.name);
      return this.http.post(`${this.apiUrl}/import`, formData);
    }

    getImportSummary(importId: string): Observable<any> {
      return this.http.get(`${this.apiUrl}/import/${importId}`);
    }
    bulkImport(request: BulkImportRequest): Observable<any> {
      return this.http.post(`${this.apiUrl}/bulk-import`, request);
    }
    clearCache(importId: string): Observable<any> {
      return this.http.delete(`${this.apiUrl}/clearCache/${importId}`);
    }

  }






  
    // getExams(): Observable<ExamListItemResponse[]> {
    //   return this.http.get<ExamListItemResponse[]>(this.apiUrl)
    // }
  
    // getExam(id: string): Observable<any> {
    //   return this.http.get(`${this.apiUrl}/${id}`)
    // }
  
    // add(exam: any): Observable<any> {
    //   return this.http.post(this.apiUrl, exam);
    // }
  
    // addExamMembers(newMembers: { examId: string, membersIds: string[]}): Observable<any>{
    //   return this.http.post(`${this.apiUrl}/addExamMembers`, newMembers);
    // }
  
    // update(item: any): Observable<any> {
    //   throw new Error('Method not implemented.');
    // }
  
    // remove(id: string): Observable<any> {
    //   return this.http.delete(`${Api.DATA_EXAMS}/${id}`)
    // }
  
    // finishExam(examId: string) {
    //   return this.http.post(`${this.apiUrl}/FinishExam`, examId);
    // }
  
    // GetCreatedExamPreview(examId: string): Observable<ExamPreviewResponse> {
    //   return this.http.get<ExamPreviewResponse>(`${this.apiUrl}/Preview/${examId}`);
    // }