import { Entity } from "src/app/shared/utils/models/entity.model";
import { ExamResultStatus } from "../enums/examResultStatus.enum";

export interface ExamResultResponse {
  status?: ExamResultStatus,
  score?: number,
  maxPoints?: number,
  closedQuestionMaxPoints?: number,
  correctOpenQuestions?: number;
  correctClosedQuestions?: number,
  startTime?: Date,
  endTime?: Date
}

export interface ExamsResultsResponse {
  lastCompleted: LastCompletedExamResponse,
  studentAverage: number,
  best: number,
  average: number,
  worst: number,
  examsResultsGridItems: ExamResultGridModelResponse[],
}

export interface LastCompletedExamResponse {
  title: string,
  completionDate: Date,
  score: number,
  comparisonToOthers: number,
}

export interface ExamResultGridModelResponse extends Entity {
  examName: string,
  status: ExamResultStatus,
  score?: number,
  endingDate: Date,
}
