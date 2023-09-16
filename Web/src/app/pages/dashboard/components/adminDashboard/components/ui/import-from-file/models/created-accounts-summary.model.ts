export class CreatedAccountsSummary {
    teachersCreated: number;
    studentsCreated: number;
    teacherCreationFailed: number;
    studentCreationFailed: number;
    groupsCreated: number;
    groupsFailed: number;
    errorList: string[];
    isSuccess: boolean;
  
    constructor() {
      this.teachersCreated = 0;
      this.studentsCreated = 0;
      this.teacherCreationFailed = 0;
      this.studentCreationFailed = 0;
      this.groupsCreated = 0;
      this.groupsFailed = 0;
      this.errorList = [];
      this.isSuccess = false;
    }
  }