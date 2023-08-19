import { Entity } from "src/app/shared/utils/models/entity.model"

export interface Exam extends Entity {
  name: string,
  status: string,
  category: string,
  questionNumber: number,
  availableFrom: Date,
  availableTo: Date,
  time: number,
  completedExams: number,
  examResults: ExamResult[]
}

export interface ExamResult extends Entity {
  fullName: string,
  email: string,
  status: string,
  finishTime: Date | null,
  score: number | null
}

export interface ExamListItem extends Entity {
  class: string,
  completedExams: string,
  endingDate: string,
  examName: string,
  status: string
}
