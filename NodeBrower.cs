using Opc.UaFx.Client;
using Opc.UaFx;
using Opc.Ua;

namespace MOM_Santé
{
    /*
    internal class PPOPList
    {
        public List<string> datacollectList = new List<string>();
        public List<string> PostPoneOP = new List<string>();

        public PPOPList()
        {
            datacollectList = new List<string>();
            PostPoneOP = new List<string>();
        }

        public void Append(string OP, string postponeComment)
        {
            datacollectList.Add(OP);
            PostPoneOP.Add(postponeComment);
        }
    }
    */

    internal class NodeBrower
    {
        //Fields
        //Pasing Parameters
        public OpcNodeInfo rootNode;

        public List<string> OPList = new List<string>();
        public List<string> PostPoneList = new List<string>();

        public NodeBrower(OpcClient _client)
        {
            OpcClient client = _client;
            rootNode = client.BrowseNode(OpcObjectTypes.ObjectsFolder);
        }


        public List<string> BrowseOP(OpcNodeInfo node, int level = 0, string datacollectDataType = "ns=2;i=1304")
        {
            /*
            Check if it's a Datacollect comparing datatypes
            */

            try
            {
                if (node.AttributeValue(OpcAttribute.DataType) != null)
                {
                    string browseName = node.Attribute(OpcAttribute.BrowseName).Value.ToString();
                    string dataType = node.AttributeValue(OpcAttribute.DataType).ToString();
                    if (dataType != null)
                    {
                        if (dataType == datacollectDataType)
                        {
                            System.Diagnostics.Debug.WriteLine("{0} TRACEABILITY DATA ADDED", browseName.ToString());
                            OPList.Add(browseName.ToString());
                            
                            System.Diagnostics.Debug.WriteLine("List : {0}", OPList.ToArray().ToString());

                            

                        }
                    }

                    else if (browseName != null)
                    {
                        if (browseName == "PostponedComment")
                        {
                            OPList.Add(browseName.ToString());
                        }
                    }
                }

            level++;

            foreach (var childNode in node.Children())
                BrowseOP(childNode, level);
            }
            catch
            {
                System.Diagnostics.Debug.WriteLine("ERROR");
            }
            return OPList;
        }




        public List<string> BrowsePostPonedComment(OpcNodeInfo node, int level = 0, string datacollectDataType = "ns=2;i=1304")
        {
            /*
            Check if it's a Datacollect comparing datatypes
            */

            try
            {
                if (node.AttributeValue(OpcAttribute.DataType) != null)
                {
                    string browseName = node.Attribute(OpcAttribute.BrowseName).Value.ToString();
                    string dataType = node.AttributeValue(OpcAttribute.DataType).ToString();
                    if (dataType != null)
                    {
                        if (dataType == datacollectDataType)
                        {
                            OpcNodeInfo OPNode = node;
                            foreach(var childNode in OPNode.Children())
                            {
                                if (browseName != null)
                                {
                                    if (browseName == "PostponedComment")
                                    {
                                        OPList.Add(browseName.ToString());
                                        System.Diagnostics.Debug.WriteLine("OP AJOUTED");
                                        PostPoneList.Add(OPNode.Attribute(OpcAttribute.BrowseName).Value.ToString());
                                        System.Diagnostics.Debug.WriteLine("PP AJOUTED");
                                    }
                                }
                            }

                        }
                    }
                }

                level++;

                foreach (var childNode in node.Children())
                    BrowseOP(childNode, level);
            }
            catch
            {
                System.Diagnostics.Debug.WriteLine("ERROR");
            }
            return OPList;
        }

    }
}