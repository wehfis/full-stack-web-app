import TaskModel from "./TaskModel";

type UserModel = {
    id: string;
    email: string;
    password: string;
    role: string;
    heavyTasks: TaskModel;
};
export default UserModel;