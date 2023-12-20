using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.DataAccess;

namespace TrackerLibrary
{
    public static class GlobalConfig
    {
        /*public static List<IDataConnection> Connections { get; private set; } = new List<IDataConnection>();*/
        public static IDataConnection Connection { get; private set; }
        public static void InitializeConnections(bool textFile)
        {
            /*if (dataBase)
            {
                //TODO - Setup the SQL connector properly.
                SQLConnector sql = new SQLConnector();
                Connections.Add(sql);
            }*/

            if (textFile)
            {
                //TODO - Create the Text Connection
                TextConnector text = new TextConnector();
                /*Connections.Add(text);*/
                Connection = text;
            }
        }

        /*public static string CnnString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }*/
    }
}
