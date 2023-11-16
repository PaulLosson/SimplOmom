using Opc.UaFx.Client;
using Opc.UaFx;
using System;

namespace MOM_Santé
{
    internal class ChienLoup
    {
        OpcClient client;
        private int i = 0;
        public int samplingInterval = 5000;

        public WatchList watchList;

        public ChienLoup(OpcClient _client,
            WatchList _watchList,
            int _samplingInterval = 5000)
        {
            client = _client;
            watchList = _watchList;
            samplingInterval = _samplingInterval;
            System.Diagnostics.Debug.WriteLine("WolfDog created");
        }
        public TriggerList Guard()
        {
            
            TriggerList triggerList = new TriggerList();
            System.Diagnostics.Debug.WriteLine("Guard pooling : " + samplingInterval + " ms");
            i = 0;
            foreach (string ppE in watchList.ppeList)
            {
                while (client.State != OpcClientState.Connected) { Thread.Sleep(200); }
                Boolean ppEnable = false;
                try
                {
                    ppEnable = (bool)client.ReadNode(ppE).Value;
                }
                catch 
                { 
                    MessageBox.Show("La configuration ne correspond pas au serveur sur lequel vous êtes connecté");
                    break;
                }
                if (ppEnable)
                {
                    OpcValue ppComment = client.ReadNode(watchList.ppcList[i]);
                    OpcValue trk = client.ReadNode(watchList.trkList[i]);
                    triggerList.AddTriggerDC(new TriggerDC(watchList.opList[i], (string)ppComment.Value, (string)trk.Value));
                }
                i++;
            }
            return triggerList;
        }
    }
}
