import { Group } from "./group.model";

export interface Student {
  id?: number,
  firstName?: string,
  lastName?: string,
  email?: string,
  groupId?: number,
  groupName?: string,
  group?: Group,
  groups?: Group[]
}
