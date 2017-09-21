using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Apache.NMS;

namespace Chat
{
    class Publisher : ActiveMQConnection
    {
        public const string queue = "queue://Client2"; //uri to client1 publishing queue
        public void publish(string message)
        {
            IDestination dest = session.GetDestination(queue); //get the destination queue from the session
            using(IMessageProducer producer = session.CreateProducer(dest)) //OPEN AND USE DESTINATION STREAM FOR PRODUCER 
            {
                IMessage text = producer.CreateTextMessage(message); //STRING IS NOW A VALID MESSAGE TO BE SENT
                producer.Send(text);
            }

        }
    }
}
