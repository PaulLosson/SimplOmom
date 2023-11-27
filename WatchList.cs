using MOM_Santé;
using Opc.UaFx.Client;
using System;
using System.Drawing;
using System.Runtime.Serialization.Formatters.Binary;

namespace MOM_Santé
{
    [Serializable]
    internal class WatchList
    {
        //Class describing the entire configuration of Datacollects in a line
        //ppe = PostPonedEnabled value
        //ppc = PostPonedComment value
        public string name;
        public string endpoint;
        public bool autoConnect = false;
        public List<string> opList = new List<string>();
        public List<string> ppeList = new List<string>();
        public List<string> ppcList = new List<string>();
        public List<string> trkList = new List<string>();
        public List<string> dmcList = new List<string>();
        public List<string> typeList = new List<string>();
        public List<string> resokList = new List<string>();
        public List<string> baseNodeList = new List<string>();
        public string sendManualDCNode;

        public WatchList(
            List<string> _opList,
            List<string> _baseNodeList,
            string _sendManualDCNode)
        {
            opList = _opList;
            baseNodeList = _baseNodeList;
            sendManualDCNode = _sendManualDCNode;
        }

        public WatchList(
            List<string> _opList,
            List<string> _baseNodeList,
            string _sendManualDCNode,
            string _endpoint)
        {
            opList = _opList;
            baseNodeList = _baseNodeList;
            endpoint = _endpoint;
            sendManualDCNode = _sendManualDCNode;
        }

        public void FillLists()
        {
            opList = cleanOpList(opList);
            foreach (var baseNode in this.baseNodeList)
            {
                ppeList.Add(baseNode + ".PostponedEnabled");
                ppcList.Add(baseNode + ".PostponedComment");
                trkList.Add(baseNode + ".Part_Id.SerialNum_Emotors_Id.Tracking_Id");
                dmcList.Add(baseNode + ".Part_Id.SerialNum_Emotors_Id.Supplier_Id");
                typeList.Add(baseNode + ".Type");
                resokList.Add(baseNode + ".Result_OK");
            }
        }

        private List<string> cleanOpList(List<string> opList)
        {
            List<string> resultOpList = new List<string>();
            int i = 0;
            foreach (string op in opList)
            {
                resultOpList.Add(op.Replace("4:", ""));
                i++;
            }
            return resultOpList;
        }

        public void SaveParameters()
        {
            //Save in a .dat file the entire object
            FileStream fileStream = new FileStream("config.dat", FileMode.Create);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(fileStream, this);
            fileStream.Close();
        }

        public string getTypeFromOp(string _op)
        {
            foreach (string op in this.opList) { if (op == _op) return typeList[opList.IndexOf(op)]; }
            return null;
        }
    }

    internal class TriggerDC
    {
        //Class Gathering information about current postponed OPs
        public string op;
        public string ppc;
        public string trk;
        public string dmc;

        public event EventHandler ObjectDestroyed;

        public TriggerDC(string _op, string _ppc, string _trk)
        {
            op = _op;
            ppc = _ppc;
            trk = _trk;
        }

        public TriggerDC(string _op, string _ppc, string _trk, string _dmc)
        {
            op = _op;
            ppc = _ppc;
            trk = _trk;
            dmc = _dmc;
        }

        // Method to destroy the object
        public void Destroy()
        {
            ObjectDestroyed?.Invoke(this, EventArgs.Empty);
        }
    }

    internal class TriggerList
    {
        //Class Gathering informations about ALL current postponed OPs
        public List<TriggerDC> triggerList = new List<TriggerDC>();

        public TriggerList()
        {
            
        }

        public void AddTriggerDC(TriggerDC _triggerDC)
        {
            triggerList.Add(_triggerDC);
        }
    }
}