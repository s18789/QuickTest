import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Api } from "src/app/shared/utils/api";
import { TeacherResponse } from "../models/teacherResponse.model";

@Injectable({
    providedIn: 'root'
})

export class TeacherService {
    private readonly apiUrl = `${Api.LOCAL_URL}teachers`;

    constructor(
        private http:HttpClient
    ) { }

    getTeachers(): Observable<TeacherResponse[]> {
        return this.http.get<TeacherResponse[]>(this.apiUrl);
    }

    add(teacher: any): Observable<any> {
        return this.http.post(this.apiUrl, teacher);
    }
}