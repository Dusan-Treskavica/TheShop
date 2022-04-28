using System.Collections.Generic;

namespace Common.Models
{
    public class Supplier
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<SupplierArticle> SupplierArticles { get; set; }
    }
}
