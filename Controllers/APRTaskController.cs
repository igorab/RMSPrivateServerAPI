using Microsoft.AspNetCore.Mvc;

namespace RMSPrivateServerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class APRTaskController : ControllerBase
    {
        private static readonly int[] TaskIds = new[] {0,1,2,3,4,5,6,7,8,9};
        private static readonly int[] TaskValues = new[] { 135, 136, 137, 144, 145, 146, 147, 148, 149, 150};
        private static readonly string[] TaskNames = new[]
        {
            "Free", "Run", "LinearMotion", "Rotate", "Arc", "Adjustment", "Parking", "ExtenionFork", "Furcation", "SyncExit"
        };

        private readonly ILogger<APRTaskController> _logger;

        public APRTaskController(ILogger<APRTaskController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetRobotTask")]
        public IEnumerable<APRTask> Get()
        {
            APRTask[] tasks = Enumerable.Range(0, TaskNames.Length).Select(index => new APRTask
            {
                TaskId = TaskIds.ElementAt(index),

                TaskValue = TaskValues.ElementAt(index),

                TaskName = TaskNames.ElementAt(index)

            }).ToArray();

            return tasks;
        }

        [HttpPost(Name = "CreateRobotTask")]
        public ActionResult<APRTask> Post([FromBody] APRTask newTask)
        {
            if (newTask == null)
            {
                return BadRequest("Task cannot be null.");
            }

            // Here you can add logic to save the new task to a database or in-memory collection
            // For demonstration, we will just log the task and return it

            _logger.LogInformation("New task created: {@Task}", newTask);

            // Return a 201 Created response with the created task
            return CreatedAtAction(nameof(Get), new { id = newTask.TaskId }, newTask);
        }

    }
}
