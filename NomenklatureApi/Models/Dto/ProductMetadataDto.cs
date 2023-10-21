#region Using
using NomenklatureApi.Models.Interfaces;
#endregion

namespace NomenklatureApi.Models.Dto
{
    #region Public Class ProductMetadataDto

    /// <summary>
    /// ДТО для метаданных
    /// </summary>
    public class ProductMetadataDto : IMetaData
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
