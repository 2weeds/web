export interface ProjectMember {
    id: string,
    userId: string,
    projectId: string,
    memberRole: number,
    isCurrentUser: boolean
}