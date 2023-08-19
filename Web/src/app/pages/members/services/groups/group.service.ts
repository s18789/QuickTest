import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Api } from 'src/app/shared/utils/api';
import { GroupResponse } from '../../models/groups/groupResponse.model';

@Injectable({
  providedIn: 'root'
})

export class GroupService {
  private readonly apiUrl = `${Api.LOCAL_URL}groups`;

  constructor(
    private http:HttpClient
  ) { }

  getGroups(): Observable<GroupResponse[]> {
    return this.http.get<GroupResponse[]>(this.apiUrl)
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
