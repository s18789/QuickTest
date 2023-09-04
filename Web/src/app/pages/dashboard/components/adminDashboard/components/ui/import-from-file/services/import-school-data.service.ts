import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ImportSchoolDataResponse } from '../models/importSchoolDataResponse.model';
import { Api } from 'src/app/shared/utils/api';

@Injectable({
  providedIn: 'root'
})
export class ImportSchoolDataService {
  private readonly apiUrl = `${Api.LOCAL_URL}importSummary`;

  constructor(private http: HttpClient) { }

  getImportSummary(importId: string): Observable<ImportSchoolDataResponse> {
    return this.http.get<ImportSchoolDataResponse>(`${this.apiUrl}/import/${importId}`);
  }

}