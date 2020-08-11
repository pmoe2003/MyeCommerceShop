using MyShop.Core.Models;
using System.Linq;

namespace MyShop.Core.Contracts
{

    //Concrete implementation
    public interface IRepository<T> where T : BaseEntity
    {
        IQueryable<T> Collection();
        void Commit();
        void Delete(string Id);
        T Find(string Id);
        void Insert(T t);
        void Update(T t);
    }
}