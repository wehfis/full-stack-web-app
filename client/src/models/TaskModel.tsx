type TaskModel = {
    id: string;
    name: string;
    description: string;
    result: number;
    startedAt: string;
    finishedAt: string;
    percentageDone: number;
    ownerId: string;
};
export default TaskModel;