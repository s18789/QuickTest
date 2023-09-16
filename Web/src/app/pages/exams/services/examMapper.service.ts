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
      ...examResponse,
    };
  }

  mapExamListItemResponseToExamListItem(examListItemResponse: ExamListItemResponse): ExamListItem {
    return {
      ...examListItemResponse,
      completedExams: `${examListItemResponse.completedExams} / ${examListItemResponse.allExams}`,
    }
  }
}
