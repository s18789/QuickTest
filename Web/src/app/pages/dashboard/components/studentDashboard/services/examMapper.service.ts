import { Injectable } from "@angular/core";
import { CompletedExamResponse, ExamToResolveResponse } from "../models/examResponse.model";
import { CompletedExam, ExamToResolve } from "../models/exam.model";

@Injectable({
    providedIn: "root",
  })
  
export class ExamMapperService {
    constructor() { }

    mapExamToResolveResponseToExamToResolve(examToResolveResponse: ExamToResolveResponse): ExamToResolve {
        return {
        ...examToResolveResponse,
        };
    }

    mapCompletedExamResponseToCompletedExam(completedExamResponse: CompletedExamResponse): CompletedExam {
        return {
            ...completedExamResponse,
        };
    }
}