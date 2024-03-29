import { Entity } from "src/app/shared/utils/models/entity.model";
import { ExamResultStatus } from "../enums/examResultStatus.enum";

export interface ExamResult {
  status?: ExamResultStatus,
  score?: number,
  maxPoints?: number,
  closedQuestionMaxPoints?: number,
  correctOpenQuestions?: number;
  correctClosedQuestions?: number,
  startTime?: Date,
  endTime?: Date,
  percentageResult?: number,
}

export interface ExamsResults {
  lastCompleted: LastCompletedExam,
  studentAverage: number,
  best: number,
  average: number,
  worst: number,
  examsResultsGridItems: ExamResultGridModel[],
}

export interface LastCompletedExam {
  title: string,
  completionDate: Date,
  score: number,
  comparisonToOthers: number,
}

export interface ExamResultGridModel extends Entity {
  examName: string,
  status: ExamResultStatus,
  score?: number,
  endingDate: Date,
}
