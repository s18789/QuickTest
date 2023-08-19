import { Entity } from "src/app/shared/utils/models/entity.model";

export interface ExamResultResponse {
  status?: string,
  maxPoints?: number,
  score?: number,
  questionCount?: number,
  correctAnswers?: number;
  wrongAnswers?: number,
  startTime?: Date,
  endTime?: Date
}

export interface ExamResultGridModelResponse extends Entity {
  examName: string,
  status: string,
  score?: number,
  endingDate: Date,
}
