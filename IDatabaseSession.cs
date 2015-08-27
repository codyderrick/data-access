using System.Linq;

namespace myfavoritetoothbrush.web.ui.Data
{
    public interface IDatabaseSession
    {
        IQueryable<T> Queryable<T>(); // where T : CollectionModel;
        void Save<T>(T entity) where T : BaseDocument;
        void Delete<T>(T entity) where T : BaseDocument;
    }
}
