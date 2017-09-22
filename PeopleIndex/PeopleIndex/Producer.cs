using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Apache.NMS;
using Apache.NMS.ActiveMQ;

namespace PeopleIndex
{
    class Producer : ActiveMQBase
    {
        private const string queueName = "surnameUppercase";

        public void send(Person p)
        {
            string toSend = JsonConvert.SerializeObject(p);
            IDestination dest = s.GetDestination(queueName);
            using(IMessageProducer producer = s.CreateProducer(dest))
            {
                ITextMessage message = producer.CreateTextMessage(toSend);
                producer.Send(message);
            }
        }
    }
}
