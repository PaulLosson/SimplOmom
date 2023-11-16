using MOM_Santé;
using Opc.UaFx.Client;
using System;
using System.Runtime.Serialization.Formatters.Binary;

namespace MOM_Santé
{
    internal class Recipe
    {
        public List<OpDone> opDoneList = new List<OpDone>();

        public Recipe()
        {
            foreach(var opDone in opDoneList)
            {
                //Contient toutes les LastOP//SSOP de la pièce sur laquelle tu clique rebeu
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
}