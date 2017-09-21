using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Apache.NMS;

namespace Client1
{
    class Producer : ActiveMQBaseConnector
    {
        private const string queueName = "lowcase";
        public void send(string message)
        {
            IDestination destQ = session.GetDestination(queueName);
            using(IMessageProducer  producer= session.CreateProducer(destQ))
            {
                IMessage toSend = producer.CreateObjectMessage(message);
                producer.Send(toSend);
            }
        }
    }
}
