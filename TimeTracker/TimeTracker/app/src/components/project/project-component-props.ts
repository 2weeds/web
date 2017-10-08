import { Project } from "../../models/projects/project";
export interface IProjectComponentProps {
    projectChanged(project: Project): void;
    projectSaved(project: Project): void;
    project: Project
}