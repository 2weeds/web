import { SelectListItem } from "../select-list-item";
import { ProjectMember } from "./project-member";
import {ProjectAction} from "./project-action";

export interface Project {
    id: string,
    usernamesWithIds: SelectListItem[],
    title: string,
    projectMemberIds: SelectListItem[],
    projectMembers: ProjectMember[],
    projectActions: ProjectAction[]
}