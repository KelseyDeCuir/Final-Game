using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterGame
{
    public class Notification
    {
        public string Name { get; set; }
        public Object Object { get; set; }
        public Dictionary<string, Object> UserInfo { get; set; }
        public Notification() : this("NotificationName")
        {
        }

        public Notification(string name) : this(name, null)
        {
        }

        public Notification(string name, Object obj) : this(name, obj, null)
        {
        }

        public Notification(string name, Object obj, Dictionary<string, Object> userInfo)
        {
            this.Name = name;
            this.Object = obj;
            this.UserInfo = userInfo;
        }
    }
}
