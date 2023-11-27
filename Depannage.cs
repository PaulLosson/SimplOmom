using Microsoft.VisualBasic.ApplicationServices;
using MOM_Santé;
using Newtonsoft.Json;
using Opc.UaFx;
using Opc.UaFx.Client;
using Org.BouncyCastle.Asn1.Crmf;
using System;
using System.Drawing.Text;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;
using System.Xml.Linq;
using static MOM_Santé.ManualDataCollect;
using static Opc.Ua.RelativePathFormatter;

namespace MOM_Santé
{
    // Faire un heritage avec la classe ManualDataCollect
    internal class Depannage
    {
        public string sendManualDCNode;
        public string baseNode;
        public string op;
        public string opNode;
        public string trk;
        public string trkNode;
        public string dmc;
        public string dmcNode;
        public string resok;
        public Int32 type = 4;
        OpcClient client;
        private Form1 form;

        private WatchList watchlist;
        public Depannage()
        {
            
        }

        public Depannage(OpcClient _client, string _op, string _trk, string _dmc, Form1 _form, string _sendManualDCNode)
        {
            op = _op; trk = _trk; dmc = _dmc;
            client = _client;
            this.form = _form;
            sendManualDCNode = _sendManualDCNode;
        }

        public void CallDepannage() 
        {
            this.form.UpdateControlColor(Color.DarkGray);
            ManualDataCollect manualDataCollect = new ManualDataCollect
            {
                trk = trk,
                op_num = op,
                dmc = dmc,
                comp1 = "",
                num_packaging = "",
                endPack = false,
                subOpTypeEnum = 4
            };
            manualDataCollect.DefineSubOpType();
            string jsonDataCollect = System.Text.Json.JsonSerializer.Serialize(manualDataCollect.dataCollect);
            while(client.State != OpcClientState.Connected)
            {
                Thread.Sleep(1000);
            }
            object[] result = client.CallMethod(
                    "ns=4;s=Magnet.ManualDataCollect",
                    "ns=4;s=Magnet.ManualDataCollect.SendManualDataCollect",
                    jsonDataCollect);
            this.form.UpdateControlColor(Color.Green);
            Thread.Sleep(2000);
            this.form.UpdateControlColor(Color.AliceBlue);
            File.WriteAllText(@"C:\Users\JV16065\Desktop\BUFFER\output.json", jsonDataCollect);
        }
    }     
}