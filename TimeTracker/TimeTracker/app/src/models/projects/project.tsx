import { SelectListItem } from "../select-list-item";
import { ProjectMember } from "./project-member";

export interface Project {
    id: string,
    usernamesWithIds: SelectListItem[],
    title: string,
    projectMemberIds: SelectListItem[],
    projectMembers: ProjectMember[]
}