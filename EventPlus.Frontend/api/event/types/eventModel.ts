import { AssignmentModel } from "~/api/assignment/types/assignmentModel";
import { Priority } from "~/api/types/Priority";

interface EventModelBase {
  id: number;

  title: string;
  description?: string;

  creatorId: number;
  commandId: number;

  priority: Priority;
  date: Date;

  icon: string;

  created: Date;
}

export interface EventModelMini extends EventModelBase {
  assignmentsCount: number;
  completedAssignmentsCount: number;
}

export interface EventModel extends EventModelBase {
  assignments: AssignmentModel[];
}
