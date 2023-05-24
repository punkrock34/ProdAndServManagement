
namespace ProdAndServManagement.utils
{
    public static class GeneralUtils
    {

        public static bool CheckIfFileValid(string? file)
        {
            if (string.IsNullOrEmpty(file)) return false;
            return true;
        }

    }
}
