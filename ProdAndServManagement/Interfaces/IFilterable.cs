using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdAndServManagement.Interfaces
{
    internal interface IFilterable<TObject>
    {
        List<TObject> FilterByPrice(decimal? minPrice, decimal? maxPrice);
        List<TObject> FilterByName(string? name);

        List<TObject> FilterByInternalCode(string? internalCode);
    }
}
