export interface ExamResult {
  status?: string,
  maxPoints?: number,
  score?: number,
  questionCount?: number,
  correctAnswers?: number;
  wrongAnswers?: number,
  startTime?: Date,
  endTime?: Date
}
