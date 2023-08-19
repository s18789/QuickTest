import { FormArray, FormControl, FormGroup } from "@angular/forms";
import { QuestionType } from "src/app/shared/enums/questionType.enum";

export interface ExamToSolve {
    title: string,
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
    examResultId: string | number,
    questions: QuestionDTO[],
}

export interface QuestionDTO {
    questionId: string | number,
    type: number,
    content: string,
    answerContent: string | null,
    answers: Answer[] | null,
}