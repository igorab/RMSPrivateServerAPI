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

        
        public async Task<Queue<RobotAction?>> GetRobotActions(string taskId)
        {
            if (String.IsNullOrEmpty(taskId)) throw new Exception("Invalid task Id");
            
            return await Task.Run(() => InitRobotActions());
        }

        private  Queue<RobotAction?> InitRobotActions()
        {
            Queue<RobotAction?> robotActions = new Queue<RobotAction?>();

            //var tasksWithLastAction = _context.Tasks
            //    .Select(task => new 
            //    {
            //        Task = task,
            //        LastAction = task.TaskActions
            //            .OrderByDescending(a => a.CreatedAt)
            //            .FirstOrDefault()
            //    })
            //    .Where(x => x.LastAction?.Status == "Received")
            //    .ToList();

            var joinedData = from task in _context.Tasks
                             join action in _context.TaskActions
                                 on task.TaskId equals action.TaskId
                             where action.Status == "Received"  // Пример фильтра
                             select new
                             {
                                 Task = task,
                                 Action = action
                             };


            robotActions.Enqueue(new RobotAction() { ActionType = ActionType.moveTo });
            robotActions.Enqueue(new RobotAction() { ActionType = ActionType.moveTo });
            return robotActions;
        }

        public async Task<robot_task> GetById(string taskId)
        {
            if (String.IsNullOrEmpty(taskId))
            {
                throw new Exception("Invalid task Id");
            }

            return await _robotTaskRepository.GetByTaskId(taskId);
        }

        public async Task<List<RobotTaskFlat?>> GetCurrent(string robotId)
        {
            if (string.IsNullOrEmpty(robotId))
            {
                throw new Exception("Invalid robot Id");
            }

            return await _robotTaskRepository.GetCurrent(robotId);
        }


        public async Task<robot_task> Insert(robot_task task)
        {
            var newId = await _robotTaskRepository.UpsertAsync(task);

            if (newId != String.Empty)
            {
                task.task_id = newId;
            }
            else
            {
                throw new Exception("Failed to insert robot task");
            }

            return task;
        }

        public async Task<robot_task> Update(robot_task task)
        {
            if (task.task_id == String.Empty)
            {
                throw new Exception("Task id must be set");
            }

            var oldId = task.task_id;

            var newId = await _robotTaskRepository.UpsertAsync(task);

            if (newId != oldId)
            {
                throw new Exception("Failed to update robot task");
            }

            return task;
        }

        public async Task DeleteTask(string taskId)
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
