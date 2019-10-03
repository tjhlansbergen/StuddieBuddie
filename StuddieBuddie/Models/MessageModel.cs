using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StuddieBuddie.Models
{
    public class MessageModel
    {
        public string Message1 { get; set; }
        public string Message2 { get; set; }
        public string Message3 { get; set; }

        //constructor
        public MessageModel(string message1, string message2 = "", string message3 = "")
        {
            Message1 = message1;
            Message2 = message2;
            Message3 = message3;
        }
    }
}