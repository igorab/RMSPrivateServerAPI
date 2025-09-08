using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RMSPrivateServerAPI.Data;
using RMSPrivateServerAPI.DTOs;
using RMSPrivateServerAPI.Entities;
using RMSPrivateServerAPI.Enums;
using RMSPrivateServerAPI.Interfaces;
using RMSPrivateServerAPI.Models;
using RMSPrivateServerAPI.Models.Lib;
using RMSPrivateServerAPI.Repositories;
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
        public async Task<Queue<RobotAction>> InitRobotActions(Guid robotId)
        {
            if (Guid.Empty == robotId) throw new Exception("Invalid robot Id");

            return await Task.Run(() => RobotActionsInitQueue(robotId));
        }

        public async Task<Queue<RobotAction>> RobotTaskActionsQueue(Guid robotId, Guid taskId)
        {
            Queue<RobotAction> robotActions = await InitRobotActions(robotId);

            TasksDto? wmsTask = _context.Tasks.Find(taskId);

            List<TaskActionsDto>? wmsTaskAction = _context.TaskActions.Where(t => t.TaskId == taskId).ToList() ;

            if (robotActions == null || wmsTask == null  || wmsTaskAction == null)  return null;

            var area = _context.Areas.FirstOrDefault(q => q.WmsID == wmsTask.AreaWmsId);
            if (area == null)
                return null;

            foreach (var taskAction in wmsTaskAction)
            {
                if (taskAction.ActionType == RMSSetup.WmsLoadingId)
                {
                    var pt_from = _context.Points.FirstOrDefault(q => q.IdInWMS == taskAction.Location);

                    Pose pose_from = new Pose() { X = pt_from?.X ?? 0, Y = pt_from?.Y ?? 0, Heading = pt_from?.RotationAngle ?? 0 };

                    MoveToAction moveToAction_from = new MoveToAction() { Pose = pose_from, ActionIndex = robotActions.Count };
                    robotActions.Enqueue(moveToAction_from);

                    robotActions.Enqueue(new CommonAction()
                    {
                        ActionTypeId = ActionType.load,
                        ActionName = nameof(ActionType.load),
                        ActionIndex = robotActions.Count
                    });

                }
                else if (taskAction.ActionType == RMSSetup.WmsUnloadingId)
                {
                    var pt_to = _context.Points.FirstOrDefault(q => q.IdInWMS == taskAction.Location);

                    Pose pose_to = new Pose() { X = pt_to?.X ?? 0, Y = pt_to?.Y ?? 0, Heading = pt_to?.RotationAngle ?? 0 };

                    MoveToAction moveToAction_to = new MoveToAction() { Pose = pose_to, ActionIndex = robotActions.Count };
                    robotActions.Enqueue(moveToAction_to);

                    robotActions.Enqueue(new CommonAction()
                    {
                        ActionTypeId = ActionType.unload,
                        ActionName = nameof(ActionType.unload),
                        ActionIndex = robotActions.Count
                    });
                }
            }

            StopAction stopAction = new StopAction() { ActionIndex = robotActions.Count };
            robotActions.Enqueue(stopAction);
            
            return robotActions;           
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
                                    orderby action.ActionOrder, action.CreatedAt ascending
                              join robot_task in _context.RobotTask   
                                on action.TaskId equals robot_task.TaskId
                                where robot_task.RobotId == robotId
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
        private Queue<RobotAction> RobotActionsInitQueue(Guid robotId)
        {
            Queue<RobotAction> robotActions = new Queue<RobotAction>();
            
            robotActions.Enqueue(new CommonAction() {
                ActionIndex  = 0,
                ActionTypeId = ActionType.wait, 
                ActionName = nameof(ActionType.wait)                 
            });
                        
            return robotActions;
        }

        /// <summary>
        /// обновить статус задачи
        /// </summary>
        /// <param name="RTaskId">Task Id Guid</param>
        /// <returns>ok?</returns>        
        public async Task<bool> TaskStatusDone(Guid RTaskId)
        {
            try
            {
                // Находим Task
                var tsk = await _context.Tasks.FirstOrDefaultAsync(t => t.TaskId == RTaskId);

                // Проверяем, найдено ли действие
                if (tsk == null) return false; // Действие не найдено

                await _robotTaskRepository.DeleteAsync(RTaskId);

                // Обновляем статус
                tsk.Status = RMSSetup.StatusDone;

                // Сохраняем изменения в базе данных
                await _context.SaveChangesAsync();

                return true; // Успешное обновление
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        /// <summary>
        /// обновить статус
        /// </summary>
        /// <param name="actionId"></param>
        /// <returns></returns>
        public async Task<bool> UpdateTaskActionStatusToCompleted(Guid taskId, int actionId)
        {
            try
            {
                // Находим действие по идентификатору
                var taskAction = await _context.TaskActions
                    .FirstOrDefaultAsync(a => a.TaskId == taskId && a.Id == actionId);

                // Проверяем, найдено ли действие
                if (taskAction == null) return false; // Действие не найдено
               
                // Обновляем статус
                taskAction.Status = RMSSetup.StatusDone;

                // Сохраняем изменения в базе данных
                await _context.SaveChangesAsync();

                return true; // Успешное обновление
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public async Task<robot_task> GetById(Guid taskId)
        {
            if (Guid.Empty == taskId)
            {
                throw new Exception("Invalid task Id");
            }

            return await _robotTaskRepository.GetByTaskId(taskId);
        }

        public async Task<List<RobotActionsDone?>> GetCurrent(Guid robotId)
        {
            if (Guid.Empty == robotId)
            {
                throw new Exception("Invalid robot Id");
            }

            return await _robotTaskRepository.GetCurrent(robotId);
        }

        public async Task<IEnumerable<robot_task>> GetAll(Guid robotId)
        {
            if (Guid.Empty == robotId)
            {
                throw new Exception("Invalid robot Id");
            }

            return await _robotTaskRepository.GetAll(robotId);
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="robotId"></param>
        /// <param name="taskId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task AssignTaskToRobot(Guid robotId, Guid taskId)
        {
            robot_task rt = new robot_task() { RobotId = robotId, TaskId = taskId, Title = $"assign {DateTime.Now}" };

            await Insert(rt);            
        }

        public async Task<RobotActionsDone> AddRobotAction(Guid robotId, ActionDoneRequest request)
        {            
            var robotAction = new RobotActionsDone
            {
                ActionId = Guid.NewGuid(),
                RobotId = robotId,
                TaskId = request.TaskId,
                Result = request.Result,
                Reason = request.Reason,
                ActionIndex = request.ActionIndex,
                ActionType = 0,
                Pose_X = 0,
                Pose_Y = 0,
                Heading = 0,
                Direction = 0,
                Distance = 0,
                Angle = 0,
                Radius = 0
            };

            _context.RobotActions.Add(robotAction);
            await _context.SaveChangesAsync();

            return robotAction;
        }

        public async Task<List<int>> GetActionDoneIndexesAsync(Guid taskId, Guid robotId)
        {
            return await _context.RobotActions
                .Where(action => action.TaskId == taskId && action.RobotId == robotId)
                .OrderBy(action => action.ActionIndex)
                .Select(action => action.ActionIndex)
                .ToListAsync();
        }


        /// <summary>
        /// все действия робота по данному таску выполнены  
        /// </summary>
        /// <param name="robotId"></param>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public async Task<bool> AllActionsDone(Guid robotId, Guid taskId)
        {
            bool res = false;

            // выполненные действие
            List<int> listActionDoneIdx = await GetActionDoneIndexesAsync(taskId, robotId);

            // действия, которые необходимо выполнить
            Queue<RobotAction> robotActions = await RobotTaskActionsQueue(robotId, taskId);

            foreach (RobotAction action in robotActions)
            {
                res = listActionDoneIdx.Contains(action.ActionIndex);
                if (!res) break;                
            }            

            return res;
        }

       
    }
}
