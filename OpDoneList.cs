using MOM_Santé;
using Opc.UaFx.Client;
using System;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.Serialization.Formatters.Binary;
using static Opc.Ua.RelativePathFormatter;

namespace MOM_Santé
{
    internal class OpDoneList
    {
        public List<OpDone> opDoneList = new List<OpDone>();
        public char partID;

        public OpDoneList()
        {
            
        }
        public void CheckDoublon()
        {
            for (int i = 0; i < this.opDoneList.Count; i++)
            {
                OpDone currentNumber = this.opDoneList[i];
                for (int j = i + 1; j < this.opDoneList.Count; j++)
                {
                    if (this.opDoneList[j].FromSsopToString() == currentNumber.FromSsopToString())
                    {
                        this.opDoneList.RemoveAt(j);
                        j--; // Decrement the index to account for the removed element
                    }
                }
            }
        }
        public void AddOpDone(OpDone _opDone)
        {
            opDoneList.Add(_opDone);
        }
        public void SaveParameters()
        {
            //Save in a .dat file the entire object
            FileStream fileStream = new FileStream("config.dat", FileMode.Create);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(fileStream, this);
            fileStream.Close();
        }
        
    }

    internal class OpDone
    {
        //Class Gathering information about current part story
        public string last_op;
        public string last_ssop;

        public OpDone(string _last_op, string _last_ssop)
        {
            last_op = _last_op;
            last_ssop = _last_ssop;
        }

        public string FromSsopToString() { return this.last_op + "_" + this.last_ssop; }
        public void FromStringToSsop(string checkBox) 
        {
            string[] parts = checkBox.Split('_');
            if (parts.Length == 2)
            {
                this.last_op = parts[0];
                this.last_ssop = parts[1];
            }
        }
       
}
}