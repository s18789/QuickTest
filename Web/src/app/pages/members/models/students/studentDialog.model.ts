import { Entity } from "src/app/shared/utils/models/entity.model";

export interface StudentDialog extends Entity {
    firstName: string,
    lastName: string,
    group: GroupDialog,
    isSelected: boolean
}

export interface GroupDialog extends Entity {
    name: string
}

export interface SelectableGroupDialog {
    isSelected: boolean, 
    name: string, 
    students: number
}