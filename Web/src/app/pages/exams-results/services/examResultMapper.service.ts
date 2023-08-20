import { Injectable } from "@angular/core";
import { ExamResultGridModelResponse, ExamResultResponse } from "../models/examResultResponse";
import { ExamResult, ExamResultGridModel } from "../models/examResult.model";
import { ExamResultStatus } from "../enums/examResultStatus.enum";
import { AuthService } from "src/app/core/main/services/auth.service";

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

  mapExamResultGridModelResponseToExamResultGridModel(examResultGridModelResponse: ExamResultGridModelResponse): ExamResultGridModel {
    return {
      ...examResultGridModelResponse,
      id: examResultGridModelResponse.status == ExamResultStatus.ToCheck && !this.authService.isUserTeacher()
        ? null
        : examResultGridModelResponse.id,
    }
  }
}
