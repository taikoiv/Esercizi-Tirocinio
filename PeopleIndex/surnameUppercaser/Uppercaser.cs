using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Apache.NMS.ActiveMQ;
using Apache.NMS;

namespace surnameUppercaser
{
    class Uppercaser : ActiveMQBase
    {
        private const string readQueue = "surnameUppercase";
        private const string writeQueue = "registered";

        public void realMain()
        {
            c.Start();
            IDestination readDest = s.GetDestination(readQueue);
            using(IMessageConsumer consumer = s.CreateConsumer(readDest))
            {
                IMessage message;
                while (true)
                {
                    message = consumer.Receive();
                    if (message != null)
                    {
                        ITextMessage txtmsg = message as ITextMessage;
                        if (!string.IsNullOrEmpty(txtmsg.Text))
                        {
                            Person p = JsonConvert.DeserializeObject<Person>(txtmsg.Text);
                            Console.WriteLine("old surname = " + p.Surname);
                            p.Surname = p.Surname.ToUpper();
                            Console.WriteLine("new surname = " + p.Surname);
                            this.send(p);
                        }
                    }
                }
            }
        }

        public void send(Person p)
        {
            string toSend = JsonConvert.SerializeObject(p);
            IDestination writeDest = s.GetDestination(writeQueue);
            using(IMessageProducer producer = s.CreateProducer(writeDest))
            {
                ITextMessage message = producer.CreateTextMessage(toSend);
                producer.Send(message);
            }
        }
    }
}
