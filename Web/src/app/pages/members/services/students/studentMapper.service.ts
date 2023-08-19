import { Injectable } from "@angular/core";
import { Student } from "../../models/students/student.model";
import { StudentResponse } from "../../models/students/studentResponse.model";
import { StudentDialog } from "../../models/students/studentDialog.model";

@Injectable({
  providedIn: "root",
})

export class StudentMapperService {
  constructor() { }

  mapStudentResponseToStudent(studentResponse: StudentResponse): Student {
    return {
      ...studentResponse,
    };
  }

  mapStudentResponseToStudentDialog(studentResponse: StudentResponse): StudentDialog {
    return {
      ...studentResponse,
      isSelected: false,
    };
  }
}
