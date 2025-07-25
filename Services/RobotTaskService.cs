using Microsoft.AspNetCore.Mvc;
using RMSPrivateServerAPI.Entities;
using RMSPrivateServerAPI.Interfaces;
using RMSPrivateServerAPI.Repositories;
#pragma warning disable CS1591, CS8603

namespace RMSPrivateServerAPI.Services
{
    public class RobotTaskService : IRobotTaskService
    {
        private readonly IRobotTaskRepository _robotTaskRepository;

        public RobotTaskService(IRobotTaskRepository robotTaskRepository)
        {
            _robotTaskRepository = robotTaskRepository;
        }
        
        public async Task<robot_task> Get(string taskId)
        {
            if (String.IsNullOrEmpty(taskId))
            {
                throw new Exception("Invalid task Id");
            }

            return await _robotTaskRepository.Get(taskId);
        }

        public async Task<robot_task> GetCurrent(string robotId)
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

        public async Task Delete(string taskId)
        {
            var task = await _robotTaskRepository.Get(taskId);

            if (task == null)
                throw new Exception("Robot task not found");

            await _robotTaskRepository.DeleteAsync(taskId);

            return;
        }
    }
}
