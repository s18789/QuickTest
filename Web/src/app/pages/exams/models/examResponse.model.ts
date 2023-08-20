import { Entity } from "src/app/shared/utils/models/entity.model"
import { ExamResultStatus } from "../../exams-results/enums/examResultStatus.enum"
import { ExamStatus } from "../enums/examStatus.enum"

export interface ExamResponse extends Entity {
  name: string,
  status: ExamStatus,
  category: string,
  questionNumber: number,
  availableFrom: Date,
  availableTo: Date,
  time: number,
  examResults: ExamResultResponse[]
}

export interface ExamResultResponse extends Entity {
  fullName: string,
  email: string,
  status: ExamResultStatus,
  finishTime: Date | null,
  score: number | null
}

export interface ExamListItemResponse extends Entity {
  class: string,
  completedExams: string,
  endingDate: string,
  examName: string,
  status: ExamStatus
}
