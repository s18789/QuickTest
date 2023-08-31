import { Entity } from "src/app/shared/utils/models/entity.model";

export interface ScheduleExamResponse extends Entity {
    date: Date,
    title: string,
    subject: string
}

export interface GroupScheduleExams {
    date: Date,
    exams: GroupScheduleExam [],
}

export interface GroupScheduleExam extends Entity {
    date: Date,
    title: string,
    subject: string,
}