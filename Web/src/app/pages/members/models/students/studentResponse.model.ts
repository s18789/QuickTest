import { Entity } from "src/app/shared/utils/models/entity.model";
import { GroupResponse } from "../groups/groupResponse.model";

export interface StudentResponse extends Entity {
  firstName: string,
  lastName: string,
  email: string,
  group: GroupResponse,
}
