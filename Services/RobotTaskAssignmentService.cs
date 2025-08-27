using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RMSPrivateServerAPI.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// This class contain the logic to check for free robots and tasks
/// </summary>
public class RobotTaskAssignmentService : BackgroundService
{
    private readonly IRobotRepository _robotRepository;
    private readonly IRobotTaskRepository _robotTaskRepository;
    private readonly IRobotTaskService _robotTaskService;
    private readonly ILogger<RobotTaskAssignmentService> _logger;

    /// <summary>
    ///  Create a Background Service
    /// </summary>    
    public RobotTaskAssignmentService(IRobotRepository robotRepository,
                                       IRobotTaskRepository robotTaskRepository,
                                       IRobotTaskService robotTaskService,
                                       ILogger<RobotTaskAssignmentService> logger)
    {
        _robotRepository = robotRepository;
        _robotTaskRepository = robotTaskRepository;
        _robotTaskService = robotTaskService;
        _logger = logger;
    }

    /// <summary>
    /// This to check every second for free robots and available tasks 
    /// assigning tasks as they become available.
    /// </summary>
    /// <param name="stoppingToken"></param>
    /// <returns></returns>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await AssignTasksToFreeRobots();
            await Task.Delay(1000, stoppingToken); // Wait for 1 second
        }
    }

    /// <summary>
    /// assigning tasks as they become available
    /// </summary>    
    private async Task AssignTasksToFreeRobots()
    {
        try
        {
            var freeRobots = await _robotRepository.GetFreeRobots();

            var availableTasks = await _robotTaskRepository.GetAvailableTasks(); 

            foreach (var robot in freeRobots)
            {
                if (availableTasks.Count > 0)
                {
                    var taskToAssign = availableTasks[0]; // Get the first available task

                    await _robotTaskService.AssignTaskToRobot(robot.RobotId, taskToAssign.TaskId); 

                    _logger.LogInformation($"Assigned task {taskToAssign.RobotId} to robot {robot.RobotId}");

                    availableTasks.RemoveAt(0); // Remove the assigned task from the list
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while assigning tasks to robots.");
        }
    }
}
