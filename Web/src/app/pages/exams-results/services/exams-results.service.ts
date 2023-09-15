import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Api } from 'src/app/shared/utils/api';
import { ExamResultDto } from '../../../core/main/components/exam-to-solve/models/examResultDto.model';
import { ExamResult } from '../models/examResult.model';
import { ExamResultGridModelResponse, ExamsResultsResponse } from '../models/examResultResponse';
import { ExamPreviewResponse } from 'src/app/shared/components/exam-preview/models/examPreviewResponse.model';
import { CheckedExam } from 'src/app/shared/components/exam-preview/models/examPreview.model';
import { CompletedExamResponse, ExamToResolveResponse } from '../../dashboard/components/studentDashboard/models/examResponse.model';
import { ExamToCheckResponse } from '../../dashboard/components/teacherDashboard/models/examResponse.model';
import { CalendarExamResponse } from 'src/app/shared/components/dashboard-calendar/models/calendarExamResponse.model';
import { ExamHeaderResponse } from '../../calendar/models/examResponse.model';
import { ScheduleExamResponse } from '../../dashboard/components/ui/scheduled-exams/models/scheduleExam.model';

@Injectable({
  providedIn: 'root'
})
export class ExamsResultsService {
  private readonly apiUrl = `${Api.LOCAL_URL}examsResults`;

  constructor(
    private http:HttpClient
  ) { }

  fetch(filters?: { [key: string]: any; } | undefined): Observable<any> {
    var userId = <string>localStorage.getItem('userId');
    var params = new HttpParams().set("studentId", userId);
    return this.http.get(this.apiUrl, { params: params })
  }

  getExamsResults(studentId?: string): Observable<ExamsResultsResponse> {
    var userId = studentId
      ? studentId
      : <string>localStorage.getItem('userId');

    var params = new HttpParams().set("studentId", userId);
    return this.http.get<ExamsResultsResponse>(this.apiUrl, { params: params })
  }

  get(id: string): Observable<ExamResult> {
    return this.http.get<ExamResult>(`${this.apiUrl}/${id}`)
  }

  startExam(examResultId: number): Observable<any> {
    var params = new HttpParams().set("examResultId", examResultId);
    return this.http.get<ExamResultDto>(`${this.apiUrl}/StartExam`, { params: params });
  }

  getExamsToResolve(): Observable<ExamToResolveResponse[]> {
    return this.http.get<ExamToResolveResponse[]>(`${this.apiUrl}/ExamsResultsToResolve`);
  }

  getCompletedExams(): Observable<CompletedExamResponse[]> {
    return this.http.get<CompletedExamResponse[]>(`${this.apiUrl}/CompletedExamsResults`);
  }

  getExamsToCheck(): Observable<ExamToCheckResponse[]> {
    return this.http.get<ExamToCheckResponse[]>(`${this.apiUrl}/ExamsResultsToCheck`);
  }

  getCalendarExamsResults(month: number, year: number): Observable<CalendarExamResponse[]> {
    return this.http.get<CalendarExamResponse[]>(`${this.apiUrl}/CalendarExamsResults/${month}/${year}`);
  }

  getExamsResultsHeader(month: number, year: number): Observable<ExamHeaderResponse[]> {
    return this.http.get<ExamHeaderResponse[]>(`${this.apiUrl}/ExamsResultsHeader/${month}/${year}`);
  }

  getScheduleExams(): Observable<ScheduleExamResponse[]> {
    return this.http.get<ScheduleExamResponse[]>(`${this.apiUrl}/ScheduleExams`);
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

  GetExamResultPreview(id: string): Observable<ExamPreviewResponse> {
    return this.http.get<ExamPreviewResponse>(`${this.apiUrl}/Preview/${id}`)
  }

  checkExamResult(checkedExam: CheckedExam): Observable<any> {
    return this.http.post(`${this.apiUrl}/CheckExam`, checkedExam);
  }
}
