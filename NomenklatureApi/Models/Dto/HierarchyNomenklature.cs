#region Using
using NomenklatureApi.Models.Interfaces;
#endregion

namespace NomenklatureApi.Models.Dto
{
    #region Public Class HierarchyNomenklature

    /// <summary>
    /// ДТО для продуктов в иерархии по уровням
    /// </summary>
    public class HierarchyNomenklature : INomenklature
    {
        #region Public Fields

        /// <summary>
        /// Уровень продукта в иерархии
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// Наименование продукта
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Идентификатор продукта
        /// </summary>
        public int NomenklatureId { get; set; }

        /// <summary>
        /// Стоимость одного экземпляра продутка
        /// </summary>
        public int Price { get; set; }

        /// <summary>
        /// Количество продукта в составе родительского продука
        /// </summary>
        public int Count { get; set; }
        
        /// <summary>
        /// Общая стоимость продукта 
        /// </summary>
        public int FullPrice { get; set; }
        #endregion

        #region Constructors

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public HierarchyNomenklature() { }  
        
        /// <summary>
        /// Конструктор с параметрами
        /// </summary>
        /// <param name="level"></param>
        /// <param name="name"></param>
        /// <param name="nomenklatureId"></param>
        /// <param name="price"></param>
        /// <param name="count"></param>
        public HierarchyNomenklature(int level, string name, int nomenklatureId, int price, int count)
        {
            Level = level;
            Name = name;
            NomenklatureId = nomenklatureId;
            Price = price;
            Count = count;
            FullPrice = Price * Count;
        }
        #endregion
    }
    #endregion
}
