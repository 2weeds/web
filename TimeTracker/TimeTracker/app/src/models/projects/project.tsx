import { SelectListItem } from "../select-list-item";
import { ProjectMemberAction } from "./project-member-action";

export interface Project {
    id: string,
    usernamesWithIds: SelectListItem[],
    title: string,
    projectMemberIds: SelectListItem[],
    projectMemberActions: ProjectMemberAction[],
}