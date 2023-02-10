using Opc.UaFx.Client;
using Opc.UaFx;
using Opc.Ua;

namespace MOM_Santé
{
    internal class NodeBrower
    {
        //Fields
        OpcClient client;
        //Pasing Parameters
        public OpcNodeInfo rootNode;

        public List<string> OPList = new List<string>();
        public List<string> PostPoneList = new List<string>();
        
        public NodeBrower(OpcClient _client)
        {
            //Initailizing client
            client = _client;

            //Initializing the root node from the server
            rootNode = client.BrowseNode(OpcObjectTypes.ObjectsFolder);
        }

        public void BrowsePostPonedComment(OpcNodeInfo node, int level = 0, string OPDataType = "ns=2;i=1304", string attributeToFind= "PostponedComment")
        {
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
                                    if (ChildbrowseName.Contains(attributeToFind))
                                    {
                                        System.Diagnostics.Debug.WriteLine(browseName.ToString());
                                        OPList.Add(browseName.ToString());
                                        PostPoneList.Add(childNode.NodeId.ToString());
                                    }
                                }
                            }
                        }
                    }
                }
                level++;
                foreach (var childNode in node.Children())
                    BrowsePostPonedComment(childNode, level);
            }
            catch
            {
                System.Diagnostics.Debug.WriteLine("ERROR");
            }
        }
    }
}