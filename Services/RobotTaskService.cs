using Microsoft.AspNetCore.Mvc;
using RMSPrivateServerAPI.Data;
using RMSPrivateServerAPI.Entities;
using RMSPrivateServerAPI.Enums;
using RMSPrivateServerAPI.Interfaces;
using RMSPrivateServerAPI.Models;
using RMSPrivateServerAPI.Repositories;
#pragma warning disable CS1591, CS8603

namespace RMSPrivateServerAPI.Services
{
    public class RobotTaskService : IRobotTaskService
    {
        private readonly IRobotTaskRepository _robotTaskRepository;

        private readonly ApplicationDbContext _context;

        public RobotTaskService(IRobotTaskRepository robotTaskRepository, ApplicationDbContext context)
        {
            _robotTaskRepository = robotTaskRepository;
            _context = context;
        }

        
        public async Task<Queue<RobotAction?>> GetRobotActions(Guid taskId)
        {
            if (Guid.Empty == taskId) throw new Exception("Invalid task Id");
            
            return await Task.Run(() => RobotActionsQueue());
        }

        private  Queue<RobotAction?> RobotActionsQueue()
        {
            Queue<RobotAction?> robotActions = new Queue<RobotAction?>();
            
            var joinedData = from task in _context.Tasks
                             join action in _context.TaskActions
                                 on task.TaskId equals action.TaskId
                             where action.Status == "Received" 
                             orderby action.CreatedAt descending
                             select new
                             {
                                 Task = task,
                                 Action = action
                             };

            var data = joinedData.ToList();

            foreach (var item in data)
            {
                var curAction = item.Action;

                var curTask = item.Task;
            }


            robotActions.Enqueue(new RobotAction() { ActionType = ActionType.moveTo });
            robotActions.Enqueue(new RobotAction() { ActionType = ActionType.load });
            robotActions.Enqueue(new RobotAction() { ActionType = ActionType.moveTo });
            robotActions.Enqueue(new RobotAction() { ActionType = ActionType.unload });


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
