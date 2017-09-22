using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Apache.NMS;
using Newtonsoft.Json;

namespace UpperCaser
{
    class Listener : ActiveMQBaseConnector
    {
        public const string queueName = "lowcase";
        public void listen(Producer p)
        {
            conn.Start();
            IDestination destQ = session.GetDestination(queueName);
            using (IMessageConsumer consumer = session.CreateConsumer(destQ))
            {
                IMessage message;
                while (true)
                {
                    message = consumer.Receive();
                    if (message!=null)
                    {
                        ITextMessage textmsg = message as ITextMessage;
                        if (!string.IsNullOrEmpty(textmsg.Text))
                        {
                            string toSend = JsonConvert.DeserializeObject<string>(textmsg.Text);
                            Console.WriteLine("text to upper : " + toSend);
                            toSend=toSend.ToUpper();
                            Console.WriteLine("text uppercased : " + toSend);
                            p.send(JsonConvert.SerializeObject(toSend));
                        }
                    }
                }
            }
        }
    }
}
