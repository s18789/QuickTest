import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Api } from 'src/app/shared/utils/api';
import { HttpServiceModel } from 'src/app/shared/utils/models/httpServiceModel';

@Injectable({
  providedIn: 'root'
})

export class GroupsService implements HttpServiceModel{
  private readonly apiUrl = `${Api.LOCAL_URL}groups`;

  constructor(
    private http:HttpClient
  ) { }

  fetch(filters?: { [key: string]: any; } | undefined): Observable<any> {
    return this.http.get(this.apiUrl, { params: filters })
  }

  get(id: string): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/${id}`);
  }

  add(group: any): Observable<any> {
    return this.http.post(this.apiUrl, group);
  }

  update(group: any): Observable<any> {
    return this.http.put(this.apiUrl, group);
  }

  remove(id: string): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`)
  }
}
