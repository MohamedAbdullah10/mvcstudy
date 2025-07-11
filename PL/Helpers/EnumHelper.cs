namespace PL.Helpers
{
    public static class EnumHelper 
    {
        public static IEnumerable<string> GetNames<T>() where T:Enum { 
         return Enum.GetNames(typeof(T)).ToList();
        }
    }
}
