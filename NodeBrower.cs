using Opc.UaFx.Client;
using Opc.UaFx;

namespace MOM_Santé
{
    internal class NodeBrower
    {
        //Fields
        //Pasing Parameters
        List<string> OPList = new List<string>();


        public NodeBrower(OpcClient client)
        {
            var objectNode = client.BrowseNode(OpcObjectTypes.ObjectsFolder);

        }

        private void BrowseOP(OpcNodeInfo node, TextBox outputTextBox, int level = 0, string datacollectDataType = "ns=2;i=1304")
        {
            /*
            Check if it's a Datacollect comparing datatypes
            */

            

            string browseName = node.Attribute(OpcAttribute.BrowseName).Value.ToString();
            string dataType = node.AttributeValue(OpcAttribute.DataType).ToString();

            try
            {
                if (dataType != null)
                {
                    if (dataType == datacollectDataType)
                    {
                        //OPList.Add(node.Attribute(OpcAttribute.BrowseName).Value.ToString());
                        outputTextBox.Text = outputTextBox.Text + Environment.NewLine + node.Attribute(OpcAttribute.BrowseName).Value.ToString();
                    }
                }

                else if (browseName != null)
                {
                    if (browseName == "PostponedComment")
                    {
                        //OPList.Add(node.Attribute(OpcAttribute.BrowseName).Value.ToString());
                        outputTextBox.Text = outputTextBox.Text + Environment.NewLine + node.Attribute(OpcAttribute.BrowseName).Value.ToString();
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
        }
    }
}