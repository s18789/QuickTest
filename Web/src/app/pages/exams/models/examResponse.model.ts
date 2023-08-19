import { Entity } from "src/app/shared/utils/models/entity.model"

export interface ExamResponse extends Entity {
  name: string,
  status: string,
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
  status: string,
  finishTime: Date | null,
  score: number | null
}

export interface ExamListItemResponse extends Entity {
  class: string,
  completedExams: string,
  endingDate: string,
  examName: string,
  status: string
}
