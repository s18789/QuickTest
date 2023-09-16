import { Entity } from "src/app/shared/utils/models/entity.model";

export interface Group extends Entity {
  name: string
  studentCount: number,
}

export interface GroupGridModel {
  name: string,
  studentCount: number,
}
