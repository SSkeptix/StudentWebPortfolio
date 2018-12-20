using StudentWebPortfolio.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentWebPortfolio.Web.Helpers
{
    public class MessageBox
    {
        protected MessageBox() { }

        public string Text { get; set; }
        public MessageType MessageType { get; set; }
        public bool IsAutoHide { get; set; } = true;

        public static MessageBox Success(string text)
            => new MessageBox { Text = text, MessageType = MessageType.Success };
        public static MessageBox Info(string text)
            => new MessageBox { Text = text, MessageType = MessageType.Info };
        public static MessageBox Warning(string text)
            => new MessageBox { Text = text, MessageType = MessageType.Warning };
        public static MessageBox Error(string text)
            => new MessageBox { Text = text, MessageType = MessageType.Error, IsAutoHide = false };
    }

    public enum MessageType
    {
        [StringValue("success")] Success, 
        [StringValue("info")]    Info,
        [StringValue("warning")] Warning, 
        [StringValue("danger")]  Error,
    }
}
