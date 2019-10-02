namespace Hexalyser.Extensions
{
    public static class StringExtensions
    {
        public static string AddCharacter(this string s, char ch, int count = 1)
        {
            for (int i = 0; i < count; i++)
            {
                s += ch;
            }
            return s;
        }
    }
}
