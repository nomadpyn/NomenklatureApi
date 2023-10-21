namespace NomenklatureApi.Models.Interfaces
{
    #region Public Interface INomenklature

    /// <summary>
    /// Интерфейс, который должен реализовать класс продукта
    /// </summary>
    public interface INomenklature
    {
        #region Public Fields

        /// <summary>
        /// Наименование продукта
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Стоимость продукта
        /// </summary>
        public int Price { get; set; }
        #endregion
    }
    #endregion
}
