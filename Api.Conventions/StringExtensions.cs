using System;

namespace Api.Conventions
{
    internal static class StringExtensions
    {
        internal static string ToKebabCase(this string s)
        {
            var len = s.Length;
            ReadOnlySpan<char> src = s;
            Span<char> dst = stackalloc char[len * 2];

            int j = 0;
            for (int i = 0; i < len; ++i)
            {
                if (i > 0 && char.IsUpper(src[i]))
                    dst[j++] = '-';
                dst[j++] = char.ToLowerInvariant(src[i]);
            }

            return new string(dst.Slice(0, j));
        }

        internal static string ToSnakeCase(this string s)
        {
            var len = s.Length;
            ReadOnlySpan<char> src = s;
            Span<char> dst = stackalloc char[len * 2];

            int j = 0;
            for (int i = 0; i < len; ++i)
            {
                if (i > 0 && char.IsUpper(src[i]))
                    dst[j++] = '_';
                dst[j++] = char.ToLowerInvariant(src[i]);
            }

            return new string(dst.Slice(0, j));
        }
    }
}
