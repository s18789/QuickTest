import { Entity } from "src/app/shared/utils/models/entity.model";

export interface GroupResponse extends Entity {
  name: string
  studentCount: number,
}
