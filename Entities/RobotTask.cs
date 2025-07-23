namespace RMSPrivateServerAPI.Entities
{
    /// <summary>
    /// Представляет задачу, назначенную роботу, включая её идентификатор и список операций.
    /// </summary>
    public class RobotTask
    {
        /// <summary>
        /// Уникальный идентификатор робота, которому назначена задача.
        /// </summary>
        public string RobotId { get; set; }

        /// <summary>
        /// Уникальный идентификатор задачи в системе RMS.
        /// </summary>
        public int TaskId { get; set; }

        /// <summary>
        /// Человекочитаемое название задачи.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Список операций, которые должны быть выполнены в рамках этой задачи.
        /// </summary>
        public List<Action> Actions { get; set; }
    }

}
