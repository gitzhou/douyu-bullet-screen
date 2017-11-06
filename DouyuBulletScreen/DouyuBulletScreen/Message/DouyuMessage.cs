using System.Collections.Generic;

namespace DouyuBulletScreen.Message
{
    public class DouyuMessage
    {
        public Dictionary<string, object> MessagePair { get; set; }

        public DouyuMessage(string message)
        {
            MessagePair = ParseMessage(message);
        }

        public Dictionary<string, object> ParseMessage(string message)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            message = message.LastIndexOf('/') != -1 ? message.Substring(0, message.LastIndexOf('/')) : message;
            string[] pairs = message.Split('/');
            foreach (string s in pairs)
            {
                int i = s.IndexOf("@=");
                if (i != -1)
                {
                    string key = s.Substring(0, i);
                    object value = s.Substring(i + 2);
                    if (value.ToString().Contains("@A"))
                    {
                        value = DouyuEncoder.EscapeToText(value.ToString());
                        value = ParseMessage(value.ToString());
                    }
                    dic[key] = value;
                }
            }
            return dic;
        }
    }
}
