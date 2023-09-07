export class CreatedAccountsSummary {
    TeachersCreated: number;
    StudentsCreated: number;
    TeacherCreationFailed: number;
    StudentCreationFailed: number;
    GroupsCreated: number;
    GroupsFailed: number;
    ErrorList: string[];
    IsSuccess: boolean;
  
    constructor() {
      this.TeachersCreated = 0;
      this.StudentsCreated = 0;
      this.TeacherCreationFailed = 0;
      this.StudentCreationFailed = 0;
      this.GroupsCreated = 0;
      this.GroupsFailed = 0;
      this.ErrorList = [];
      this.IsSuccess = false;
    }
  }