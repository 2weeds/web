import { ProjectMemberAction } from "./project-member-action";
export interface ProjectMember {
    id: string,
    userId: string,
    projectId: string,
    memberRole: number,
    projectMemberActions: ProjectMemberAction[],
    isCurrentUser: boolean
}