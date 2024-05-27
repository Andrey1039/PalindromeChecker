namespace Server.Data
{
    public static class Palindrome
    {
        // Проверка строки на палиндром
        public static async Task<bool> IsPalindrome(this string text)
        {
            bool result = await Task.Run(() => 
            {
                int left = 0;
                int right = text.Length - 1;

                while (left < right)
                {
                    while (left < right && !char.IsLetterOrDigit(text[left]))
                        left++;
                    while (left < right && !char.IsLetterOrDigit(text[right]))
                        right--;

                    if (char.ToLower(text[left]) != char.ToLower(text[right]))
                        return false;

                    left++;
                    right--;
                }

                return true;
            });

            return result;
        }
    }
}