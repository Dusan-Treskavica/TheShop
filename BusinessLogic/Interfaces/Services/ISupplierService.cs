using System.Collections.Generic;
using Common.Models;

namespace BusinessLogic.Interfaces.Services
{
    public interface ISupplierService
    {
        /// <summary>
        /// Retrieves suppliers with articles from external service.
        /// </summary>
        /// <returns>Suppliers with articles.</returns>
        IList<Supplier> GetSuppliers();
    }
}
