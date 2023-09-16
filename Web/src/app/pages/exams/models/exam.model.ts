import { Entity } from "src/app/shared/utils/models/entity.model"
import { ExamResultStatus } from "../../exams-results/enums/examResultStatus.enum"
import { ExamStatus } from "../enums/examStatus.enum"

export interface Exam extends Entity {
  title: string,
  status: ExamStatus,
  questionsCount: number,
  availableFrom: Date,
  availableTo: Date,
  average?: number,
  hardQuestion?: Question,
  examResults: ExamResult[]
}

export interface Question {
  index: number,
  average: number,
}

export interface ExamResult extends Entity {
  fullName: string,
  email: string,
  status: ExamResultStatus,
  finishTime: Date | null,
  score: number | null
}

export interface ExamListItem extends Entity {
  title: string,
  status: ExamStatus,
  completedExams: string,
  availableFrom: Date,
  availableTo: Date,
}