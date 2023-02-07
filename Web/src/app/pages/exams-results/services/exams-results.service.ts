import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Api } from 'src/app/shared/utils/api';
import { HttpServiceModel } from 'src/app/shared/utils/models/httpServiceModel';
import { ExamResultDto } from '../../../core/main/components/exam-to-solve/models/examResultDto.model';
import { ExamResult } from '../models/examResult.model';

@Injectable({
  providedIn: 'root'
})
export class ExamsResultsService implements HttpServiceModel {
  private readonly apiUrl = `${Api.LOCAL_URL}examsResults`;

  constructor(
    private http:HttpClient
  ) { }

  fetch(filters?: { [key: string]: any; } | undefined): Observable<any> {
    var userId = <string>localStorage.getItem('userId');
    var params = new HttpParams().set("studentId", userId);
    return this.http.get(this.apiUrl, { params: params })
  }

  get(id: string): Observable<ExamResult> {
    return this.http.get<ExamResult>(`${this.apiUrl}/${id}`)
  }

  getStatus(examResultId: string): any {
    var params = new HttpParams().set("examResultId", examResultId);

    return this.http.get(`${this.apiUrl}/GetStatus`, { params: params });
  }

  startExam(examResultId: number): Observable<any> {
    var params = new HttpParams().set("examResultId", examResultId);
    return this.http.get<ExamResultDto>(`${this.apiUrl}/StartExam`, { params: params });
  }

  add(examResult: ExamResultDto): Observable<any> {
    return this.http.post(`${this.apiUrl}/FinishExam`, examResult);
  }

  update(item: any): Observable<any> {
    throw new Error('Method not implemented.');
  }

  remove(id: string): Observable<any> {
    return this.http.delete(`${Api.DATA_EXAMS}/${id}`)
  }
}
