#region Using
using NomenklatureApi.Models.Interfaces;
#endregion

namespace NomenklatureApi.Models
{
    #region Public Class ProductMetadata

    /// <summary>
    /// Модель для метаданных класса для БД
    /// </summary>
    public class ProductMetadata : IMetaData
    {
        #region Public Fields
        
        /// <summary>
        /// Идентификатор модели в БД
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Идентификатор продукта, к которому относятся метаданные
        /// </summary>
        public int NomenklatureId { get; set; }

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
