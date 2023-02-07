import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Api } from 'src/app/shared/utils/api';
import { ExamResultDto } from '../components/exam-to-solve/models/examResultDto.model';

@Injectable({
  providedIn: 'root'
})
export class ExamSolveService {
  private readonly apiUrl = `${Api.LOCAL_URL}examsResults`;

  public isExamResolving: boolean = false;

  constructor(
    private http:HttpClient
  ) { }

  startExam(examResultId: number): Observable<any> {
    this.isExamResolving = true;
    var params = new HttpParams().set("examResultId", examResultId);
    return this.http.get<ExamResultDto>(`${this.apiUrl}/StartExam`, { params: params });
  }

  finishExam(examResult: ExamResultDto): Observable<any> {
    this.isExamResolving = false;
    return this.http.post(`${this.apiUrl}/FinishExam`, examResult);
  }
}
