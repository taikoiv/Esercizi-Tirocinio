using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Apache.NMS;
using Newtonsoft.Json;

namespace PeopleIndex
{
    class Listener : ActiveMQBase
    {
        private const string queueName = "registered";
        private MainWindow win;
        public Listener(MainWindow win) : base()
        {
            this.win = win;
        }

        public void listen()
        {
            c.Start();
            IMessage message;
            IDestination dest = s.GetDestination(queueName);
            using(IMessageConsumer consumer = s.CreateConsumer(dest))
            {
                while (true)
                {
                    message = consumer.Receive();
                    if (message != null)
                    {
                        ITextMessage txtmsg = message as ITextMessage;
                        if (!string.IsNullOrEmpty(txtmsg.Text))
                            win.updateIndex(JsonConvert.DeserializeObject<Person>(txtmsg.Text));
                    }
                }
            }
        }
    }
}
