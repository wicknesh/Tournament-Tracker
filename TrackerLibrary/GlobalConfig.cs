﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary
{
    public static class GlobalConfig
    {
        public static List<IDataConnection> Connections { get; private set; } = new List<IDataConnection>();
        public static void InitializeConnections(bool dataBase, bool textFile)
        {
            if (dataBase)
            {
                //TODO - Setup the SQL connector properly.
                SQLConnector sql = new SQLConnector();
                Connections.Add(sql);
            }

            if (textFile)
            {
                //TODO - Create the Text Connection
                TextConnector text = new TextConnector();
                Connections.Add(text);
            }
        }
    }
}
