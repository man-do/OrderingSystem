using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Data.Data.Abstract
{
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Gets all entities of type T.
        /// </summary>
        /// <returns>Entities of type T</returns>
        IEnumerable<T> GetAll(bool isAdmin = false);

        /// <summary>
        /// Gets the entity by identifier.
        /// </summary>
        /// <param name="Id">The identifier.</param>
        /// <returns>The object with the defined Id</returns>
        T GetById(object Id);

        /// <summary>
        /// Inserts the specified object into the entity collection.
        /// </summary>
        /// <param name="obj">The object.</param>
        void Insert(T obj);

        /// <summary>
        /// Updates the specified object.
        /// </summary>
        /// <param name="obj">The object.</param>
        void Update(T obj);

        /// <summary>
        /// Deletes the entity with the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void Delete(object id);

        /// <summary>
        /// Saves all changes made prior.
        /// </summary>
        void Save();
    }
}
