using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Apache.NMS;
using Apache.NMS.ActiveMQ;

namespace PeopleIndex
{
    abstract class ActiveMQBase
    {
        private const string activeMQURI = "tcp://localhost:61616";
        private IConnectionFactory factory;
        protected IConnection c;
        protected ISession s;

        public ActiveMQBase()
        {
            factory = new ConnectionFactory(activeMQURI);
            c = factory.CreateConnection();
            s = c.CreateSession();
        }
    }
}
