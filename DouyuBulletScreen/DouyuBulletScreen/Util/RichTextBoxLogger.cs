using System;
using System.Windows.Forms;

namespace DouyuBulletScreen.Util
{
    public class RichTextBoxLogger
    {
        private static RichTextBoxLogger instance;

        private RichTextBoxLogger() { }

        public static RichTextBoxLogger GetInstance()
        {
            if (instance == null)
            {
                instance = new RichTextBoxLogger();
            }
            return instance;
        }

        private RichTextBox richTextBox;

        public void Init(RichTextBox richTextBox)
        {
            this.richTextBox = richTextBox;
        }

        public void Log(string log)
        {
            if (richTextBox.InvokeRequired)
            {
                Action<string> actionDelegate = (x) => { richTextBox.Text += x.ToString() + "\n"; };
                richTextBox.Invoke(actionDelegate, log);
            }
            else
            {
                richTextBox.Text += log + "\n";
            }
        }
    }
}
