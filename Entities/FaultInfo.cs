namespace RMSPrivateServerAPI.Entities
{
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
}
