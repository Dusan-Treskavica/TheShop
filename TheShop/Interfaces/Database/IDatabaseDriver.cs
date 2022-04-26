using System.Collections.Generic;
using TheShop.Model;

namespace TheShop.Interfaces.Database
{
    public interface IDatabaseDriver
    {
		Article GetById(int id);
		void Save(Article article);
		IList<Supplier> GetSuppliers();
    }
}
