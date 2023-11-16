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


        public Depannage(OpcClient _client, string _baseNode, string _op, string _trk, string _dmc, Form1 _form, string _sendManualDCNode)
        {
            op = _op; trk = _trk; dmc = _dmc;
            baseNode = _baseNode;
            client = _client;
            this.form = _form;
            sendManualDCNode = _sendManualDCNode;
        }

        public void SetDepannage(OpcClient _client, string _baseNode, string _op, string _trk, string _dmc, Form1 _form, string _sendManualDCNode)
        {
            op = _op; trk = _trk; dmc = _dmc;
            baseNode = _baseNode;
            client = _client;
            this.form = _form;
            sendManualDCNode = _sendManualDCNode;
        }

        public void CallDepannage() 
        {
            this.form.UpdateControlColor(Color.DarkGray);
            DataCollect manualDataCollect = new DataCollect
            {
                ack_data = false,
                component_1 = "comp1fgd",
                component_2 = "comp2",
                component_3 = "comp3",
                cycle_time_real = 0.2,
                data_av = false,
                end_pack = false,
                location = 17,
                op_num = "OP203-01",
                part_id = new PartId
                {
                    reference = "ref",
                    serial_num = new SerialNum
                    {
                        custumer_id = "",
                        emotors_id = "",
                        supplier_id = "",
                        tracking_id = trk
                    }
                },
                position = 18,
                postpone_comment = "",
                prog_number = 2,
                resultat_nok = false,
                resultat_ok = true,
                subop_type_enum = 4,
                subop_type = "ee",
                control_values = new List<ControlValue> {}
            };
            ManualDataCollect manual = new ManualDataCollect();
            
            manual.DefineSubOpType();
            string jsonDataCollect = System.Text.Json.JsonSerializer.Serialize(manualDataCollect);
            MessageBox.Show(jsonDataCollect);
            while(client.State != OpcClientState.Connected)
            {
                Thread.Sleep(1000);
            }
            object[] result = client.CallMethod(
                    "ns=4;s=Rotor.ManualDataCollect",
                    "ns=4;s=Rotor.ManualDataCollect.SendManualDataCollect",
                    jsonDataCollect);
            MessageBox.Show(result[0].ToString());
            this.form.UpdateControlColor(Color.Green);
            Thread.Sleep(1000);
            this.form.UpdateControlColor(Color.AliceBlue);
            // Write the JSON string to a file
            File.WriteAllText(@"C:\Users\JV16065\Desktop\BUFFER\output.json", jsonDataCollect);
            MessageBox.Show("depannage envoyé");
        }

        //TODO : Mettre au propre cette fonction dans une autre classe
        public void DefineSubOpType(DataCollect dataCollect, int type)
        {
            switch (type)
            {
                case 0:
                    // Code for Type 0
                    break;
                case 1:
                    // Code for Type 1
                    break;
                case 2:
                    // Code for Type 2
                    break;
                case 3:
                    // Code for Type 3
                    break;
                case 4:
                    dataCollect.
                    break;
                case 5:
                    // Code for Type 5
                    break;
                case 6:
                    // Code for Type 6
                    break;
                case 7:
                    // Code for Type 7
                    break;
                default:
                    // Default case or error handling
                    break;
            }
        }
    }     
}