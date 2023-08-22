import { Entity } from "src/app/shared/utils/models/entity.model";

export interface TeacherResponse extends Entity {
    firstName: string,
    lastName: string,
    email: string,
  }