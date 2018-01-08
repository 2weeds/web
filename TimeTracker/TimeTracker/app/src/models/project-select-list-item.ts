import { SelectListItem } from "./select-list-item";

export interface ProjectSelectListItem extends SelectListItem {
    projectMemberId: string,
    isProjectManager: boolean
}