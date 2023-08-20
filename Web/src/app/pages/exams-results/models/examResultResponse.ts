import { Entity } from "src/app/shared/utils/models/entity.model";
import { ExamResultStatus } from "../enums/examResultStatus.enum";

export interface ExamResultResponse {
  status?: ExamResultStatus,
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
  status: ExamResultStatus,
  score?: number,
  endingDate: Date,
}
