using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Apache.NMS;

namespace Client1
{
    class Listener : ActiveMQBaseConnector
    {
        public const string queueName = "lowcase";
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
                    //TO DO
                }
            }
        }
    }
}
