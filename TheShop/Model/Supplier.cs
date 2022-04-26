using System.Collections.Generic;

namespace TheShop.Model
{
    public class Supplier
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<Article> Articles { get; set; }
    }
}
