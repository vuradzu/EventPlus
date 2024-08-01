import { Priority } from "~/api/types/Priority";

export interface AssignmentModel {
  id: number;

  title: string;
  description?: string;

  priority: Priority;
  date?: Date;

  completed: boolean;
  canBeCompleted: boolean;

  assigneeId: number;
  creatorId: number;
  eventId: number;

  completionTime?: Date;
  created: Date;
}
