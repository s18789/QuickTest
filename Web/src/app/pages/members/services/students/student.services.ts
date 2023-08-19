import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Api } from 'src/app/shared/utils/api';
import { StudentResponse } from '../../models/students/studentResponse.model';

@Injectable({
  providedIn: 'root'
})
export class StudentService {
  private readonly apiUrl = `${Api.LOCAL_URL}students`;

  constructor(
    private http:HttpClient
  ) { }

  getStudents(): Observable<StudentResponse[]> {
    return this.http.get<StudentResponse[]>(this.apiUrl);
  }

  getStudentsForExam(id: string): Observable<StudentResponse[]> {
    return this.http.get<StudentResponse[]>(`${this.apiUrl}/ForExam/${id}`);
  }

  getStudent(id: string): Observable<StudentResponse> {
    return this.http.get<StudentResponse>(`${this.apiUrl}/${id}`);
  }

  add(student: any): Observable<any> {
    return this.http.post(this.apiUrl, student);
  }

  update(student: any): Observable<any> {
    return this.http.put(this.apiUrl, student);
  }

  remove(id: string): Observable<any> {
    return this.http.delete(`${Api.DATA_STUDENTS}/${id}`)
  }
}
