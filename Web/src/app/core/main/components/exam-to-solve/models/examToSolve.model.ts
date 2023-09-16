import { FormArray, FormControl, FormGroup } from "@angular/forms";
import { QuestionType } from "src/app/shared/enums/questionType.enum";

export interface ExamToSolve {
    title: string,
    startDate: Date,
    finishDate: Date,
    questions: Question[],
}

export interface Question {
    questionId: string,
    type: QuestionType,
    content: string,
    answerContent: string | null,
    answers: Answer[] | null,
}

export interface Answer {
    answerId: string | number,
    content: string,
    isSelected: boolean | null
}

export interface ExamToSolveForm {
    title: FormControl,
    questions: FormArray<FormGroup>,
}

export interface ResolvedExamDTO {
    examResultId: string,
    questions: QuestionDTO[],
}

export interface QuestionDTO {
    questionId: string,
    type: number,
    content: string,
    answerContent: string | null,
    answers: Answer[] | null,
}