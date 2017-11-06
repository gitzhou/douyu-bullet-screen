using DouyuBulletScreen.Client;
using DouyuBulletScreen.Util;
using System;
using System.Configuration;
using System.Threading;
using System.Windows.Forms;

namespace DouyuBulletScreen
{
    public partial class MainForm : Form
    {
        private static readonly int KEEP_ALIVE_PERIOD = int.Parse(ConfigurationManager.AppSettings["KeepAlive"]);
        private static readonly int CLEAR_OUTPUT_PERIOD = int.Parse(ConfigurationManager.AppSettings["ClearOutput"]);

        private Thread keepGettingMessage;
        private Thread keepAlive;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            textBoxGroupID.Text = "-9999";

            clearTimer.Interval = CLEAR_OUTPUT_PERIOD * 60 * 1000;
            clearTimer.Enabled = true;

            RichTextBoxLogger logger = RichTextBoxLogger.GetInstance();
            logger.Init(messageRichTextBox);
        }

        private void ButtonConnect_Click(object sender, EventArgs e)
        {
            try
            {
                DouyuBulletScreenClient client = DouyuBulletScreenClient.GetInstance();
                client.Init(int.Parse(textBoxRoomID.Text), int.Parse(textBoxGroupID.Text));

                keepGettingMessage = new Thread(KeepGettingMessage);
                keepGettingMessage.IsBackground = true;
                keepGettingMessage.Start();

                keepAlive = new Thread(KeepAlive);
                keepAlive.IsBackground = true;
                keepAlive.Start();
            }
            catch (FormatException)
            {
                MessageBox.Show("请输入正确的房间号和分组号", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("程序异常，请联系管理员\n" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void KeepGettingMessage()
        {
            DouyuBulletScreenClient client = DouyuBulletScreenClient.GetInstance();
            while (client.IsReady())
            {
                client.GetServerMessage();
            }
        }

        private void KeepAlive()
        {
            DouyuBulletScreenClient client = DouyuBulletScreenClient.GetInstance();
            while (client.IsReady())
            {
                client.KeepAlive();
                Thread.Sleep(KEEP_ALIVE_PERIOD * 1000);
            }
        }

        private void MessageRichTextBox_TextChanged(object sender, EventArgs e)
        {
            messageRichTextBox.SelectionStart = messageRichTextBox.Text.Length; // Set the current caret position at the end
            messageRichTextBox.ScrollToCaret(); // Now scroll it automatically
        }

        private void ClearTimer_Tick(object sender, EventArgs e)
        {
            messageRichTextBox.Clear();
        }
    }
}
