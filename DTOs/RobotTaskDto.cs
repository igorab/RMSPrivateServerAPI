using RMSPrivateServerAPI.Models;

namespace RMSPrivateServerAPI.DTOs
{
    /// <summary>
    /// Robot Task Dto
    /// </summary>
    public class RobotTaskDto
    {
        /// <summary>
        /// Уникальный идентификатор задачи в системе RMS.
        /// </summary>
        public string? TaskId { get; set; }

        /// <summary>
        /// Уникальный идентификатор робота, которому назначена задача.
        /// </summary>
        public string? RobotId { get; set; }
        
        /// <summary>
        /// Человекочитаемое название задачи.
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// Список операций, которые должны быть выполнены в рамках этой задачи.
        /// </summary>
        public List<RobotAction>? Actions { get; set; }
    }
}
