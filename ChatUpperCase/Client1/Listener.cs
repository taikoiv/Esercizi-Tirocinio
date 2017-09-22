using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Apache.NMS;
using Newtonsoft.Json;

namespace Client1
{
    class Listener : ActiveMQBaseConnector
    {
        public const string queueName = "uppercase";
        MainWindow win;

        public Listener(MainWindow w) : base()
        {
            this.win = w;
        }
        public void listen()
        {
            conn.Start();
            IDestination destQ = session.GetDestination(queueName);
            using (IMessageConsumer consumer = session.CreateConsumer(destQ))
            {
                IMessage message;
                while (true)
                {
                    message = consumer.Receive();
                    if (message != null)
                    {
                        ITextMessage textmsg = message as ITextMessage;
                        if(!string.IsNullOrEmpty(textmsg.Text))
                        win.updateText(JsonConvert.DeserializeObject<string>(textmsg.Text));
                    }
                }
            }
        }
    }
}
