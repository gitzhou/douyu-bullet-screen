using DouyuBulletScreen.Util;
using System;

namespace DouyuBulletScreen.Message
{
    public class DouyuClientPackageBuilder
    {
        private static readonly int DOUYU_MESSAGE_TYPE_CLIENT = 689;

        // 生成登录请求数据包
        public static byte[] GetLoginRequestPackage(int roomID)
        {
            DouyuEncoder encoder = new DouyuEncoder();
            encoder.AddItem("type", "loginreq");
            encoder.AddItem("roomid", roomID);
            return BuildPackage(encoder.GetResult());
        }

        // 生成加入弹幕分组池数据包
        public static byte[] GetJoinGroupRequestPackage(int roomID, int groupID)
        {
            DouyuEncoder encoder = new DouyuEncoder();
            encoder.AddItem("type", "joingroup");
            encoder.AddItem("rid", roomID);
            encoder.AddItem("gid", groupID);
            return BuildPackage(encoder.GetResult());
        }

        // 生成心跳协议数据包
        public static byte[] GetKeepAlivePackage()
        {
            DouyuEncoder encoder = new DouyuEncoder();
            encoder.AddItem("type", "mrkl");
            return BuildPackage(encoder.GetResult());
        }

        // 构建协议消息包
        private static byte[] BuildPackage(string data)
        {
            System.IO.MemoryStream stream = new System.IO.MemoryStream();
            try
            {
                // 消息长度
                stream.Write(BitConverter.GetBytes(data.Length + 8), 0, 4);
                // 消息头部，8字节                
                stream.Write(BitConverter.GetBytes(data.Length + 8), 0, 4); // 消息长度，4字节
                stream.Write(BitConverter.GetBytes(DOUYU_MESSAGE_TYPE_CLIENT), 0, 2); // 消息类型，2字节
                stream.WriteByte(0); // 加密字段，1字节
                stream.WriteByte(0); // 保留字段，1字节
                // 数据部
                byte[] dataBytes = System.Text.Encoding.UTF8.GetBytes(data);
                stream.Write(dataBytes, 0, dataBytes.Length);
            }
            catch (Exception e)
            {
                RichTextBoxLogger.GetInstance().Log(e.Message);
            }
            return stream.ToArray();
        }
    }
}
