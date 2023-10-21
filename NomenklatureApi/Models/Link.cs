
namespace NomenklatureApi.Models
{
    #region Public Class Link

    /// <summary>
    /// Модель ссылок между продуктами для БД
    /// </summary>
    public class Link 
    {
        #region Public Fields

        /// <summary>
        /// Идентификатор модели для БД
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Подчиненный продукт
        /// </summary>
        public Nomenklature ChildId { get; set; }

        /// <summary>
        /// Продукт родитель
        /// </summary>
        public Nomenklature ParentId { get; set; }

        /// <summary>
        /// Количество подчиненных продуктов в составе родительского продукта
        /// </summary>
        public int Count { get; set; }
        #endregion
    }
    #endregion
}
