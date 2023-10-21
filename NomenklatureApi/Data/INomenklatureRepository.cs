#region Using
using NomenklatureApi.Models;
using NomenklatureApi.Models.Dto;
#endregion

namespace NomenklatureApi.Data
{
    #region Public Interface INomenklatureRepository

    /// <summary>
    /// Интерфейс, который должен реализовать репозитории продуктов
    /// </summary>
    public interface INomenklatureRepository
    {
        #region Private Fields

        /// <summary>
        /// Добавляет информацию о продукте в БД
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Price"></param>
        /// <returns></returns>
        public Task<Nomenklature> AddNomenklatureAsync(string Name, int Price);

        /// <summary>
        /// Обновление информации о продукте в БД
        /// </summary>
        /// <param name="id"></param>
        /// <param name="Name"></param>
        /// <param name="Price"></param>
        /// <returns></returns>
        public Task<Nomenklature> UpdateNomenklatureAsync(int id, string Name, int Price);

        /// <summary>
        /// Получение списка всех продуктов с метаданными
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<Nomenklature>> NomenklatureListAsync();

        /// <summary>
        /// Добавляет метаданные продукта в БД
        /// </summary>
        /// <param name="nomenklatureId"></param>
        /// <param name="metadata"></param>
        /// <returns></returns>
        public Task<ProductMetadata> AddMetaDataToNomenklature(int nomenklatureId, ProductMetadataDto metadata);

        /// <summary>
        /// Добавляет информацию о ссылке между продуктами в БД
        /// </summary>
        /// <param name="linkDto"></param>
        /// <returns></returns>
        public Task<Link> AddNomenklatureLink(LinkDto linkDto);

        /// <summary>
        /// Получение списка продуктов с иерархическими уровнями
        /// </summary>
        /// <param name="nomenklatureId"></param>
        /// <returns></returns>
        public Task<IEnumerable<HierarchyNomenklature>> HierarchyNomenklatureListAsync(int nomenklatureId);

        /// <summary>
        /// Удаляет продукт из БД
        /// </summary>
        /// <param name="nomenklatureId"></param>
        /// <returns></returns>
        public Task<bool> DeleteNomenklature(int nomenklatureId);

        /// <summary>
        /// Удаляет ссылку между продуктами из БД
        /// </summary>
        /// <param name="linkDto"></param>
        /// <returns></returns>
        public Task<bool> DeleteNomenklatureLink(LinkDto linkDto);
        #endregion
    }
    #endregion
}
