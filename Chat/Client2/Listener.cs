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
        public const string queue = "queue://Client1";
        MainWindow finestra;
        public Listener(MainWindow window) : base()
        {
            finestra = window;
            c.Start();
        }

        public void listen()
        {
            IDestination dest = session.GetDestination(queue);
            using(IMessageConsumer consumer = session.CreateConsumer(dest))
            {
                Console.WriteLine("i'm listening . . . ");
                IMessage text;
                while (true)
                { 
                    text = consumer.Receive();
                    
                    if (text != null)
                    {
                        ITextMessage message = text as ITextMessage;
                        if (!string.IsNullOrEmpty(message.Text))
                        {
                            Console.WriteLine(message.Text);
                            ChatMessage cmsg=JsonConvert.DeserializeObject<ChatMessage>(message.Text);
                            finestra.updateCollection(cmsg);
                        } else
                        {
                            Console.Write("VUOTO!");
                        }
                    }
                }
            }
        }
    }
}
