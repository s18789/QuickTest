import { Injectable } from "@angular/core";
import { ExamListItemResponse, ExamResponse } from "../models/examResponse.model";
import { Exam, ExamListItem } from "../models/exam.model";

@Injectable({
  providedIn: "root",
})

export class ExamMapperService {
  constructor() { }

  mapExamResponseToExam(examResponse: ExamResponse): Exam {
    return {
      completedExams: examResponse.examResults.filter(x => x.status == 'Completed').length,
      ...examResponse,
    };
  }

  mapExamListItemResponseToExamListItem(examListItemResponse: ExamListItemResponse): ExamListItem {
    return {
      ...examListItemResponse,
    }
  }
}
