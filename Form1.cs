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
using Org.BouncyCastle.Asn1.Cms;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using System.Runtime.Serialization.Formatters.Binary;
using static Org.BouncyCastle.Math.EC.ECCurve;
using MOM_Santé;
using System.Reflection.Emit;
using Label = System.Windows.Forms.Label;
using System.Windows.Forms.VisualStyles;
using static Opc.Ua.RelativePathFormatter;

namespace MOM_Santé
{
    public partial class Form1 : Form
    {
        //Parametre connexion serveur
        OpcClient client;
        private Boolean isConnexion = false;

        WatchList watchListGlobal;

        //Chien Loup management
        bool chienLoupExist = false;
        bool watchEnded = false;
        Thread guardThread;

        //Depannage Management
        OpDoneList recipe = new OpDoneList();
        Depannage depannage;
        Thread threadDepannage;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button_LoadParameter.PerformClick();
            if (watchListGlobal != null)
            {
                //Enabled_WatchListGlobal_controls(false);
                autoConnectToolStripMenuItem.Enabled = true;
                autoConnectToolStripMenuItem.Checked = watchListGlobal.autoConnect;
                textBox_endpoint.Text = watchListGlobal.endpoint;
                if (watchListGlobal.autoConnect)
                {
                    button_connexion.PerformClick();
                    button_ChienLoup.PerformClick();
                }
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            button_connexion.Enabled = false; // Disable the button during connection attempt
            if (!isConnexion)
            {
                await ConnexionAsync();
            }
            else
            {
                watchEnded = true;
                client.Disconnect();
                button_connexion.Text = "Déconnecté";
                button_connexion.BackColor = Color.Red;
                isConnexion = !isConnexion;
            }
            button_connexion.Enabled = true; // Re-enable the button after connection attempt
        }

        public async Task ConnexionAsync()
        {
            try
            {
                if (client == null || client.State == OpcClientState.Created)
                {
                    client = new OpcClient(textBox_endpoint.Text);
                }
                else
                {
                    if (client.UsedEndpoint.ToString() != textBox_endpoint.Text)
                    {
                        client = null;
                        client = new OpcClient(textBox_endpoint.Text);
                    }
                    else if (client.State == OpcClientState.Connected)
                    {
                        Log("Merci d'arrêter d'essayer de vous connecter plusieurs fois au serveur");
                        return; // No need to continue if already connected
                    }
                }

                client.Connect(); // Assuming ConnectAsync doesn't take any arguments
                button_connexion.Text = "Connecté";
                button_connexion.BackColor = Color.Green;
                isConnexion = !isConnexion;
            }

            catch (Exception e)
            {
                Log("Problème de connexion au serveur " + e.Message.ToString());
                button_connexion.Text = "XXXXXXX";
            }
        }

        private void button_find_Click(object sender, EventArgs e)
        {
            button_ChienLoup.Enabled = false;
            button_depannage.Enabled = false;
            button_connexion.Enabled = false;
            button_LoadParameter.Enabled = false;
            button_SaveParameters.Enabled = false;
            if (client != null)
            {
                //Objets faisant le parsing des nodes, puis abonnement sur le PostPonedComments//
                NodeBrower nodeBrowser = new NodeBrower(client);

                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                Button button = (Button)sender;
                button.Text = "Looking for OPs";
                button.BackColor = Color.Brown;
                WatchList watchList = nodeBrowser.FindOP(nodeBrowser.rootNode);
                watchList.FillLists();
                button.Text = "Found";
                button.BackColor = Color.AntiqueWhite;
                stopwatch.Stop();
                Log("Durée de l'aspiration de la config OOUA " + stopwatch.Elapsed.TotalSeconds.ToString() + " secondes");
                watchListGlobal = watchList;
                Enabled_WatchListGlobal_controls(true);
            }
            else
            {
                MessageBox.Show("Connectez vous avant de faire la recherche des OPs");
            }

            button_ChienLoup.Enabled = true;
            button_depannage.Enabled = true;
            button_connexion.Enabled = true;
            button_LoadParameter.Enabled = true;
            button_SaveParameters.Enabled = true;
        }

        void Log(string message)
        {
            // Execute the delegate on the UI thread
            try
            {
                textBox_Log.Invoke((MethodInvoker)delegate
                {
                    textBox_Log.Text += message + "\r\n";
                });
            }
            catch (Exception e)
            {

            }
        }

        private void button_SaveParameters_Click(object sender, EventArgs e)
        {
            if (watchListGlobal == null)
            {
                MessageBox.Show("Pas de configuration à sauvegarder");
            }
            else
            {
                watchListGlobal.endpoint = textBox_endpoint.Text;
                if (File.Exists("config.dat"))
                {
                    DialogResult dialogResult = MessageBox.Show("Il y a déjà un fichier de configuration existant, voulez vous le supprimer ?", "Sauvegarde", MessageBoxButtons.YesNoCancel);
                    if (dialogResult == DialogResult.Yes)
                    {
                        button_SaveParameters.BackColor = System.Drawing.Color.Brown;
                        watchListGlobal.SaveParameters();
                        MessageBox.Show("Configuration Saved successfully");
                        button_SaveParameters.BackColor = System.Drawing.Color.AliceBlue;
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        //do something else
                    }
                }

                else
                {
                    button_SaveParameters.BackColor = System.Drawing.Color.Brown;
                    watchListGlobal.SaveParameters();
                    MessageBox.Show("Configuration Saved successfully");
                    button_SaveParameters.BackColor = System.Drawing.Color.AliceBlue;
                }

            }
        }

        private void button_LoadParameter_Click(object sender, EventArgs e)
        {

            if (!File.Exists("config.dat"))
            {
                if (watchListGlobal == null)
                {
                    return;
                }
                MessageBox.Show("Pas de fichier de configuration disponible");
            }
            else
            {
                FileStream fileStream = new FileStream("config.dat", FileMode.Open);
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                watchListGlobal = (WatchList)binaryFormatter.Deserialize(fileStream);
                fileStream.Close();
                button_LoadParameter.BackColor = System.Drawing.Color.Green;
                Enabled_WatchListGlobal_controls(true);
            }
        }

        private void button_ChienLoup_Click(object sender, EventArgs e)
        {
            if (client.State == OpcClientState.Disconnected) { MessageBox.Show("Veuillez vous connecter avant"); }
            else
            {
                if (chienLoupExist)
                {
                    watchEnded = true;
                    chienLoupExist = false;
                    Log("Recherche des défauts en arret demandé par l'utilisateur");
                }
                else
                {
                    if (watchListGlobal == null) { MessageBox.Show("Veuillez charger une configuration avant de lancer"); }
                    else
                    {
                        Action<TriggerList> updateTextDelegate = new Action<TriggerList>((TriggerList triggerList) =>
                        {

                            foreach (Control control in Controls)
                            {
                                if (control is Label l && l.Name.Contains("label_PP_"))
                                    Controls.Remove(l);
                            }
                            string print = "";
                            int i = 0;
                            foreach (var triggerDC in triggerList.triggerList)
                            {
                                triggerDC.ppc = Translate(triggerDC);
                                print = triggerDC.op + " : " + triggerDC.ppc + " : " + triggerDC.trk + Environment.NewLine;
                                i++;
                                Label label = new Label
                                {
                                    Text = print,
                                    Location = new System.Drawing.Point(216, 80 + i * 30),
                                    AutoSize = true,
                                    BackColor = Color.White,
                                    Name = "label_PP_" + i.ToString()
                                };
                                label.Click += (sender, e) => Label_Click(sender, e, i, triggerDC);
                                Controls.Add(label);
                            }
                        });

                        ChienLoup wouf = new ChienLoup(client, watchListGlobal);
                        chienLoupExist = true;
                        watchEnded = false;
                        guardThread = new Thread(() =>
                        {
                            Log("Recherche des défauts activé");
                            while (!watchEnded)
                            {
                                // execute the delegate on the UI thread
                                this.Invoke(updateTextDelegate, wouf.Guard());
                                Thread.Sleep(wouf.samplingInterval);
                            }
                            Log("Recherche des défauts désactivé");
                            watchEnded = false;
                            Thread.CurrentThread.Join();
                        });
                        guardThread.Start();
                    }
                }
            }
        }

        private void Label_Click(object sender, EventArgs e, int i, TriggerDC triggerDC)
        {
            //Clear les anciens labels
            if (checkedListBox_op_done.Items.Count != 0)
            {
                checkedListBox_op_done.Items.Clear();
            }

            //Affectation du DMC si trk vide
            if (triggerDC.trk == "")
            {
                if (triggerDC.dmc == null)
                {
                    //Chercher directement la valeur du DMC dans le node de l'OP en defaut
                }
                else
                {
                    textBox_DMC_Input.Text = triggerDC.dmc.ToString();
                }
            }

            char partId = GetPartID(triggerDC.trk);
            string str = "";
            SqlHoover sqlHooverRecipe = new SqlHoover(partId);
            if (sqlHooverRecipe.opDoneList.opDoneList != null)
            {
                recipe = sqlHooverRecipe.opDoneList;
                recipe.CheckDoublon();
                foreach (OpDone ssop in recipe.opDoneList)
                {
                    checkedListBox_op_done.Items.Add(ssop.FromSsopToString());
                }
            }

            str = "";
            SqlHoover sqlHoover = new SqlHoover(triggerDC.trk);
            sqlHoover.opDoneList.CheckDoublon();

            //TrackinID Sans 
            if (sqlHoover.opDoneList.opDoneList.Count == 0)
            {
                //TODO Si la pièce n'existe pas proposer la premiere OP avec bon DMC + TRK
                MessageBox.Show("La piece n'existe pas !");
                //!depannage.subOP = 0 !!!!
                textBox_OP_Input.Text = watchListGlobal.opList[0];
                try
                {
                    textBox_OP_Input.Text = recipe.opDoneList.Last().FromSsopToString();
                }
                catch (Exception)
                {
                    Log("Pas de TrackingId correct" + triggerDC.op);
                    return;
                }

                textBox_TRK_Input.Text = triggerDC.trk;
                textBox_DMC_Input.Text = sqlHoover.dmc;
                depannage = new Depannage();
                depannage.type = 0;
                depannage = new Depannage(client, watchListGlobal.opList[0], triggerDC.trk, sqlHoover.dmc, this, watchListGlobal.sendManualDCNode);

                return;
            }
            OpDone last_op = sqlHoover.opDoneList.opDoneList[0];

            //Check des OP DonesS
            if (sqlHoover.opDoneList.opDoneList != null)
            {
                foreach (OpDone currentOp in sqlHoover.opDoneList.opDoneList)
                {
                    int index = 0;
                    foreach (OpDone recipeOp in recipe.opDoneList)
                    {
                        if (currentOp.FromSsopToString() == recipeOp.FromSsopToString())
                        {
                            checkedListBox_op_done.SetItemChecked(checkedListBox_op_done.Items.IndexOf(recipeOp.FromSsopToString()), true);
                        }
                        if (last_op.FromSsopToString() == recipeOp.FromSsopToString())
                        {
                            textBox_OP_Input.Text = checkedListBox_op_done.Items[index].ToString();
                        }
                        index++;
                    }
                }
            }

            //Prevision Depannage
            int opADepannerIndex = watchListGlobal.opList.FindIndex(op => op.Contains(textBox_OP_Input.Text));
            if (opADepannerIndex != -1)
            {
                textBox_OP_Input.Text = watchListGlobal.opList[opADepannerIndex];
                textBox_TRK_Input.Text = triggerDC.trk;
                textBox_DMC_Input.Text = sqlHoover.dmc;

                depannage = new Depannage(client, watchListGlobal.opList[opADepannerIndex], triggerDC.trk, sqlHoover.dmc, this, watchListGlobal.sendManualDCNode);
            }
            else
            {
                Log("ERROR");
                //TODO
                //MessageBOx Erreur
            }

        }

        private char GetPartID(string _trackingId)
        {
            int partIdPosition = 11;
            if (partIdPosition >= 0 && partIdPosition < _trackingId.Length)
            {
                char partId = _trackingId[partIdPosition];

                if (char.IsLetter(partId) && char.IsUpper(partId))
                {
                    return partId;
                }
                else
                {
                    MessageBox.Show("Impossible de retrouver le type de pièce " + partId + ", y a t il un trackingid correct ?");
                    return 'X';
                }
            }
            else
            {
                MessageBox.Show("Impossible d'interpréter le trackingID de l'OP en défaut, y a t il un trackingid correct ?");
                return 'X';
            }
        }
        
        private string Translate(TriggerDC triggerDC)
        {
            switch (triggerDC.ppc)
            {
                case "NOTENGAGEMENT : trackingId not found":
                    {
                        return "La pièce n'as pas été créer, elle n'a pas reçu la première OP";
                        break;
                    }
                case "TRACEABILITY_SUBASSEMBLY trackingId(COMP1) not Found":
                    {
                        return "Le composant de cette pièce n'as pas été créer";
                        break;
                    }
                case "TRACEABILITY_SUBASSEMBLY trackingId(COMP1) not Completed":
                    {
                        return "Le composant de cette pièce n'as pas été déclaré par sa dernière OP";
                        break;
                    }
                case "DECLARATION: not SP and OP_Traceability != Completed  ou != Good":
                    {
                        return "Cette pièce ne peux pas se finir car elle possède une OP NOK";
                        break;
                    }
            }
            return triggerDC.ppc;
        }

        private void button_depannage_Click(object sender, EventArgs e)
        {
            if (depannage == null)
            {
                Log("Start depannage");
                depannage = new Depannage(client, textBox_OP_Input.Text, textBox_TRK_Input.Text, textBox_DMC_Input.Text, this, watchListGlobal.sendManualDCNode);
                Thread threadDepannage = new Thread(depannage.CallDepannage);
                threadDepannage.Start();
            }
            else if (client.State != OpcClientState.Connected)
            {
                Thread.Sleep(1000);
                Log("Le serveur est n'est pas dans un bon état pour pouvoir lancer un dépannage, merci de le relancer");
                button_depannage_Click(sender, e);
            }
            else
            {
                depannage = new Depannage(client, textBox_OP_Input.Text, textBox_TRK_Input.Text, textBox_DMC_Input.Text, this, watchListGlobal.sendManualDCNode);
                Thread threadDepannage = new Thread(depannage.CallDepannage);
                threadDepannage.Start();
            }
        }

        public void UpdateControlText(string newText)
        {
            if (label_DMC_input.InvokeRequired)
            {
                // Use Invoke to safely update the control from a different thread
                button_depannage.Invoke(new Action<string>(UpdateControlText), newText);
            }
            else
            {
                button_depannage.Text = newText;
            }
            MessageBox.Show(newText);
        }

        public void UpdateControlColor(Color newColor)
        {
            if (label_DMC_input.InvokeRequired)
            {
                // Use Invoke to safely update the control from a different thread
                button_depannage.Invoke(new Action<Color>(UpdateControlColor), newColor);
            }
            else
            {
                button_depannage.BackColor = newColor;
            }
        }

        private void Enabled_WatchListGlobal_controls(bool isEnable)
        {
            Color newColor;
            if (!isEnable) { newColor = Color.Gray; }
            else { newColor = Color.Coral; }
            autoConnectToolStripMenuItem.Enabled = isEnable;
            button_ChienLoup.Enabled = isEnable;
            button_depannage.Enabled = isEnable;
            button1_Fichier.Enabled = isEnable;
            button1_Fichier.BackColor = newColor;
            button_ChienLoup.BackColor = newColor;
            button_depannage.BackColor = newColor;
            autoConnectToolStripMenuItem.BackColor = Color.Gray;
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            watchEnded = true;
            if (client != null)
            {
                if ((client.State != OpcClientState.Disconnecting) || (client.State != OpcClientState.Disconnected))
                {
                    client.Disconnect();
                }
            }
            if(threadDepannage!=null)
            {
                if (threadDepannage.ThreadState == System.Threading.ThreadState.Running)
                {
                    threadDepannage.Abort();
                }
            }
            if (guardThread != null)
            {
                if (guardThread.ThreadState == System.Threading.ThreadState.Running)
                {
                    guardThread.Abort();
                }
            }

        }

        private void contextMenuStrip_menu_Opening(object sender, CancelEventArgs e)
        {

        }

        private void button1_Fichier_Click(object sender, EventArgs e)
        {
            contextMenuStrip_menu.Show(button_LoadParameter, 0, 0);
        }

        private void autoConnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            autoConnectToolStripMenuItem.Checked = !autoConnectToolStripMenuItem.Checked;
            watchListGlobal.autoConnect = autoConnectToolStripMenuItem.Checked;
        }
    }
}