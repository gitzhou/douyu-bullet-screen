using System.Text;

namespace DouyuBulletScreen.Message
{
    public class DouyuEncoder
    {
        private StringBuilder builder = new StringBuilder();

        public string GetResult()
        {
            builder.Append("\0");
            return builder.ToString();
        }

        public void AddItem(string key, object value)
        {
            builder.Append(TextToEscape(key));
            builder.Append("@=");
            if (value is string)
            {
                builder.Append(TextToEscape(value.ToString()));
            }
            else if (value is int)
            {
                builder.Append(value);
            }
            builder.Append("/");
        }

        // 字符串转义
        private static string TextToEscape(string text)
        {
            return text.Replace("/", "@S").Replace("@", "@A");
        }

        // 字符串逆转义
        public static string EscapeToText(string text)
        {
            return text.Replace("@S", "/").Replace("@A", "@");
        }
    }
}
