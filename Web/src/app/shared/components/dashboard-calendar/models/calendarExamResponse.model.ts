import { Entity } from "src/app/shared/utils/models/entity.model";

export interface CalendarExamResponse extends Entity {
    title: string,
    dayOfMonth: number,
}