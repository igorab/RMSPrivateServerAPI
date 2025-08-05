using RMSPrivateServerAPI.Models;
using System.Text.Json.Serialization;

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
        public Guid TaskId { get; set; }

        /// <summary>
        /// Уникальный идентификатор робота, которому назначена задача.
        /// </summary>
        public Guid RobotId { get; set; }
        
        /// <summary>
        /// Человекочитаемое название задачи.
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// Список операций, которые должны быть выполнены в рамках этой задачи.
        /// </summary>
        [JsonIgnore]
        public List<RobotActionsDto>? RobotActionsDto { get; set; }

        /// <summary>
        /// Список операций, которые должны быть выполнены в рамках этой задачи.
        /// </summary>
        public List<RobotAction?>? RobotActions { get; set; }

    }
}
