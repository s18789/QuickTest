import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Api } from 'src/app/shared/utils/api';
import { ExamListItemResponse } from '../models/examResponse.model';
import { ExamPreviewResponse } from 'src/app/shared/components/exam-preview/models/examPreviewResponse.model';
import { CalendarExamResponse } from '../../../shared/components/dashboard-calendar/models/calendarExamResponse.model';
import { CreatedExamResponse } from '../../dashboard/components/teacherDashboard/models/examResponse.model';
import { ExamHeaderResponse } from '../../calendar/models/examResponse.model';

@Injectable({
  providedIn: 'root'
})
export class ExamsService {
  private readonly apiUrl = `${Api.LOCAL_URL}exams`;

  constructor(
    private http:HttpClient
  ) { }

  getExams(): Observable<ExamListItemResponse[]> {
    return this.http.get<ExamListItemResponse[]>(this.apiUrl)
  }

  getExam(id: string): Observable<any> {
    return this.http.get(`${this.apiUrl}/${id}`)
  }

  getCreatedExams(): Observable<CreatedExamResponse[]> {
    return this.http.get<CreatedExamResponse[]>(`${this.apiUrl}/CreatedExams`);
  }

  getCalendarExams(month: number, year: number): Observable<CalendarExamResponse[]> {
    return this.http.get<CalendarExamResponse[]>(`${this.apiUrl}/CalendarExams/${month}/${year}`);
  }

  getExamsHeader(month: number, year: number): Observable<ExamHeaderResponse[]> {
    return this.http.get<ExamHeaderResponse[]>(`${this.apiUrl}/ExamsHeader/${month}/${year}`);
  }

  add(exam: any): Observable<any> {
    return this.http.post(this.apiUrl, exam);
  }

  addExamMembers(newMembers: { examId: string, membersIds: string[]}): Observable<any>{
    return this.http.post(`${this.apiUrl}/addExamMembers`, newMembers);
  }

  update(item: any): Observable<any> {
    throw new Error('Method not implemented.');
  }

  remove(id: string): Observable<any> {
    return this.http.delete(`${Api.DATA_EXAMS}/${id}`)
  }

  finishExam(examId: string) {
    return this.http.post(`${this.apiUrl}/FinishExam`, examId);
  }

  GetCreatedExamPreview(examId: string): Observable<ExamPreviewResponse> {
    return this.http.get<ExamPreviewResponse>(`${this.apiUrl}/Preview/${examId}`);
  }
}
