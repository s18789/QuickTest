import { Entity } from "src/app/shared/utils/models/entity.model";

export interface ExamToResolve extends Entity {
    title: string,
    deadline: Date,
    teacher: Teacher
}

interface Teacher {
    firstName: string,
    lastName: string
}

export interface CompletedExam extends Entity {
    title: string,
    completionDate: Date,
    score: number,
    comparisonToOthers: number
}