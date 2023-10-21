
namespace NomenklatureApi.Models.Dto
{
    #region Public Class LinkDto

    /// <summary>
    /// ДТО для ссылки между продуктами
    /// </summary>
    public class LinkDto
    {
        #region Public Fields

        /// <summary>
        /// Идентификатор родительского продукта
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// Идентификатор подчиненного продукта
        /// </summary>
        public int ChildId { get; set; }

        /// <summary>
        /// Количество подчиненных продуктов
        /// </summary>
        public int Count { get; set; }
        #endregion
    }
    #endregion
}
