import { QuestionType } from "src/app/shared/enums/questionType.enum";

export interface ExamPreviewResponse {
    title: string,
    questions: QuestionPreviewResponse[],
}

export interface QuestionPreviewResponse {
    questionId: string,
    type: QuestionType,
    content: string,
    answerContent: string | null,
    point: number,
    answers: AnswerPreviewResponse[] | null,
}

export interface AnswerPreviewResponse{
    answerId: string,
    content: string,
    isSelected: boolean | null,
    isCorrect: boolean
}