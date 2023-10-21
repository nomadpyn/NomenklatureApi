#region Using
using NomenklatureApi.Models.Interfaces;
#endregion

namespace NomenklatureApi.Models
{
    #region Public Class Nomenklature

    /// <summary>
    /// Модель для продукта для БД
    /// </summary>
    public class Nomenklature : INomenklature
    {
        #region Public Fields

        /// <summary>
        /// Идентификатор модели в БД
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Наименование продукта
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Стоимость продукта
        /// </summary>
        public int Price { get; set; }

        /// <summary>
        /// Коллекция для хранения метаданных продукта
        /// </summary>
        public virtual List<ProductMetadata> Metadata { get; set; } = new();
        #endregion
    }
    #endregion
}
