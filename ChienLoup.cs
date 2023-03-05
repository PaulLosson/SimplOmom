using Opc.UaFx.Client;
using Opc.UaFx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;                                           
using System.ComponentModel;
using System.Windows.Forms;

namespace MOM_Santé
{
    internal class ChienLoup : OpcDataChangeReceivedEventArgs
    {
        public List<string> resultList = new List<string>();
        public bool Cancel { get; set; }

        /*
        public ChienLoup(string nodeid,OpcDataChangeReceivedEventHandler handler, List<string> resultList)
        {
            this.resultList = resultList;
        }
        */
    }

    public bool fireEvent()
    {
        ChienLoup e = new ChienLoup();

        //Don't forget a null check, assume this is an event
        FireEventHandler(this, e);

        return e.Cancel;
    }

    public void HandleFireEvent(object sender, ChienLoup e)
    {
        e.Cancel = true;
    }
}
