using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Apache.NMS;
using Apache.NMS.ActiveMQ;

namespace UpperCaser
{
    abstract class ActiveMQBaseConnector
    {
        protected const string uri = "tcp://localhost:61616";
        protected IConnectionFactory factory;
        protected IConnection conn;
        protected ISession session;

        protected ActiveMQBaseConnector()
        {
            factory = new ConnectionFactory(uri);
            conn = factory.CreateConnection();
            session = conn.CreateSession();
        }
    }
}
