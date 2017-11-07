using DouyuBulletScreen.Message;
using DouyuBulletScreen.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Sockets;
using System.Text;

namespace DouyuBulletScreen.Client
{
    public class DouyuBulletScreenClient
    {
        private static readonly string HOST = ConfigurationManager.AppSettings["Host"];
        private static readonly int PORT = int.Parse(ConfigurationManager.AppSettings["Port"]);
        private static readonly int BUFFER_SIZE = int.Parse(ConfigurationManager.AppSettings["BufferSize"]);

        private ILogger logger = RichTextBoxLogger.GetInstance();

        private Socket socket;

        private bool isReady = false;

        private static DouyuBulletScreenClient instance;

        private DouyuBulletScreenClient() { }

        public static DouyuBulletScreenClient GetInstance()
        {
            if (instance == null)
            {
                instance = new DouyuBulletScreenClient();
            }
            return instance;
        }

        public void Init(int roomID, int groupID)
        {
            if (ConnectServer() && LoginRoom(roomID) && JoinGroup(roomID, groupID))
            {
                isReady = true;
            }
        }

        public bool IsReady()
        {
            return isReady;
        }

        // 连接弹幕服务器
        private bool ConnectServer()
        {
            bool result = false;
            try
            {
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(HOST, PORT);
                result = true;
                logger.Log("连接弹幕服务器...成功");
            }
            catch (Exception e)
            {
                logger.Log("连接弹幕服务器...失败");
                logger.Log(e.Message);
            }
            return result;
        }

        // 登录指定房间
        private bool LoginRoom(int roomID)
        {
            bool result = false;
            try
            {
                socket.Send(DouyuClientPackageBuilder.GetLoginRequestPackage(roomID));
                byte[] response = new byte[BUFFER_SIZE];
                socket.Receive(response);
                if (ParseLoginResponse(response))
                {
                    result = true;
                    logger.Log("登录直播间...成功");
                }
                else
                {
                    logger.Log("登录直播间...失败");
                }
            }
            catch (Exception e)
            {
                logger.Log("登录直播间...失败");
                logger.Log(e.Message);
            }
            return result;
        }

        // 解析登录请求返回结果
        private static bool ParseLoginResponse(byte[] response)
        {
            bool result = false;
            if (response.Length > 12) // 包含12位信息头和信息内容
            {
                string data = System.Text.Encoding.UTF8.GetString(response);
                if (data.Contains("type@=loginres"))
                {
                    result = true;
                }
            }
            return result;
        }

        // 加入弹幕分组池
        private bool JoinGroup(int roomID, int groupID)
        {
            bool result = false;
            try
            {
                socket.Send(DouyuClientPackageBuilder.GetJoinGroupRequestPackage(roomID, groupID));
                result = true;
                logger.Log("加入弹幕分组池...成功");
            }
            catch (Exception e)
            {
                logger.Log("加入弹幕分组池...失败");
                logger.Log(e.Message);
            }
            return result;
        }

        // 心跳连接
        public void KeepAlive()
        {
            try
            {
                socket.Send(DouyuClientPackageBuilder.GetKeepAlivePackage());
            }
            catch (Exception e)
            {
                logger.Log("发送心跳包...失败");
                logger.Log(e.Message);
            }
        }

        // 获取服务器返回的消息
        public void GetServerMessage()
        {
            try
            {
                byte[] response = new byte[BUFFER_SIZE];
                int responseLength = socket.Receive(response);
                if (responseLength > 12)
                {
                    string message = Encoding.UTF8.GetString(response, 12, responseLength - 12);
                    while (message.LastIndexOf("type@=") > 5)
                    {
                        ParseServerMessage(new DouyuMessage(message.Substring(message.LastIndexOf("type@="))).MessagePair);
                        message = message.Substring(0, message.LastIndexOf("type@=") - 12);
                    }
                    message = message.LastIndexOf("type@=") != -1 ? message.Substring(message.LastIndexOf("type@=")) : message;
                    ParseServerMessage(new DouyuMessage(message).MessagePair);
                }
            }
            catch (Exception e)
            {
                logger.Log(e.Message);
            }
        }

        // 解析服务器返回的消息
        private void ParseServerMessage(Dictionary<string, object> message)
        {
            if (message.ContainsKey("type") && message["type"] != null)
            {
                // 服务器反馈错误信息
                if (message["type"].ToString() == "error")
                {
                    logger.Log(message.ToString());
                    isReady = false; // 结束心跳和获取弹幕线程
                }

                /* TODO 根据业务需求来处理获取到的所有弹幕及礼物信息 */

                // 判断消息类型
                if (message["type"].ToString() == "chatmsg" && message.ContainsKey("nn") && message.ContainsKey("txt")) // 弹幕消息
                {
                    logger.Log(string.Format("[{0}] {1}", message["nn"], message["txt"]));
                }
            }
        }
    }
}
