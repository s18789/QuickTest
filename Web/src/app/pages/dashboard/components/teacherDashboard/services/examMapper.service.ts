import { Injectable } from "@angular/core";
import { CreatedExamResponse, ExamToCheckResponse } from "../models/examResponse.model";
import { CreatedExam, ExamToCheck } from "../models/exam.model";

@Injectable({
    providedIn: "root",
  })
  
export class ExamMapperService {
    constructor() { }

    mapExamToCheckResponseToExamToCheck(examToCheckResponse: ExamToCheckResponse): ExamToCheck {
        return {
        ...examToCheckResponse,
        };
    }

    mapCreatedExamResponseToCreatedExam(createdExamResponse: CreatedExamResponse): CreatedExam {
        return {
            ...createdExamResponse,
        };
    }
}