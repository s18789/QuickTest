import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Api } from 'src/app/shared/utils/api';
import { ResolvedExamDTO } from '../components/exam-to-solve/models/examToSolve.model';
import { ExamToSolveResponse } from '../components/exam-to-solve/models/examToSolveResponse';

@Injectable({
  providedIn: 'root'
})
export class ExamSolveService {
  private readonly apiUrl = `${Api.LOCAL_URL}examsResults`;

  public isExamResolving: boolean = localStorage.getItem("isExamResolving") == "true" ? true : false;
  public examResultId: string = localStorage.getItem("examResultId");

  constructor(
    private http:HttpClient
  ) { }

  prepareExam(examResultId: string) {
    localStorage.setItem("isExamResolving", "true");
    localStorage.setItem("examResultId", examResultId);
    this.isExamResolving = true;
    this.examResultId = examResultId;
  }

  startExam(): Observable<ExamToSolveResponse> {
    var params = new HttpParams().set("examResultId", this.examResultId);
    return this.http.get<ExamToSolveResponse>(`${this.apiUrl}/StartExam`, { params: params });
  }

  finishExam(examResult: ResolvedExamDTO): Observable<any> {
    return this.http.post(`${this.apiUrl}/FinishExam`, examResult);
  }

  removeStorage() {
    localStorage.removeItem("isExamResolving");
    localStorage.removeItem("examResultId");
    this.isExamResolving = false;
    this.examResultId = null;
  }
}
