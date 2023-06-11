
using ProdAndServManagement.Products;
using ProdAndServManagement.Services;

namespace ProdAndServManagement.utils
{
    public static class GeneralUtils
    {

        public static bool CheckIfFileValid(string? file)
        {
            if (string.IsNullOrEmpty(file)) return false;
            return true;
        }

        public static List<Type> GetKnownTypes()
        {
            return new List<Type>
            {
                typeof(Product),
                typeof(Service)
                // Add other known types here
            };
        }

    }
}
