namespace RMSPrivateServerAPI.Entities
{
    /// <summary>
    /// Представляет информацию о неисправности или ошибке, возникшей в системе.
    /// </summary>
    class FaultInfo
    {
        /// <summary>
        /// Код ошибки
        /// </summary>
        public Guid FaultId { get; set; }

        /// <summary>
        /// Источник аварийной ситуации (например, робот или зарядная станция).
        /// </summary>
        public string? Source { get; set; }

        /// <summary>
        /// Уникальный идентификатор источника, вызвавшего аварийную ситуацию.
        /// </summary>
        public int SourceId { get; set; }

        /// <summary>
        /// Код неисправности или ошибки.
        /// </summary>
        public string? Code { get; set; }

        /// <summary>
        /// Человекочитаемое краткое описание неисправности или ошибки.
        /// </summary>
        public string? Title { get; set; }
    }
}
