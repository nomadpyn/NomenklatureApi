#region Using
using Microsoft.EntityFrameworkCore;
using NomenklatureApi.Models;
#endregion

namespace NomenklatureApi.Data
{
    #region Public Class NomenklatureContext

    /// <summary>
    /// Контекст для БД
    /// </summary>
    public class NomenklatureContext : DbContext
    {
        #region Public Fields

        /// <summary>
        /// DbSet для продуктов
        /// </summary>
        public DbSet<Nomenklature> Nomenklatures { get; set; }

        /// <summary>
        /// DbSet для метаданных
        /// </summary>
        public DbSet<ProductMetadata> ProductMetaDatas { get; set; }

        /// <summary>
        /// DbSet для ссылок между продуктами
        /// </summary>
        public DbSet<Link> Links { get; set; }
        #endregion

        #region Constructor

        /// <summary>
        /// Конструтор, принимающий options 
        /// </summary>
        /// <param name="options"></param>
        public NomenklatureContext(DbContextOptions<NomenklatureContext> options) : base(options) { }
        #endregion
    }
    #endregion
}
