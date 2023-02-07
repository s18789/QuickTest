import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Api } from 'src/app/shared/utils/api';
import { ExamDTO } from 'src/app/shared/utils/models/examModel';
import { HttpServiceModel } from 'src/app/shared/utils/models/httpServiceModel';

@Injectable({
  providedIn: 'root'
})
export class ExamsService implements HttpServiceModel {
  private readonly apiUrl = `${Api.LOCAL_URL}exams`;

  constructor(
    private http:HttpClient
  ) { }

  fetch(filters?: { [key: string]: any; } | undefined): Observable<any> {
    return this.http.get(this.apiUrl, { params: filters })
  }

  get(id: string): Observable<any> {
    return this.http.get(`${this.apiUrl}/${id}`)
  }

  add(exam: any): Observable<any> {
    return this.http.post(this.apiUrl, exam);
  }

  update(item: any): Observable<any> {
    throw new Error('Method not implemented.');
  }

  remove(id: string): Observable<any> {
    return this.http.delete(`${Api.DATA_EXAMS}/${id}`)
  }
}
