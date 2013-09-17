namespace SmartElk.ElkMate.Common.Ex
{
    public static class StringEx
    {
        public static string CombinePath(this string left, string right)
        {
            if (right == null)
                return left;
            if (left == null)
                return right;
            return System.IO.Path.Combine(left, right);
        }
    }
}
