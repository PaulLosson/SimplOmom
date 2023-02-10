using FastExcel;
using System.Diagnostics;

namespace MOM_Santé
{
    internal class ExcelWriter
    {
        public string targetEquipment = "EMOTORS\\Trémery\\D210\\Mod1\\Magnet\\DataCollect";
        public string instanceName = "OP200";
        public string equipmentClass = "DataslashDataCollect";
        public string displayName = "OP200";
        public int top = 100;
        public int left = 100;
        public string type = "4";
        public string site = "Trémery";
        public string ligne = "Magnet";
        public string dataCollect = "Dt1";
        public string posponed_Variable_Path = "ua:D210_Module1_Magnet_LM\\[http://Emotors/Instance/UMY/D210/Mod1/Magnet]Magnet.Station.IlotPrepaStack.LoadingStack.OP200_01.Traceability_Data.PostponedEnabled";

        public List<string> OPList = new List<string>();
        
        public ExcelWriter()
        {

        }

        public void FastExcelWriteAddRow(FileInfo outputFile)
        {
            /*
             * 
            */

            Console.WriteLine();
            Console.WriteLine("DEMO WRITE 3");

            Stopwatch stopwatch = new Stopwatch();

            using (FastExcel.FastExcel fastExcel = new FastExcel.FastExcel(outputFile))
            {
                Worksheet worksheet = new Worksheet();

                for (int rowNumber = 1; rowNumber < 0; rowNumber++)
                {
                    worksheet.AddRow(1 * DateTime.Now.Millisecond
                                , 2 * DateTime.Now.Millisecond
                                , 3 * DateTime.Now.Millisecond
                                , 4 * DateTime.Now.Millisecond
                                , 45678854
                                , 87.01d
                                , "Test 2" + rowNumber
                                , DateTime.Now.ToLongTimeString());
                }
                stopwatch.Start();
                Console.WriteLine("Writing using AddRow(params object[])...");
                fastExcel.Write(worksheet, "sheet4");
            }

            Console.WriteLine(string.Format("Writing using AddRow(params object[]) took {0} seconds", stopwatch.Elapsed.TotalSeconds));
        }
    }
}