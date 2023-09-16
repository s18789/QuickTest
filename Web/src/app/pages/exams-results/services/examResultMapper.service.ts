import { Injectable } from "@angular/core";
import { ExamResultGridModelResponse, ExamResultResponse, ExamsResultsResponse } from "../models/examResultResponse";
import { ExamResult, ExamResultGridModel, ExamsResults } from "../models/examResult.model";
import { ExamResultStatus } from "../enums/examResultStatus.enum";
import { AuthService } from "src/app/core/main/services/auth.service";
import { UserRole } from "src/app/shared/enums/userRole.enum";

@Injectable({
  providedIn: "root",
})

export class ExamResultMapperService {
  constructor(
    private authService: AuthService
  ) { }

  mapExamResultResponseToExamResult(examResultResponse: ExamResultResponse): ExamResult {
    return {
      ...examResultResponse,
      percentageResult: examResultResponse?.score / examResultResponse?.maxPoints * 100,
    };
  }

  mapExamResultGridModelResponseToExamResultGridModel(examsResultsResponse: ExamsResultsResponse): ExamsResults {
    return {
      ...examsResultsResponse,
      examsResultsGridItems: examsResultsResponse.examsResultsGridItems.map((examResultGridItemResponse: ExamResultGridModelResponse) => {
        let examResultGridItem: ExamResultGridModel = {
          ...examResultGridItemResponse,
          id: examResultGridItemResponse.status == ExamResultStatus.ToCheck && this.authService.getUserRole() != UserRole.Teacher
            || examResultGridItemResponse.status == ExamResultStatus.NotResolved && this.authService.getUserRole() != UserRole.Student
            ? null
            : examResultGridItemResponse.id,
        }
        
        return examResultGridItem;
      })
    }
  }
}
