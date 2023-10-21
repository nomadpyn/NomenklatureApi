#region Using
using Microsoft.EntityFrameworkCore;
using NomenklatureApi.Models;
using NomenklatureApi.Models.Dto;
#endregion

namespace NomenklatureApi.Data
{
    #region Public Class NomenklatureRepository

    /// <summary>
    /// Репозиторий для работы с данными
    /// </summary>
    public class NomenklatureRepository : INomenklatureRepository
    {
        #region Private Fields

        /// <summary>
        /// Контекст для БД
        /// </summary>
        private NomenklatureContext _context;
        #endregion

        #region Constructor

        /// <summary>
        /// Конструктор, принимающий DbContext
        /// </summary>
        /// <param name="context"></param>
        public NomenklatureRepository(NomenklatureContext context) 
        {
            _context = context;
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// Добавляет в БД новый продукт и возвращает его 
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Price"></param>
        /// <returns></returns>
        public async Task<Nomenklature> AddNomenklatureAsync(string Name, int Price)
        {
            Nomenklature newNomenklature = new() { Name = Name, Price = Price };
            
            await _context.Nomenklatures.AddAsync(newNomenklature);

            await _context.SaveChangesAsync();

            return newNomenklature;
        }

        /// <summary>
        /// Обновляет продукт в БД и возвращает его в случае успеха, а в случае неудачи возвращает пустой объект Nomenklature
        /// </summary>
        /// <param name="id"></param>
        /// <param name="Name"></param>
        /// <param name="Price"></param>
        /// <returns></returns>
        public async Task<Nomenklature> UpdateNomenklatureAsync(int id, string Name, int Price)
        {
            Nomenklature oldNomenklature = await _context.Nomenklatures.Where(x => x.Id == id).FirstOrDefaultAsync();

            if(oldNomenklature != null)
            {
                oldNomenklature.Name = Name;
                oldNomenklature.Price = Price;

                await _context.SaveChangesAsync();
            }                   

            return oldNomenklature == null ? new Nomenklature() : oldNomenklature ;
        }

        /// <summary>
        /// Возвращает список продуктов из БД вместе с метаданными
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Nomenklature>> NomenklatureListAsync()
        {
            List<Nomenklature> nomenklatures = await _context.Nomenklatures.OrderBy(x=> x.Id).ToListAsync();

            var productMetadata = await _context.ProductMetaDatas.GroupBy(x => x.NomenklatureId).ToDictionaryAsync(t => t.Key);

            foreach(var data in productMetadata)
            {
                nomenklatures.Where(x => x.Id == data.Key).FirstOrDefault().Metadata = data.Value.ToList();
            }

            return nomenklatures;
        }

        /// <summary>
        /// Добавляет метаданные к продукту и возвращает их в случае успеха, а в случае неудачи возвращает пустой объект ProductMetadata
        /// </summary>
        /// <param name="nomenklatureId"></param>
        /// <param name="metaData"></param>
        /// <returns></returns>
        public async Task<ProductMetadata> AddMetaDataToNomenklature(int nomenklatureId, ProductMetadataDto metaData)
        {
            Nomenklature nomenklature = _context.Nomenklatures.AsParallel().Where(x => x.Id == nomenklatureId).FirstOrDefault();

            ProductMetadata newProductMetadata = new();
            if (nomenklature != null)
            {
                newProductMetadata.NomenklatureId = nomenklature.Id;
                newProductMetadata.MetaDataName = metaData.MetaDataName;
                newProductMetadata.MetaDataValue = metaData.MetaDataValue;               

                await _context.ProductMetaDatas.AddAsync(newProductMetadata);

                await _context.SaveChangesAsync();
            }
            return newProductMetadata;
        }

        /// <summary>
        /// Добавляет в БД ссылку между продуктами и возвращает новый объект, а в случае неудачи возвраащет пустой объект Link
        /// </summary>
        /// <param name="linkDto"></param>
        /// <returns></returns>
        public async Task<Link> AddNomenklatureLink(LinkDto linkDto)
        {
            Nomenklature parent = await GetNomenklatureById(linkDto.ParentId);

            Nomenklature child = await GetNomenklatureById(linkDto.ChildId);

            Link newLink = new();

            if (parent != null && child != null)
            {
                newLink = await AddLink(parent, child, linkDto.Count);
            }

            return newLink;
        }

        /// <summary>
        /// Возвращает список иерархических продуктов с указанием уровня
        /// </summary>
        /// <param name="nomenklatureId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<HierarchyNomenklature>> HierarchyNomenklatureListAsync(int nomenklatureId)
        {
            Nomenklature nomenklature = await _context.Nomenklatures.FirstOrDefaultAsync(p => p.Id == nomenklatureId);

            if (nomenklature == null)
            {
                return new List<HierarchyNomenklature>();
            };

            return GetNomenklatureRecursive(nomenklature);
        }
        
        /// <summary>
        /// Удаляет продукт из БД, в случае успеха возвращает true, в случае неудачи false
        /// </summary>
        /// <param name="nomenklatureId"></param>
        /// <returns></returns>
        public async Task<bool> DeleteNomenklature(int nomenklatureId)
        {
            var data = await _context.Links.Where(x => (x.ParentId.Id == nomenklatureId || x.ChildId.Id == nomenklatureId)).CountAsync();

            if(data > 0)
            {
                return await Task.FromResult(false);
            }

            Nomenklature nomenklature = _context.Nomenklatures.Where(x => x.Id == nomenklatureId).FirstOrDefault();

            if (nomenklature != null)
            {
                _context.ProductMetaDatas.RemoveRange(_context.ProductMetaDatas.Where(x => x.Id == nomenklatureId));

                _context.Nomenklatures.Remove(nomenklature);

                await _context.SaveChangesAsync();

                return await Task.FromResult(true);
            }

            return await Task.FromResult(false);
        }

        /// <summary>
        /// Удаляет ссылку между продуктами из БД, в случае успеха возвращает true, в случае неудачи false
        /// </summary>
        /// <param name="linkDto"></param>
        /// <returns></returns>
        public async Task<bool> DeleteNomenklatureLink(LinkDto linkDto)
        {
            var data = await _context.Links.Where(x => (x.ParentId.Id == linkDto.ParentId && x.ChildId.Id == linkDto.ChildId)).FirstOrDefaultAsync();

            if (data == null)
            {
                return await Task.FromResult(false);
            }

            _context.Links.Remove(data);

            await _context.SaveChangesAsync();

            return await Task.FromResult(true);
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// Возвращает продукт из БД по id
        /// </summary>
        /// <param name="nomenklatureId"></param>
        /// <returns></returns>
        private async Task<Nomenklature> GetNomenklatureById(int nomenklatureId)
        {
            return await _context.Nomenklatures.Where(x => x.Id == nomenklatureId).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Возвращает ссыку из БД по id родителя и id подчиненного
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="child"></param>
        /// <returns></returns>
        private async Task<Link> GetLink(int parent, int child)
        {
            Link link = await _context.Links.Where(x => (x.ParentId.Id == parent && x.ChildId.Id == child)).FirstOrDefaultAsync();

            return link;
        }

        /// <summary>
        /// Получение списка HierarchyNomenklature и расчет стоимости в рекурсии по продукту
        /// </summary>
        /// <param name="nomenklature"></param>
        /// <param name="count"> Первый продукт принимает в количестве 1</param>
        /// <param name="level">Для первого продукта уровень 1</param>
        /// <returns></returns>
        private List<HierarchyNomenklature> GetNomenklatureRecursive(Nomenklature nomenklature, int count = 1, int level = 1)
        {
            List<HierarchyNomenklature> nomenklatures = new List<HierarchyNomenklature>();

            List<Link> links = _context.Links.Where(x => x.ParentId.Id == nomenklature.Id).Include(x => x.ParentId).Include(x => x.ChildId).ToList();

            nomenklatures.Add(new HierarchyNomenklature(level, nomenklature.Name,nomenklature.Id,nomenklature.Price, count));

            level++;

            foreach (var item in links) 
            { 
                nomenklatures.AddRange(GetNomenklatureRecursive(item.ChildId, item.Count, level));
                CalculateSum(ref nomenklatures, level);
            }

            return nomenklatures;
        }

        /// <summary>
        /// Расчитывает общую сумму подчиненного продукта и прибавляет ее к родительской общей сумме
        /// </summary>
        /// <param name="nomenklatures"></param>
        /// <param name="level"></param>
        private void CalculateSum(ref List<HierarchyNomenklature> nomenklatures, int level)
        {
            nomenklatures.Where(x => x.Level == level - 1).LastOrDefault().FullPrice
                    += nomenklatures.Where(x => x.Level == level).LastOrDefault().FullPrice
                    * nomenklatures.Where(x => x.Level == level - 1).LastOrDefault().Count;
        }

        /// <summary>
        /// Проверяет есть ли уже такие ссылки в БД между продуктами и есть ли между ними иерархическая зависимость, и в случае отсутствия таких делать запись в БД
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="child"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        private async Task<Link> AddLink(Nomenklature parent, Nomenklature child, int count)
        {
            Link newLink = new();

            Link checkLink = await GetLink(parent.Id, child.Id);

            Link reverseLink = await GetLink(child.Id, parent.Id);            

            if (checkLink == null && reverseLink == null)
            {
                bool checkDependency = await CheckParentChildDependency(parent, child);

                if (checkDependency)
                {

                    newLink.ParentId = parent;
                    newLink.ChildId = child;
                    newLink.Count = count;

                    await _context.Links.AddAsync(newLink);

                    await _context.SaveChangesAsync();
                }
            }

            return newLink;
        }

        /// <summary>
        /// Проверяет есть ли зависимость между продуктами, является ли parent уже в подчинении у child,
        /// в случает отсутсвия зависимости возвращает true, в обратном случае false
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="child"></param>
        /// <returns></returns>
        private async Task<bool> CheckParentChildDependency(Nomenklature parent, Nomenklature child)
        {
            if (parent != null && child != null)
            {
                List<HierarchyNomenklature> hierarchy = GetNomenklatureRecursive(child);

                if((hierarchy.Where(x => x.NomenklatureId == parent.Id)).Count() > 0)
                {
                    HierarchyNomenklature parentNomenklature = hierarchy.Where(x => x.NomenklatureId == parent.Id).FirstOrDefault();

                    HierarchyNomenklature childNomenklature = hierarchy.Where(x => x.NomenklatureId == child.Id).FirstOrDefault();

                    if(parentNomenklature.Level > childNomenklature.Level)
                    {
                        return false;
                    }
                }

                return true;
            }

            return false;
        }
        #endregion
    }
    #endregion
}
