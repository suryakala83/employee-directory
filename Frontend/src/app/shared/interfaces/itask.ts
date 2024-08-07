export interface ITask {
  id: number;
  name: string;
  description: string;
  dueDate: Date;
  createdBy: string;
  createdAt: string;
  modifiedBy: string;
  modifiedAt: string;
  isCompleted: boolean;
  completedAt: string;
  userId: string;
}
