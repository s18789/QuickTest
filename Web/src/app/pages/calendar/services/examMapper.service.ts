import { Injectable } from "@angular/core";
import { ExamHeaderResponse } from "../models/examResponse.model";
import { ExamHeader } from "../models/exam.model";

@Injectable({
    providedIn: "root",
  })
  
  export class ExamMapperService {
    constructor() { }
  
    mapExamHeaderResponseToExamHeader(examHeaderResponse: ExamHeaderResponse): ExamHeader {
      return {
        ...examHeaderResponse,
      };
    }
  
  }