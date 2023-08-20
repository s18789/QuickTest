import { FormArray, FormControl, FormGroup } from "@angular/forms";
import { QuestionType } from "src/app/shared/enums/questionType.enum";

export interface ExamPreview {
    examResultId: string,
    title: string,
    questions: QuestionPreview[],
}

export interface QuestionPreview {
    questionId: string,
    type: QuestionType,
    content: string,
    points: number,
    score: number,
    answerContent: string | null,
    answers: AnswerPreview[] | null,
}

export interface AnswerPreview {
    answerId: string,
    content: string,
    isSelected: boolean | null,
    isCorrect: boolean
}

export interface ExamPreviewForm {
    title: FormControl,
    questions: FormArray<FormGroup>,
}

export interface CheckedExam {
    examResultId: string,
    questions: CheckedQuestion[],
}

export interface CheckedQuestion {
    questionId: string,
    type: QuestionType,
    score: number,
}