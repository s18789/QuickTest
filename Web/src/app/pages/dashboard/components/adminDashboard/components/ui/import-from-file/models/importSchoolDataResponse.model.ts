import { Student } from "src/app/pages/members/models/students/student.model";
import { Teacher } from "src/app/pages/teachers/models/teacher.model";

export interface ImportSchoolDataResponse {
    IsSuccess: boolean;
    ErrorMessages: string[];
    summary: ImportSummary;
  }
  
  export interface ImportSummary {
    existingTeachers: number;
    existingStudents: number;
    recordsSummary: ExistingRecordsSummary;
    importedGroupsFromFile: ImportedGroupsDto;
  }
  
  export interface ExistingRecordsSummary {
    existingTeacherEmails: string[];
    existingStudentEmails: string[];
    existingGroups: string[];
  }
  
  export interface ImportedGroupsDto {
    importedGroups: Array<[string, Teacher, Student[]]>;
    successfulTeacherLoads: number;  
    failedTeacherLoads: number;  
    successfulStudentLoads: number;  
    failedStudentLoads: number;  
    successfulGroupLoads: number;  
    failedGroupLoads: number; 
    errorList: string[];  
  }