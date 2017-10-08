import { SelectListItem } from "../select-list-item";

export interface Project {
    id: string,
    usernamesWithIds: SelectListItem[],
    title: string,
    projectMemberIds: string[],
    projectMembers: any,
}