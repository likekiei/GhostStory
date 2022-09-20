using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GhostStory.Models;

namespace GhostStory.Hubs
{
    public class SignalRHub : Hub
    {
        /// <summary>
        /// 將資料接收的方法
        /// </summary>
        /// <param name="name">使用者註冊的名稱</param>
        /// <param name="message">使用者傳遞的訊息</param>
        public void Send(string name, string message)
        {
            //將使用者端傳來的資料推播給所有使用者
            Clients.All.addNewMessageToPage(name , message, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

        }
        public void Hello()
        {
            Clients.All.hello();
        }
    }
}