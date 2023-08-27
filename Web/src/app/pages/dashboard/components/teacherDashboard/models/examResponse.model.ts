import { Entity } from "src/app/shared/utils/models/entity.model"

export interface ExamToCheckResponse extends Entity {
    title: string,
    completionDate: Date,
    student: Student
}

interface Student {
    firstName: string,
    lastName: string
}

export interface CreatedExamResponse extends Entity {
    title: string,
    validTo: Date,
    average: number,
    completedExams: number,
    allExams: number,
}