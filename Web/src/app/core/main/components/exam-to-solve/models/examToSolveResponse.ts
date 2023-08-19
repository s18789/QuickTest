import { QuestionType } from "src/app/shared/enums/questionType.enum";

export interface ExamToSolveResponse {
    examResultId: string,
    title: string,
    questions: QuestionResponse[],
}

export interface QuestionResponse {
    questionId: string,
    type: QuestionType,
    content: string,
    answers: AnswerResponse[] | null,
}

export interface AnswerResponse {
    answerId: string,
    content: string
}