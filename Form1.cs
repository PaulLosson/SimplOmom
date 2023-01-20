using Microsoft.VisualBasic.Logging;
using System.Net.Sockets;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Opc.UaFx.Client;
using System.Threading;
using Microsoft.VisualBasic.ApplicationServices;
using static System.Net.Mime.MediaTypeNames;
using Opc.UaFx;
using System.ComponentModel.DataAnnotations;
using Opc.Ua;
using System.Xml.Linq;

namespace MOM_Santé
{
    public partial class Form1 : Form
    {
        

        //Parametre connexion serveur
        string endpoint = "Startup";
        OpcClient client;
        private Boolean autoConnect = true;
        private Boolean isConnexion = true;
        

        // Default folder    
        static readonly string rootFolder = @"C:\Temp\Data\";
        //Default file. MAKE SURE TO CHANGE THIS LOCATION AND FILE PATH TO YOUR FILE   
        static readonly string textFile = @"C:\Users\JV16065\Desktop\PreProd local\Line_Middleware_V2\Logs\VpiLine-26000.log";

        //Pasing Parameters
        List<string> OPList = new List<string>();


        public Form1()
        {
            InitializeComponent();
            textBox_endpoint.Text = "opc.tcp://EX0012.inetemotors.com:6011/LineMiddleware1";
            endpoint = textBox_endpoint.Text;
            if (autoConnect)
            {
                client = new OpcClient(endpoint);
                client.Connect();
                //dotConnexionThread.Abort();
                button_connexion.Text = "Connecté";
                button_connexion.BackColor = Color.Green;
            }
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            //Thread dotConnexionThread = new Thread(new ThreadStart(ThreadDotConnexion));
            //dotConnexionThread.Start();
            // Read entire text file content in one string  
            //string text = File.ReadAllText(textFile);
            //textBox_Log.Text = text;

            if(isConnexion)
            {
                Connexion("tt");
            }
            else
            {
                Console.WriteLine("Deconnexion du serveur avant fermeture ...");
                client.Disconnect();
                textBox_Log.Text = "Deonnecté";
                button_connexion.Text = "Déconnecté";
                button_connexion.BackColor = Color.Red;
            }
        isConnexion = !isConnexion;
        }

        public void Connexion(string _endpoint)
        {
            try
            {
                _endpoint = textBox_endpoint.Text;
                if (client == null)
                {
                    client = new OpcClient(_endpoint);
                }
                else
                {
                    Console.WriteLine("Merci d'arreter d'essayer de vous connecter plusieurs fois au serveur");
                }

                client.Connect();
                //dotConnexionThread.Abort();
                button_connexion.Text = "Connecté";
                button_connexion.BackColor = Color.Green;
            }
            catch
            {
                Console.WriteLine("Problème de connection au serveur");
                for(int i = 0; i < 2; i++)
                {
                    Connexion(_endpoint);
                }
            }
        }

        /*
        public void ThreadDotConnexion()
        {
            String dots = ".";

            for (int i = 0; i < 3; i++)
            {
                
                button_connexion.Invoke(new MethodInvoker(delegate
                {
                    //button_connexion.Text = dots;
                }));
                dots = dots + ".";
                Thread.Sleep(250);
            }
        }
        */

        private void button_find_Click(object sender, EventArgs e)
        {
            //var objectNode = client.BrowseNode(OpcObjectTypes.ObjectsFolder);
            //BrowseOP(objectNode);

            //CLASS//
            
            NodeBrower nodeBrowser = new NodeBrower(client);

            OPList = nodeBrowser.BrowsePostPonedComment(nodeBrowser.rootNode);
            OPList = nodeBrowser.OPList;
            int i= 1;
            foreach(string op in nodeBrowser.OPList)
            {
                System.Diagnostics.Debug.WriteLine(op);
                //System.Diagnostics.Debug.WriteLine(nodeBrowser.PostPoneList[i]);
                i++;
            }
        }

        
        private void BrowseOP(OpcNodeInfo node, int level = 0,string datacollectDataType = "ns=2;i=1304")
        {
            /*
            Check if it's a Datacollect comparing datatypes
            */

            string browseName = node.Attribute(OpcAttribute.BrowseName).Value.ToString();

            try
            {  
                if (node.AttributeValue(OpcAttribute.DataType) != null)
                {
                    string dataType = node.AttributeValue(OpcAttribute.DataType).ToString();
                    if (dataType == datacollectDataType)
                    {
                        //OPList.Add(node.Attribute(OpcAttribute.BrowseName).Value.ToString());
                        textBox_Log.Text = textBox_Log.Text + Environment.NewLine + node.Attribute(OpcAttribute.BrowseName).Value.ToString();
                    }

                    if (node.Attribute(OpcAttribute.BrowseName).Value.ToString() != null)
                    {
                        if (browseName == "PostponedComment")
                        {
                            //OPList.Add(node.Attribute(OpcAttribute.BrowseName).Value.ToString());
                            textBox_Log.Text = textBox_Log.Text + Environment.NewLine + node.Attribute(OpcAttribute.BrowseName).Value.ToString();
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
        }

    }
}