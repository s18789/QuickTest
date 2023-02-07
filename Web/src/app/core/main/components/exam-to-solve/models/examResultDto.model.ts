export interface ExamResultDto {
  examId: number,
  examResultId: number,
  questions: QuestionDto[]
}

export interface QuestionDto {
  questionId: number,
  content?: string,
  answers: AnswerDto[]
}

export interface AnswerDto {
  answerId: number,
  content?: string,
  isSelected: boolean
}

