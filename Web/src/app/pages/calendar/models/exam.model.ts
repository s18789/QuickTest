import { Entity } from "src/app/shared/utils/models/entity.model";

export interface ExamHeader extends Entity {
    title: string,
    availableFrom: Date,
    availableTo: Date,
}