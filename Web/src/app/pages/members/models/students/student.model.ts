import { Entity } from "src/app/shared/utils/models/entity.model";
import { Group } from "../groups/group.model";

export interface Student extends Entity {
  firstName: string,
  lastName: string,
  email: string,
  group: Group,
}

export interface StudentGridModel extends Entity {
  firstName: string,
  lastName: string,
  isSelected: boolean,
}
