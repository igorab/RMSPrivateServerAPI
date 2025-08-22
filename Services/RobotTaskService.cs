using RMSPrivateServerAPI.DTOs;
using RMSPrivateServerAPI.Data;
using RMSPrivateServerAPI.Entities;
using RMSPrivateServerAPI.Enums;
using RMSPrivateServerAPI.Interfaces;
using RMSPrivateServerAPI.Models;
using RMSPrivateServerAPI.Repositories;
using RMSPrivateServerAPI.Models.Lib;
#pragma warning disable CS1591, CS8603

namespace RMSPrivateServerAPI.Services
{
    public class RobotTaskService : IRobotTaskService
    {
        private readonly IRobotTaskRepository _robotTaskRepository;

        private readonly WmsDbContext _context;
        
        public RobotTaskService(IRobotTaskRepository robotTaskRepository, WmsDbContext context)
        {
            _robotTaskRepository = robotTaskRepository;
            _context = context;
        }

        //Получение текущей задачи для робота
        public async Task<Queue<RobotAction>> GetRobotActions(Guid robotId)
        {
            if (Guid.Empty == robotId) throw new Exception("Invalid robot Id");
            
            return await Task.Run(() => RobotActionsQueue(robotId));
        }

        /// <summary>
        ///  Task and TaskActions from wms
        /// </summary>
        /// <param name="robotId">Guid robot id </param>
        /// <returns></returns>
        public (TasksDto? curTask, List<TaskActionsDto>? taskActions) RobotTaskActions(Guid robotId)
        {
            var statusReceived = nameof(RobotTaskStatus.Received);

            var joinedData = from task in _context.Tasks
                             where task.StoreWmsId == RMSSetup.DefaultStoreWmsId
                             orderby task.Priority ascending
                             join action in _context.TaskActions
                                 on task.TaskId equals action.TaskId
                             where action.Status == statusReceived
                             orderby action.CreatedAt ascending
                             select new
                             {
                                 Task = task,
                                 Action = action
                             };

            List<TaskActionsDto>? taskActionsDtos = new List<TaskActionsDto>();
            
            foreach (var task_data in joinedData)
            {
                taskActionsDtos.Add(task_data.Action);
            }

            var data = joinedData.FirstOrDefault();
           
            var curTask = data?.Task;

            return (curTask, taskActionsDtos);
        }


        /// <summary>
        /// очередь действий по заданию
        /// </summary>
        /// <returns></returns>
        private Queue<RobotAction> RobotActionsQueue(Guid robotId)
        {
            Queue<RobotAction> robotActions = new Queue<RobotAction>();

            var (curTask, curTaskAction) = RobotTaskActions(robotId);

            robotActions.Enqueue(new CommonAction() { 
                ActionTypeId = ActionType.wait, 
                ActionName = nameof(ActionType.wait)                 
            });

            //TODO Логика привязки очереди к Task 

            //robotActions.Enqueue(new MoveToAction() { 
            //    ActionTypeId = ActionType.moveTo, 
            //    ActionName = nameof(ActionType.moveTo), 
            //    Pose = new Pose { X = 0, Y = 0, Heading = 0 }
            //});
            //robotActions.Enqueue(new CommonAction() {
            //    ActionTypeId = ActionType.load, 
            //    ActionName = nameof(ActionType.load)
            //});
            //robotActions.Enqueue(new MoveToAction() {
            //    ActionTypeId = ActionType.moveTo, 
            //    ActionName = nameof(ActionType.moveTo),
            //    Pose = new Pose { X = 100, Y = 80, Heading = 40 }
            //});
            //robotActions.Enqueue(new CommonAction() 
            //{ 
            //    ActionTypeId = ActionType.unload, ActionName = nameof(ActionType.unload) 
            //});

            return robotActions;
        }

        public async Task<robot_task> GetById(Guid taskId)
        {
            if (Guid.Empty == taskId)
            {
                throw new Exception("Invalid task Id");
            }

            return await _robotTaskRepository.GetByTaskId(taskId);
        }

        public async Task<List<RobotTaskFlat?>> GetCurrent(Guid robotId)
        {
            if (Guid.Empty == robotId)
            {
                throw new Exception("Invalid robot Id");
            }

            return await _robotTaskRepository.GetCurrent(robotId);
        }


        public async Task<robot_task> Insert(robot_task task)
        {
            var newId = await _robotTaskRepository.UpsertAsync(task);

            if (newId != Guid.Empty)
            {
                task.TaskId = newId;
            }
            else
            {
                throw new Exception("Failed to insert robot task");
            }

            return task;
        }

        public async Task<robot_task> Update(robot_task task)
        {
            if (task.TaskId == Guid.Empty)
            {
                throw new Exception("Task id must be set");
            }

            var oldId = task.TaskId;

            var newId = await _robotTaskRepository.UpsertAsync(task);

            if (newId != oldId)
            {
                throw new Exception("Failed to update robot task");
            }

            return task;
        }

        public async Task DeleteTask(Guid taskId)
        {
            var r_task = await _robotTaskRepository.GetByTaskId(taskId);

            if (r_task != null)
            {
                await _robotTaskRepository.DeleteAsync(taskId);
            }
            return ;
        }       
    }
}
