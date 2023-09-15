import { Entity } from "src/app/shared/utils/models/entity.model"
import { ExamResultStatus } from "../../exams-results/enums/examResultStatus.enum"
import { ExamStatus } from "../enums/examStatus.enum"

export interface ExamResponse extends Entity {
  title: string,
  status: ExamStatus,
  questionsCount: number,
  availableFrom: Date,
  availableTo: Date,
  average?: number,
  hardQuestion?: QuestionResponse,
  examResults: ExamResultResponse[]
}

export interface QuestionResponse {
  index: number,
  average: number,
}

export interface ExamResultResponse extends Entity {
  fullName: string,
  email: string,
  status: ExamResultStatus,
  finishTime: Date | null,
  score: number | null
}

export interface ExamListItemResponse extends Entity {
  title: string,
  status: ExamStatus,
  completedExams: number,
  allExams: number,
  availableFrom: Date,
  availableTo: Date,
}
