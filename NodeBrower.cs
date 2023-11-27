using Opc.UaFx.Client;
using Opc.UaFx;
using Opc.Ua;
using System.ComponentModel;
using System.Xml.Linq;

namespace MOM_Santé
{
    internal class NodeBrower
    {
        //Fields
        OpcClient client;
        //Pasing Parameters
        public OpcNodeInfo rootNode;

        public List<string> OPList = new List<string>();
        public List<string> PostPoneEList = new List<string>();
        public List<string> PostPoneList = new List<string>();
        public List<string> trkList = new List<string>();

        //Todestroy
        public List<string> baseNodeList = new List<string>();
        public List<string> typeList = new List<string>();

        public NodeBrower(OpcClient _client)
        {
            //Initailizing client
            client = _client;

            //Initializing the root node from the server
            rootNode = client.BrowseNode(OpcObjectTypes.ObjectsFolder);
        }

        public WatchList FindGlobalParameters(OpcNodeInfo node, int level = 0, string OPDataType = "ns=2;i=1304")
        {
            string sendManualDCNode = "";
            /*
            Check if it's a Datacollect comparing datatypes
            Then get the NodeId of PostponedComment
            Fill 2 lists
            One for the OP Name
            One for the corresponding NodeId of PPComment
            */
            try
            {
                if (node.AttributeValue(OpcAttribute.DataType) != null)
                {
                    string browseName = node.Attribute(OpcAttribute.BrowseName).Value.ToString();
                    string dataType = node.AttributeValue(OpcAttribute.DataType).ToString();
                    if (dataType != null)
                    {
                        if (dataType == OPDataType)
                        {

                            foreach (var childNode in node.Children())
                            {
                                string ChildbrowseName = childNode.Name.ToString();
                                if (ChildbrowseName != null)
                                {
                                    if (ChildbrowseName.Contains("PostponedEnabled"))
                                    {
                                        //OPNumber
                                        OPList.Add(node.Parents().ToList()[0].Name.ToString());
                                        //baseNode
                                        baseNodeList.Add(node.NodeId.ToString());
                                    }
                                    else if (ChildbrowseName.Contains("SendManualDataCollect"))
                                    {
                                        sendManualDCNode = childNode.NodeId.ToString();
                                    }
                                }
                            }
                        }
                    }
                }
                level++;
                foreach (var childNode in node.Children())
                    FindOP(childNode, level);
            }
            catch
            {
                System.Diagnostics.Debug.WriteLine("ERROR");
            }

            WatchList watchList = new WatchList(OPList, baseNodeList, sendManualDCNode);
            return watchList;
        }

        public WatchList FindOP(OpcNodeInfo node, int level = 0, string OPDataType = "ns=2;i=1304", string ppeAttributeToFind = "PostponedEnabled", string ppcAttributeToFind= "PostponedComment", string trkAttributeToFind = " ")
        {
            string sendManualDCNode = "";
            /*
            Check if it's a Datacollect comparing datatypes
            Then get the NodeId of PostponedComment
            Fill 2 lists
            One for the OP Name
            One for the corresponding NodeId of PPComment
            */
            try
            {
                if (node.AttributeValue(OpcAttribute.DataType) != null)
                {
                    string browseName = node.Attribute(OpcAttribute.BrowseName).Value.ToString();
                    string dataType = node.AttributeValue(OpcAttribute.DataType).ToString();
                    if (dataType != null)
                    {
                        if (dataType == OPDataType)
                        {
                                  
                            foreach(var childNode in node.Children())
                            {
                                string ChildbrowseName = childNode.Name.ToString();
                                if (ChildbrowseName != null)
                                {
                                    if (ChildbrowseName.Contains(ppeAttributeToFind))
                                    {
                                        //OPNumber
                                        OPList.Add(node.Parents().ToList()[0].Name.ToString());
                                        //baseNode
                                        baseNodeList.Add(node.NodeId.ToString());
                                    }
                                    else if(ChildbrowseName.Contains("SendManualDataCollect")) 
                                    { 
                                        sendManualDCNode = childNode.NodeId.ToString();
                                    }
                                }
                            }
                        }
                    }
                }
                level++;
                foreach (var childNode in node.Children())
                    FindOP(childNode, level);
            }
            catch
            {
                System.Diagnostics.Debug.WriteLine("ERROR");
            }

            WatchList watchList = new WatchList(OPList, baseNodeList, sendManualDCNode);
            return watchList;
        }
        
        /*
        private void BrowsePostPonedComment(OpcNodeInfo node, OpcNodeInfo[] datacollectAttributes, ref string sendManualDCNode)
        {
            string ppeAttributeToFind = "PostponedEnabled";
            foreach (var childNode in datacollectAttributes)
            {
                string ChildbrowseName = childNode.Name.ToString();
                if (ChildbrowseName != null)
                {
                    if (ChildbrowseName.Contains(ppeAttributeToFind))
                    {
                        //OPNumber
                        OPList.Add(node.Parents().ToList()[0].Name.ToString());
                        //baseNode
                        baseNodeList.Add(node.NodeId.ToString());
                    }
                    else if (ChildbrowseName.Contains("SendManualDataCollect"))
                    {
                        sendManualDCNode = childNode.NodeId.ToString();
                    }
                }
            }
        }
        */

    }
}