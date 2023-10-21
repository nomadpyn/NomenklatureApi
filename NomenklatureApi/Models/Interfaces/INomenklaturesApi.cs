#region Using
using Microsoft.AspNetCore.Mvc;
using NomenklatureApi.Models.Dto;
#endregion

namespace NomenklatureApi.Models.Interfaces
{
    #region Public Interface INomenklaturesApi

    /// <summary>
    /// Интерфейс, который должен реализовать контроллер, который работает с данными
    /// </summary>
    public interface INomenklaturesApi
    {
        #region Public Fields
        
        /// <summary>
        /// Возвращает список всех продуктов
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<Nomenklature>> ListAsync();

        /// <summary>
        /// Добавляет новый продукт
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Price"></param>
        /// <returns></returns>
        public Task<Nomenklature> AddNomenklatureAsync(string Name, int Price);

        /// <summary>
        /// Обновление данных о продукте по id
        /// </summary>
        /// <param name="nomenklatureId"></param>
        /// <param name="Name"></param>
        /// <param name="Price"></param>
        /// <returns></returns>
        public Task<Nomenklature> UpdateNomenklatureAsync(int nomenklatureId, string Name, int Price);

        /// <summary>
        /// Добавляет метаданные и привязывают его к продукту по id
        /// </summary>
        /// <param name="nomenklatureId"></param>
        /// <param name="metadata"></param>
        /// <returns></returns>
        public Task<ProductMetadata> AddMetadataToNomenklature(int nomenklatureId, [FromBody] ProductMetadataDto metadata);

        /// <summary>
        /// Добавляет ссылку между продуктами
        /// </summary>
        /// <param name="linkDto"></param>
        /// <returns></returns>
        public Task<Link> AddNomenklatureLink(LinkDto linkDto);

        /// <summary>
        /// Возвращает информацию о продукте, включая все подчиненные продукты
        /// </summary>
        /// <param name="nomenklatureId"></param>
        /// <returns></returns>
        public Task<IEnumerable<HierarchyNomenklature>> HierarchyNomenklatureAsync(int nomenklatureId);

        /// <summary>
        /// Удаляет продукт по id
        /// </summary>
        /// <param name="nomenklatureId"></param>
        /// <returns></returns>
        public Task<bool> DeleteNomenklature(int nomenklatureId);

        /// <summary>
        /// Удаляет ссылку между продуктами
        /// </summary>
        /// <param name="linkDto"></param>
        /// <returns></returns>
        public Task<bool> DeleteNomenklatureLink(LinkDto linkDto);
        #endregion
    }
    #endregion
}
