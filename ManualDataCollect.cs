using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOM_Santé
{
    internal class ManualDataCollect
    {
        public DataCollect dataCollect;
        private string trk { get; set; }
        private string dmc { get; set; }
        private string comp1 { get; set; }
        private string emotors_id { get; set; }
        private string customer_id { get; set; }
        private string num_packaging { get; set; }
        private bool endPack { get; set; }
        private int subOpTypeEnum { get; set; }

        // TODO : Faire un constructeur par Type d'OP
        public ManualDataCollect(string _trk, string _dmc, string _comp1, string _emotorsId, string _customerId, string _numPackaging, bool _endPack, int _subOpTypeEnum)
        {
            this.trk = _trk;
            this.dmc = _dmc;
            this.comp1 = _comp1;
            this.emotors_id = _emotorsId;
            this.customer_id = _customerId;
            this.num_packaging = _numPackaging;
            this.endPack = _endPack;
            this.subOpTypeEnum = _subOpTypeEnum;
        }


        public ManualDataCollect()
        {
        }

        public class SerialNum
        {
            public string custumer_id { get; set; }
            public string emotors_id { get; set; }
            public string supplier_id { get; set; }
            public string tracking_id { get; set; }
        }

        public class PartId
        {
            public string reference { get; set; }
            public SerialNum serial_num { get; set; }
        }

        public class ControlValue
        {
            public string description { get; set; }
            public bool quality_result_nok { get; set; }
            public bool quality_result_ok { get; set; }
            public double result_value { get; set; }
            public double setpoint { get; set; }
            public double tol_max { get; set; }
            public double tol_min { get; set; }
            public string unit { get; set; }
        }

        public class DataCollect
        {
            public bool ack_data { get; set; }
            public string component_1 { get; set; }
            public string component_2 { get; set; }
            public string component_3 { get; set; }
            public double cycle_time_real { get; set; }
            public bool data_av { get; set; }
            public bool end_pack { get; set; }
            public int location { get; set; }
            public string op_num { get; set; }
            public PartId part_id { get; set; }
            public int position { get; set; }
            public string postpone_comment { get; set; }
            public bool postpone_enabled { get; set; }
            public int prog_number { get; set; }
            public bool request_spc_3d { get; set; }
            public bool request_spc_camcpk { get; set; }
            public bool request_spc_change_tool { get; set; }
            public bool request_spc_cmc { get; set; }
            public bool request_spc_cycle_stop { get; set; }
            public bool request_spc_expertise { get; set; }
            public bool request_spc_frequency { get; set; }
            public bool request_spc_manual { get; set; }
            public bool request_spc_opcuamom { get; set; }
            public bool resultat_nok { get; set; }
            public bool resultat_ok { get; set; }
            public int scrap_declaration_enum { get; set; }
            public int subop_type_enum { get; set; }
            public string subop_type { get; set; }
            public List<ControlValue> control_values { get; set; }
        }


        public void DefineSubOpType()
        {
            switch (this.subOpTypeEnum)
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
                    this.dataCollect = new DataCollect
                    {
                        ack_data = false,
                        component_1 = "comp1fgd",
                        component_2 = "comp2",
                        component_3 = "comp3",
                        cycle_time_real = 0.2,
                        data_av = false,
                        end_pack = false,
                        location = 17,
                        op_num = this.,
                        part_id = new PartId
                        {
                            reference = "ref",
                            serial_num = new SerialNum
                            {
                                custumer_id = "",
                                emotors_id = "",
                                supplier_id = "",
                                tracking_id = this.trk
                            }
                        },
                        position = 18,
                        postpone_comment = "",
                        prog_number = 2,
                        resultat_nok = false,
                        resultat_ok = true,
                        subop_type_enum = 4,
                        subop_type = "ee",
                        control_values = new List<ControlValue> { }
                    };
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
