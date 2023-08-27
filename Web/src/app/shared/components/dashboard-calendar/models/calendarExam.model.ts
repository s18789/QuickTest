import { Entity } from "src/app/shared/utils/models/entity.model";

export interface CalendarExam extends Entity{
    title: string,
    dayOfMonth: number,
}