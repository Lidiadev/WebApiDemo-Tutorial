namespace Library.Common
{
    using System;

    public static class CommonFunctions
    {
        public static string GenerateId()
        {
            return Guid.NewGuid().ToString("N");
        }
    }
}
