namespace NomenklatureApi.Models.Interfaces
{
    #region Public Interface IMetaData

    /// <summary>
    /// Интерфейс, который должен реализовать класс метаданных
    /// </summary>
    public interface IMetaData
    {
        #region Public Fields

        /// <summary>
        /// Наименование метаданных
        /// </summary>
        public string MetaDataName { get; set; }

        /// <summary>
        /// Значение метаданных
        /// </summary>
        public string MetaDataValue { get; set; }
        #endregion
    }
    #endregion
}
