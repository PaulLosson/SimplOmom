using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Opc.UaFx.Client;
using static System.Windows.Forms.LinkLabel;
using System.Data.Common;
using Org.BouncyCastle.Asn1.X509;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using System.Security.Cryptography.X509Certificates;

namespace MOM_Santé
{
    internal class SqlHoover
    {
        public string dmc;
        private string tracking_id;
        private char part_id;
        private NpgsqlConnection connection;
        public OpDoneList opDoneList = new OpDoneList();

        public SqlHoover(string _tracking_id)
        {
            tracking_id = _tracking_id;
            SqlConnexion();
            GetFromSql();
            this.connection.Close();
        }

        public SqlHoover(char _part_id)
        {
            part_id = _part_id;
            SqlConnexion();
            GetRecipeFromSql();
            Thread.Sleep(1000);
            GetFromSql();
            this.connection.Close();
        }

        private void SqlConnexion()
        {
            //Connexion à KPITR
            string connectionString = "Host=10.57.107.15;" +
                                    "Port=5432;" +
                                    "Database=kpitr;" +
                                    "Username=produser1;" +
                                    "Password=userprod1";
            NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            this.connection = connection;
            try
            {
                this.connection.Open();
            }
            catch (Exception ex) { MessageBox.Show("Problème de connexion à la base de données"); }
        }


        public bool GetRecipeFromSql()
        {
            try
            {
                NpgsqlCommand command_part_id = new NpgsqlCommand("SELECT tracking_id FROM kpitr.part_produce WHERE part_identification = '" + this.part_id + "' and status = 6 and quality_last_modification_id = 'VpiLine' and quality_status = 0 order by time_stamp DESC Limit 1", this.connection);
                NpgsqlDataReader reader = command_part_id.ExecuteReader();
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        if (reader.GetString(i) == null)
                        {
                            MessageBox.Show("Pièce " + this.dmc + " with empty columns");
                        }
                    }
                    //Get last op / ssop
                    this.tracking_id = reader.GetString(0);
                }

                reader.Close();
                command_part_id.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur de l'execution de la requete SQL, pièce : " + this.dmc + Environment.NewLine + " ; message d'erreur: " + ex.Message);
                return false;
            }
        }


        public bool GetFromSql()
        {
            try
            {
                NpgsqlCommand command_last_op = new NpgsqlCommand("SELECT last_op_working, last_ss_op_working, supplier_id FROM kpitr.part_produce WHERE tracking_id ='" + this.tracking_id + "' order by time_stamp DESC;", this.connection);
                NpgsqlDataReader reader = command_last_op.ExecuteReader();
                while (reader.Read())
                {
                    for(int i = 0; i < reader.FieldCount; i++) {
                        if (reader.GetString(i) == null) 
                        {
                            MessageBox.Show("Pièce " + this.dmc + " with empty columns");
                        } 
                    }
                    //Get last op / ssop
                    OpDone opDone = new OpDone(reader.GetString(0), reader.GetString(1));
                    if (opDone != null) { opDoneList.AddOpDone(opDone); }
                    if (this.dmc == null) { this.dmc = reader.GetString(2); }
                }
                reader.Close();
                command_last_op.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur de l'execution de la requete SQL, pièce : " + this.dmc + Environment.NewLine + " ; message d'erreur: " + ex.Message);
                return false;
            }
            
            return true;
        }

        public bool isOpDoneListEmpty()
        {
            OpDoneList opDoneListEmpty = new OpDoneList();
            if (this.opDoneList.opDoneList.Equals(opDoneListEmpty)) { return true; }
            else { return false; }
        }

    }
}