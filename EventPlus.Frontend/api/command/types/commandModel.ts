export interface CommandModel {
  id: number;
  name: string;
  description?: string;
  avatar?: string;
  creatorId: number;
  created: Date;
}
