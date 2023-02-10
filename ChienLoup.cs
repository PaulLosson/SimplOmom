using Opc.UaFx.Client;
using Opc.UaFx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace MOM_Santé
{
    internal class ChienLoup
    {
        //Fields
        OpcClient client;



        public ChienLoup(OpcClient _client)
        {
            //Initailizing client
            client = _client;

            
        }

        public void PostPonedCommentSubscribe()
        {
            //Browse Nodes Form NodeBrowser Class
            NodeBrower nodeBrowser = new NodeBrower(client);
            nodeBrowser.BrowsePostPonedComment(nodeBrowser.rootNode);

            System.Diagnostics.Debug.WriteLine(nodeBrowser.OPList.Count().ToString() + nodeBrowser.PostPoneList.Count().ToString());

            foreach (string pp in nodeBrowser.PostPoneList)
            {
                //Conversion of String in Byte array for OPC UA Subscription
                byte[] bytes = Encoding.Default.GetBytes(pp); 

                //Creating subscription for Each PostponedComment
                OpcSubscription subscription = client.SubscribeDataChange(
                pp,
                HandleDataChanged);
            }
        }


        private static void HandleDataChanged(object sender,OpcDataChangeReceivedEventArgs e)
        {
            // The 'sender' variable contains the OpcMonitoredItem with the NodeId.
            OpcMonitoredItem item = (OpcMonitoredItem)sender;

            System.Diagnostics.Debug.WriteLine(
                    "Data Change from NodeId '{0}': {1}",
                    item.NodeId,
                    e.Item.Value);
        }


    }
}
