import { Entity } from "src/app/shared/utils/models/entity.model";

export interface Group extends Entity {
  name: string
}

export interface GroupGridModel {
  name: string,
  allStudents: number
}
