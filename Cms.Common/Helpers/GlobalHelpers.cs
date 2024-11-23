using System.Collections;

namespace Cms.Common.Helpers
{
    public static class GlobalHelpers
    {
        public static bool IsNullOrEmpty(this IEnumerable @this)
        {
            if (@this != null)
            {
                return !@this.GetEnumerator().MoveNext();
            }

            return true;
        }
    }
}
