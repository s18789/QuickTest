import { Entity } from "src/app/shared/utils/models/entity.model";

export interface ExamResult {
  status?: string,
  maxPoints?: number,
  score?: number,
  questionCount?: number,
  correctAnswers?: number;
  wrongAnswers?: number,
  startTime?: Date,
  endTime?: Date,
  percentageResult?: number,
}

export interface ExamResultGridModel extends Entity {
  examName: string,
  status: string,
  score?: number,
  endingDate: Date,
}
