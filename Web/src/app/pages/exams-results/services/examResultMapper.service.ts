import { Injectable } from "@angular/core";
import { ExamResultGridModelResponse, ExamResultResponse } from "../models/examResultResponse";
import { ExamResult, ExamResultGridModel } from "../models/examResult.model";

@Injectable({
  providedIn: "root",
})

export class ExamResultMapperService {
  constructor() { }

  mapExamResultResponseToExamResult(examResultResponse: ExamResultResponse): ExamResult {
    return {
      ...examResultResponse,
      percentageResult: examResultResponse?.score / examResultResponse?.maxPoints * 100,
    };
  }

  mapExamResultGridModelResponseToExamResultGridModel(examResultGridModelResponse: ExamResultGridModelResponse): ExamResultGridModel {
    return {
      ...examResultGridModelResponse,
    }
  }
}
