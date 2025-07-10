namespace RMSPrivateServerAPI.Models
{
    //  классы для моделей, описанных в OpenAPI спецификации

    using System.ComponentModel;

    /// <summary>
    /// Представляет информацию о роботе, включая его характеристики и идентификаторы.
    /// </summary>
    public class RobotInfo
    {
        [Description("Уникальный идентификатор робота (PRIMARY KEY)")]
        public int RobotID { get; set; }

        [Description("Тип робота (например, APR, AMR и т.д.)")]
        public string RobotType { get; set; }

        [Description("Наименование модели робота")]
        public string RobotModel { get; set; }

        [Description("Человекочитаемое имя робота")]
        public string RobotName { get; set; }

        [Description("IP-адрес робота")]
        public string IP { get; set; }

        [Description("Версия программного обеспечения робота (RCS)")]
        public string SwVersion { get; set; }

        [Description("Версия конструктивного исполнения робота")]
        public string HwVersion { get; set; }
    }


    /// <summary>
    /// Представляет положение робота или объекта на плоскости.
    /// </summary>
    public class Pose
    {
        /// <summary>
        /// Координата X в метрах.
        /// </summary>
        public float X { get; set; }

        /// <summary>
        /// Координата Y в метрах.
        /// </summary>
        public float Y { get; set; }

        /// <summary>
        /// Угол поворота в градусах, от 0 до 360 (измеряется от оси X против часовой стрелки).
        /// </summary>
        public float Heading { get; set; }
    }


    /// <summary>
    /// Представляет задачу, назначенную роботу, включая её идентификатор и список операций.
    /// </summary>
    public class RobotTask
    {
        /// <summary>
        /// Уникальный идентификатор робота, которому назначена задача.
        /// </summary>
        public int RobotID { get; set; }

        /// <summary>
        /// Уникальный идентификатор задачи в системе RMS.
        /// </summary>
        public int TaskID { get; set; }

        /// <summary>
        /// Человекочитаемое название задачи.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Список операций, которые должны быть выполнены в рамках этой задачи.
        /// </summary>
        public List<Action> Actions { get; set; }
    }


    /// <summary>
    /// Представляет информацию о неисправности или ошибке, возникшей в системе.
    /// </summary>
    public class FaultInfo
    {
        /// <summary>
        /// Источник аварийной ситуации (например, робот или зарядная станция).
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// Уникальный идентификатор источника, вызвавшего аварийную ситуацию.
        /// </summary>
        public int SourceID { get; set; }

        /// <summary>
        /// Код неисправности или ошибки.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Человекочитаемое краткое описание неисправности или ошибки.
        /// </summary>
        public string Title { get; set; }
    }



    /// <summary>
    /// agvStat enumeration
    /// </summary>
    public class APRStatus
    {
        public int Id { get; set; } = 0;
        public string Name { get; set; } = "";
        public int Value { get; set; } = 0;
        public string Definition { get; set; } = "";
    }
}
