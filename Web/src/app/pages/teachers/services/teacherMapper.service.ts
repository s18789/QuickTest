import { Injectable } from "@angular/core";
import { Teacher } from "../models/teacher.model";
import { TeacherResponse } from "../models/teacherResponse.model";

@Injectable({
  providedIn: "root",
})

export class TeacherMapperService {
  constructor() { }

  mapTeacherResponseToTeacher(teacherResponse: TeacherResponse): Teacher {
    return {
      ...teacherResponse,
    };
  }
}