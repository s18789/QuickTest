export interface Exam {
  id: number,
  name: string,
  status: string,
  category: string,
  questionNumber: number,
  availableFrom: Date,
  availableTo: Date,
  time: number,
  examResults: ExamResult[]
}

export interface ExamResult {
  id: number,
  fullName: string,
  email: string,
  status: string,
  finishTime: Date | null,
  score: number | null
}
