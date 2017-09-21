using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Apache.NMS;
using Newtonsoft.Json;

namespace Chat
{
    class Listener : ActiveMQConnection
    {
        public const string queue = "queue://Client2"; //queue to listen uri
        MainWindow finestra;
        public Listener(MainWindow window) : base()
        {
            finestra = window;
        }

        public void listen()
        {
            c.Start(); //NEEDED TO START THE CONNECTION TO ACTIVEMQ
            IDestination dest = session.GetDestination(queue);
            using(IMessageConsumer consumer = session.CreateConsumer(dest)) //create the stream to dequeue messages
            {
                IMessage text;
                while (true)
                {
                    text = consumer.Receive(); //dequeue dest
                    if (text != null)
                    {
                        ITextMessage message = text as ITextMessage;
                        if (!string.IsNullOrEmpty(message.Text))
                        {
                            ChatMessage cmsg=JsonConvert.DeserializeObject<ChatMessage>(message.Text); //RETRIVE OBJECT FROM JSON STRING
                            finestra.updateCollection(cmsg);
                        }
                    }
                }
            }
        }
    }
}
