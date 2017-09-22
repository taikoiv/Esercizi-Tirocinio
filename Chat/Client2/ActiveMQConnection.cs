using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Apache.NMS;
using Apache.NMS.ActiveMQ;

namespace Chat
{
    public class ActiveMQConnection
    {
        //URI TO ACTIVE MQ
        public const string uri = "activemq:tcp://localhost:61616";
        //NMS INTERFACES (NOT REAL ACTIVEMQ IMPLEMENTATION)
        public IConnectionFactory connectionFactory;
        public IConnection c;
        public ISession session;

        public ActiveMQConnection()
        {
            connectionFactory = new ConnectionFactory(uri); //real ACTIVEMQ implementation 
            if (c == null)
            {
                c = connectionFactory.CreateConnection();
                session = c.CreateSession();
            }
        }
    }
}
