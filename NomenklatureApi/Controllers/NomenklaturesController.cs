#region Using
using Microsoft.AspNetCore.Mvc;
using NomenklatureApi.Data;
using NomenklatureApi.Models;
using NomenklatureApi.Models.Dto;
using NomenklatureApi.Models.Interfaces;
#endregion

namespace NomenklatureApi.Controllers
{
    #region Public Clas NomenklaturesController

    /// <summary>
    /// Контроллер для доступа к методам через http
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class NomenklaturesController : Controller, INomenklaturesApi
    {
        #region Private Fields

        /// <summary>
        /// Репозитории для работы с данными
        /// </summary>
        private readonly INomenklatureRepository _nomenklatureRepository;
        #endregion

        #region Constructor

        /// <summary>
        /// Конструктор принимает объект, реализующий интерфейс INomenklatureRepository
        /// </summary>
        /// <param name="nomenklatureRepository"></param>
        public NomenklaturesController(INomenklatureRepository nomenklatureRepository)
        {
            _nomenklatureRepository = nomenklatureRepository;
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// Получить список всей продукции
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public virtual async Task<IEnumerable<Nomenklature>> ListAsync()
        {
            return await _nomenklatureRepository.NomenklatureListAsync();
        }

        /// <summary>
        /// Создать новый продукт
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Price"></param>
        /// <returns></returns>
        [HttpPost]
        public virtual async Task<Nomenklature> AddNomenklatureAsync(string Name, int Price) 
        {
            return await _nomenklatureRepository.AddNomenklatureAsync(Name, Price);
        }

        /// <summary>
        /// Обновить продукт с определенным id
        /// </summary>
        /// <param name="nomenklatureId"></param>
        /// <param name="Name"></param>
        /// <param name="Price"></param>
        /// <returns></returns>
        [HttpPut("{nomenklatureId}")]
        public virtual async Task<Nomenklature> UpdateNomenklatureAsync(int nomenklatureId, string Name, int Price)
        {
            return await _nomenklatureRepository.UpdateNomenklatureAsync(nomenklatureId, Name, Price);
        }

        /// <summary>
        /// Добавить метаданные для продукта с определенным id
        /// </summary>
        /// <param name="nomenklatureId"></param>
        /// <param name="metadata"></param>
        /// <returns></returns>
        [HttpPost("metadata/{nomenklatureId}")]
        public virtual async Task<ProductMetadata> AddMetadataToNomenklature(int nomenklatureId, [FromBody] ProductMetadataDto metadata)
        {
            return await _nomenklatureRepository.AddMetaDataToNomenklature(nomenklatureId, metadata);
        }

        /// <summary>
        /// Cвязать два продукта
        /// </summary>
        /// <param name="linkDto"></param>
        /// <returns></returns>
        [HttpPost("link")]
        public virtual async Task<Link> AddNomenklatureLink(LinkDto linkDto)
        {
            return await _nomenklatureRepository.AddNomenklatureLink(linkDto);
        }

        /// <summary>
        /// Получить информацию о продукте с определенным id, включая все подчиненные продукты 
        /// и подсчитать стоимость продуктов на каждом уровне
        /// </summary>
        /// <param name="nomenklatureId"></param>
        /// <returns></returns>
        [HttpGet("{nomenklatureId}")]
        public virtual async Task<IEnumerable<HierarchyNomenklature>> HierarchyNomenklatureAsync(int nomenklatureId)
        {
            var data = await _nomenklatureRepository.HierarchyNomenklatureListAsync(nomenklatureId);

            return data;
        }

        /// <summary>
        /// Удалить продукт с определенным id
        /// </summary>
        /// <param name="nomenklatureId"></param>
        /// <returns></returns>
        [HttpDelete("{nomenklatureId}")]
        public virtual async Task<bool> DeleteNomenklature(int nomenklatureId)
        {
            return await _nomenklatureRepository.DeleteNomenklature(nomenklatureId);
        }

        /// <summary>
        /// Удалить связь между двумя продуктами
        /// </summary>
        /// <param name="linkDto"></param>
        /// <returns></returns>
        [HttpDelete("link")]
        public virtual async Task<bool> DeleteNomenklatureLink(LinkDto linkDto)
        {
            var boool = await _nomenklatureRepository.DeleteNomenklatureLink(linkDto);

            return boool;
        }
        #endregion
    }
    #endregion
}
