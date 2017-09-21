﻿using System;
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
        public const string uri = "activemq:tcp://100.103.0.175:61616";
        //NMS INTERFACES (NOT REAL ACTIVEMQ IMPLEMENTATION)
        public IConnectionFactory connectionFactory;
        public IConnection c;
        public ISession session;

        public ActiveMQConnection()
        {
            connectionFactory = new ConnectionFactory(uri); //real ACTIVEMQ connection factory implementation 
            if (c == null)
            {
                c = connectionFactory.CreateConnection(); //get the real connection 
                session = c.CreateSession();//create a session
            }
        }
    }
}
