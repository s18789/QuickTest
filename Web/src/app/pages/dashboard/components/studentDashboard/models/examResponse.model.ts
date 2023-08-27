import { Entity } from "src/app/shared/utils/models/entity.model"

export interface ExamToResolveResponse extends Entity {
    title: string,
    deadline: Date,
    teacher: Teacher
}

interface Teacher {
    firstName: string,
    lastName: string
}

export interface CompletedExamResponse extends Entity {
    title: string,
    completionDate: Date,
    score: number,
    comparisonToOthers: number
}