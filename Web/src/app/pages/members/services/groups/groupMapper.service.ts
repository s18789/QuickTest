import { Injectable } from "@angular/core";
import { Group } from "../../models/groups/group.model";
import { GroupResponse } from "../../models/groups/groupResponse.model";

@Injectable({
  providedIn: "root",
})

export class GroupMapperService {
  constructor() { }

  mapGroupResponseToGroup(groupResponse: GroupResponse): Group {
    return {
      ...groupResponse,
      id: null,
    };
  }
}
