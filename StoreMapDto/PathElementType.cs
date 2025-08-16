namespace RMSPrivateServerAPI.StoreMapDto
{
    /// <summary>
    /// Представляет тип элемента маршрута.
    /// </summary>
    public class PathElementType
    {
        /// <summary>
        /// Идентификатор типа элемента маршрута.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Описание схемы в виде GeoJSON.
        /// </summary>
        public string Scheme { get; set; } // GeoJSON as string

        /// <summary>
        /// Описание типа элемента маршрута.
        /// </summary>
        public string Desc { get; set; }
    }


    /// <summary>
    /// Представляет элемент маршрута.
    /// </summary>
    public class PathElement
    {
        /// <summary>
        /// Идентификатор элемента маршрута.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Ссылка на маршрут.
        /// </summary>
        public Guid PathId { get; set; }

        /// <summary>
        /// Номер п/п в маршруте.
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// Ссылка на территорию.
        /// </summary>
        public Guid AreaId { get; set; }

        /// <summary>
        /// Ссылка на точку (Point) начала маршрута.
        /// </summary>
        public Guid StartId { get; set; }

        /// <summary>
        /// Ссылка на точку (Point) окончания маршрута.
        /// </summary>
        public Guid FinishId { get; set; }

        /// <summary>
        /// Тип (схема) элемента маршрута.
        /// </summary>
        public Guid TypeId { get; set; }

        /// <summary>
        /// Описание геометрии элемента маршрута.
        /// </summary>
        public string Geometry { get; set; } // Формат описания необходимо уточнить
    }

    /// <summary>
    /// Представляет маршрут на складе.
    /// </summary>
    public class Path
    {
        /// <summary>
        /// Идентификатор маршрута.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Ссылка на склад.
        /// </summary>
        public Guid MapId { get; set; }

        /// <summary>
        /// Ссылка на территорию.
        /// </summary>
        public Guid AreaId { get; set; }

        /// <summary>
        /// Наименование маршрута.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Описание маршрута (по необходимости).
        /// </summary>
        public string Desc { get; set; }

        /// <summary>
        /// Дата/время создания записи.
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Дата/время изменения записи.
        /// </summary>
        public DateTime ModifiedOn { get; set; }

        /// <summary>
        /// ID пользователя, который создал запись.
        /// </summary>
        public Guid CreatedBy { get; set; }

        /// <summary>
        /// ID пользователя, который изменил запись.
        /// </summary>
        public Guid ModifiedBy { get; set; }
    }

    /// <summary>
    /// Представляет тип точки.
    /// </summary>
    public class PointType
    {
        /// <summary>
        /// Идентификатор типа точки.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Наименование типа точки.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Иконка с изображением типа точки.
        /// </summary>
        public byte Icon { get; set; } // Image as byte

        /// <summary>
        /// Описание типа точки.
        /// </summary>
        public string Desc { get; set; }
    }

    /// <summary>
    /// Представляет точку на складе.
    /// </summary>
    public class Point
    {
        /// <summary>
        /// Идентификатор точки.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Ссылка на склад.
        /// </summary>
        public Guid MapId { get; set; }

        /// <summary>
        /// Ссылка на территорию.
        /// </summary>
        public Guid AreaId { get; set; }

        /// <summary>
        /// Тип точки.
        /// </summary>
        public Guid TypeId { get; set; }

        /// <summary>
        /// Идентификатор WMS.
        /// </summary>
        public string WmsId { get; set; }

        /// <summary>
        /// X координата точки.
        /// </summary>
        public float X { get; set; }

        /// <summary>
        /// Y координата точки.
        /// </summary>
        public float Y { get; set; }

        /// <summary>
        /// Угол подъезда робота к точке.
        /// </summary>
        public float RotationAngle { get; set; }

        /// <summary>
        /// Метаданные точки.
        /// </summary>
        public string Metadata { get; set; } // JSON as string

        /// <summary>
        /// Описание точки (по необходимости).
        /// </summary>
        public string Desc { get; set; }

        /// <summary>
        /// Дата/время создания записи.
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Дата/время изменения записи.
        /// </summary>
        public DateTime ModifiedOn { get; set; }

        /// <summary>
        /// ID пользователя, который создал запись.
        /// </summary>
        public Guid CreatedBy { get; set; }

        /// <summary>
        /// ID пользователя, который изменил запись.
        /// </summary>
        public Guid ModifiedBy { get; set; }
    }

    /// <summary>
    /// Представляет тип зоны.
    /// </summary>
    public class ZoneType
    {
        /// <summary>
        /// Идентификатор типа зоны.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Наименование типа зоны.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// HEX значение цвета зоны.
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// Описание типа зоны.
        /// </summary>
        public string Desc { get; set; }
    }

    /// <summary>
    /// Представляет зону на складе.
    /// </summary>
    public class Zone
    {
        /// <summary>
        /// Идентификатор зоны.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Ссылка на склад.
        /// </summary>
        public Guid MapId { get; set; }

        /// <summary>
        /// Ссылка на территорию.
        /// </summary>
        public Guid AreaId { get; set; }

        /// <summary>
        /// Идентификатор WMS.
        /// </summary>
        public string WmsId { get; set; }

        /// <summary>
        /// Тип зоны.
        /// </summary>
        public Guid TypeId { get; set; }

        /// <summary>
        /// Описание геометрии зоны (GeoJSON).


        /// </summary>
        public string Geometry { get; set; } // GeoJSON as string

        /// <summary>
        /// Описание зоны (по необходимости).
        /// </summary>
        public string Desc { get; set; }

        /// <summary>
        /// Дата/время создания записи.
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Дата/время изменения записи.
        /// </summary>
        public DateTime ModifiedOn { get; set; }

        /// <summary>
        /// ID пользователя, который создал запись.
        /// </summary>
        public Guid CreatedBy { get; set; }

        /// <summary>
        /// ID пользователя, который изменил запись.
        /// </summary>
        public Guid ModifiedBy { get; set; }
    }


    /// <summary>
    /// Представляет складскую территорию.
    /// </summary>
    public class Area
    {
        /// <summary>
        /// Идентификатор территории.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Ссылка на склад.
        /// </summary>
        public Guid MapId { get; set; }

        /// <summary>
        /// Ссылка на родительскую территорию.
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        /// Идентификатор WMS.
        /// </summary>
        public string WmsID { get; set; }

        /// <summary>
        /// Описание геометрии территории (GeoJSON).
        /// </summary>
        public string Geometry { get; set; }

        /// <summary>
        /// Наименование территории.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Описание территории (по необходимости).
        /// </summary>
        public string Desc { get; set; }

        /// <summary>
        /// Дата/время создания записи.
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Дата/время изменения записи.
        /// </summary>
        public DateTime ModifiedOn { get; set; }

        /// <summary>
        /// ID пользователя, который создал запись.
        /// </summary>
        public Guid CreatedBy { get; set; }

        /// <summary>
        /// ID пользователя, который изменил запись.
        /// </summary>
        public Guid ModifiedBy { get; set; }
    }

    /// <summary>
    /// Представляет склад с его атрибутами.
    /// </summary>
    public class Store
    {
        /// <summary>
        /// Идентификатор склада.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Идентификатор WMS.
        /// </summary>
        public string WmsId { get; set; }

        /// <summary>
        /// Бинарное содержимое файла изображения карты.
        /// </summary>
        public byte[] ContentImage { get; set; }

        /// <summary>
        /// Оригинальное имя файла изображения.
        /// </summary>
        public string NameImage { get; set; }

        /// <summary>
        /// Бинарное содержимое файла навигации.
        /// </summary>
        public byte[] ContentNavi { get; set; }

        /// <summary>
        /// Оригинальное имя файла навигации.
        /// </summary>
        public string NameNavi { get; set; }

        /// <summary>
        /// Наименование склада.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Описание склада (по необходимости).
        /// </summary>
        public string Desc { get; set; }

        /// <summary>
        /// Дата/время создания записи.
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Дата/время изменения записи.
        /// </summary>
        public DateTime ModifiedOn { get; set; }

        /// <summary>
        /// ID пользователя, который создал запись.
        /// </summary>
        public Guid CreatedBy { get; set; }

        /// <summary>
        /// ID пользователя, который изменил запись.
        /// </summary>
        public Guid ModifiedBy { get; set; }
    }

}
