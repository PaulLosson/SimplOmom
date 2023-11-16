SET client_encoding TO 'UTF8';

SET search_path = kpitr;

DROP TYPE kpitr."job_statusenum" CASCADE;
CREATE TYPE kpitr."job_statusenum" AS ENUM ('sad', 'ok', 'happy');


DROP TABLE kpitr.erp_of_type;

CREATE TABLE kpitr.ERP_OF_Type  (
   Reference varchar,

   Customer_Id varchar,
   Emotors_Id varchar,
   Supplier_Id varchar,
   Tracking_Id varchar, 

   End_Time_Theo timestamp,
   Line_Requested varchar,
   Quantity integer,
   
   Recipes_Number_Requested varchar,
   
   Start_Time_Theo timestamp,
   End_Time_Real timestamp,

   Cycle_Time_Max float4,
   Cycle_Time_Mean float4,
   Cycle_Time_Min float4,
   Cycle_Time_Real float4,
   Cycle_Time_Theo float4,
   Nb_Part_In_Progress int,
   Nb_Part_Out int,
   Nb_Part_produced int,
   Nb_Part_Scrap int,
   Nb_Part_Theo int,
   OEE float4,
   Performance_Rate float4,
   Quality_Rate float4,
   T_O float4,
   Total_NVA float4,
   TR_div_TEP float4,  
   TRO float4,
   TRS float4,
   TU float4,
   Nb_Quality_Control_Nok int,
   Nb_Quality_Control_OK int,
   Nb_Quality_Control_Planned int,
   Nb_Quality_Control_Real int,
   Rate_Quality_Control float4,

   Line_Affected  varchar,
   Line_Identification  varchar,

   Part_Identification  varchar,
   Plant_Identification  varchar,

   Quantity_Engaged int,
   Quantity_Produced int,
   Quantity_Scrapped int, 
   
   
   Start_Time_Real timestamp,

   Status_Change_Date timestamp,  
   Status_Change_Owner varchar,
   
   Status int,

   Type_Identification  varchar,

   Time_stamp timestamp

   
      
) TABLESPACE "USR_KPITR";


CREATE INDEX idx_erp_of_type_Emotors_Id ON erp_of_type (Emotors_Id) TABLESPACE "IDX_KPITR";
CREATE INDEX idx_erp_of_type_Line_Requested ON erp_of_type (Line_Requested) TABLESPACE "IDX_KPITR";
CREATE INDEX idx_erp_of_type_Time_stamp ON erp_of_type (Time_stamp) TABLESPACE "IDX_KPITR";


DROP TABLE kpitr.Part_Produce ;

CREATE TABLE kpitr.Part_Produce   (

End_Time_Real timestamp,
ERP_OF_Id varchar,
Exit_by_OP varchar,

Total_VA float4,
VA_NVA_Rate float4,
Last_OP_Working varchar,
Last_SS_OP_Working varchar,

Line_Identification varchar,
Part_Identification varchar,
Plant_Identification varchar,
Type_Identification varchar,



Part_Packaging_Identification_Number varchar,
Layer Int,
Position int,

Reference varchar,
Customer_Id varchar,
Emotors_Id varchar,
Supplier_Id varchar,
Tracking_Id varchar, 
	

Quality_Comment varchar,   
Quality_Last_Modification_Id varchar,  
Quality_Status int,  

Start_Time_Real timestamp,

Status_Date timestamp,  
Status_Owner varchar,  
Status int,  
Line_Real varchar,
Cycle_Time_Theo float4,
Time_stamp timestamp 


) TABLESPACE "USR_KPITR";

CREATE INDEX idx_Part_Produce_ERP_OF_Id ON Part_Produce (ERP_OF_Id) TABLESPACE "IDX_KPITR";
CREATE INDEX idx_Part_Produce_Reference ON Part_Produce (Reference) TABLESPACE "IDX_KPITR";
CREATE INDEX idx_Part_Produce_eMotors_Customer_Id ON Part_Produce (Customer_Id) TABLESPACE "IDX_KPITR";
CREATE INDEX idx_Part_Produce_Emotors_Id ON Part_Produce (Emotors_Id ) TABLESPACE "IDX_KPITR";
CREATE INDEX idx_Part_Produce_Supplier_Id ON Part_Produce (Supplier_Id) TABLESPACE "IDX_KPITR";
CREATE INDEX idx_Part_Produce_Tracking_Id ON Part_Produce (Tracking_Id) TABLESPACE "IDX_KPITR";
CREATE INDEX idx_Part_Produce_Cycle_Time_Theo ON Part_Produce (Cycle_Time_Theo) TABLESPACE "IDX_KPITR";
CREATE INDEX idx_Part_Produce_Time_stamp ON Part_Produce (Time_stamp) TABLESPACE "IDX_KPITR";


DROP TABLE kpitr.OP_Traceability  ;

CREATE TABLE kpitr.OP_Traceability    (

Cycle_Time float4,
Description varchar,
End_Time timestamp,
Operation varchar,
Operation_Progress int,
Operation_Quality_Status int,

Start_time timestamp,

Part_Id varchar,
Cycle_Time_Theo  float4, 
Time_stamp timestamp  



) TABLESPACE "USR_KPITR";

CREATE INDEX idx_OP_Traceability_Time_stamp ON OP_Traceability (Time_stamp) TABLESPACE "IDX_KPITR";
CREATE INDEX idx_OP_Traceability_Operation ON OP_Traceability (Operation) TABLESPACE "IDX_KPITR";
CREATE INDEX idx_OP_Traceability_Part_Id ON OP_Traceability (Part_Id) TABLESPACE "IDX_KPITR";
CREATE INDEX idx_OP_Traceability_Cycle_Time_Theo ON OP_Traceability (Cycle_Time_Theo) TABLESPACE "IDX_KPITR";


DROP TABLE kpitr.Sub_Op_Traceability  ;

CREATE TABLE kpitr.Sub_Op_Traceability    (
Cycle_Time float4,
End_Time timestamp, 

Attemps_Rate float4,
Attemps_Real int,
Attemps_Theo int,
Cycle_Time_Real float4,
Cycle_Time_Theo float4,

Nb_Attemp_Real int,
Progress_Status int,
Quality_status int,

SS_Op_Trigger int,
SS_op_type varchar,
SubOP_Name varchar,



Start_time timestamp, 

Tracability_SubAssembly varchar, 

Frequency int,
Instruction_Sheet varchar,
Program_Number_Theo int,
RessourceId varchar,
SS_OP_Description varchar,
Sub_Op_Control Boolean,
VA_NVA int,

Part_Id varchar,
OP_Name varchar,
Time_stamp timestamp  

) TABLESPACE "USR_KPITR";


CREATE INDEX idx_Sub_Op_Traceability_Time_stamp ON Sub_Op_Traceability (Time_stamp) TABLESPACE "IDX_KPITR";
CREATE INDEX idx_Sub_Op_Traceability_Part_Id ON Sub_Op_Traceability (Part_Id) TABLESPACE "IDX_KPITR";
CREATE INDEX idx_Sub_Op_Traceability_Cycle_Time_Theo ON Sub_Op_Traceability (Cycle_Time_Theo) TABLESPACE "IDX_KPITR";
CREATE INDEX idx_Sub_Op_Traceability_OP_Name ON Sub_Op_Traceability (OP_Name) TABLESPACE "IDX_KPITR";
CREATE INDEX idx_Sub_Op_Traceability_SubOP_Name ON Sub_Op_Traceability (SubOP_Name) TABLESPACE "IDX_KPITR";



DROP TABLE kpitr.Sub_Op_Recipe_Consumed_Components;

CREATE TABLE kpitr.Sub_Op_Recipe_Consumed_Components(
Component varchar,
Component_Id varchar,  
Component_Quantity float4,
Sub_Part Boolean,
Unit_Quantity varchar,

Part_Id varchar,
OP_Name varchar,
Sous_OP_Name varchar,
From_Sub_Assembly Boolean,      

Time_stamp timestamp



) TABLESPACE "USR_KPITR";


CREATE INDEX idx_Consumed_Components_Part_Id ON Sub_Op_Recipe_Consumed_Components (Part_Id) TABLESPACE "IDX_KPITR";

DROP TABLE kpitr.SubOp_Result  ;

CREATE TABLE kpitr.SubOp_Result   (
Cycle_Time float4,
Program_Number_Real int,
Quality_result_Ok boolean,
Tracability_Component1 varchar,	
Tracability_Component2 varchar,	
Tracability_Component3 varchar,	

Part_Id varchar,
OP_Name varchar,
Sous_OP_Name varchar,
Try_number int,      
Time_stamp timestamp

) TABLESPACE "USR_KPITR";

CREATE INDEX idx_SubOp_Result_Time_stamp ON SubOp_Result (Time_stamp) TABLESPACE "IDX_KPITR";
CREATE INDEX idx_SubOp_Result_Part_Id ON SubOp_Result (Part_Id) TABLESPACE "IDX_KPITR";
CREATE INDEX idx_SubOp_Result_OP_Name ON SubOp_Result (OP_Name) TABLESPACE "IDX_KPITR";
CREATE INDEX idx_SubOp_Result_Sous_OP_Name ON SubOp_Result (Sous_OP_Name) TABLESPACE "IDX_KPITR";
CREATE INDEX idx_SubOp_Result_Try_number ON SubOp_Result (Try_number) TABLESPACE "IDX_KPITR";

DROP TABLE kpitr.Sub_Op_Control_Value  ;

CREATE TABLE kpitr.Sub_Op_Control_Value   (
Description varchar,

Quality_Comment varchar,  
Quality_Last_Modification_Id varchar,  
Quality_Status int,  
Result_Value float4,
Setpoint float4,
Tol_Max float4,
Tol_Min float4,
Unit varchar,

Part_Id varchar,
OP_Name varchar,
Sous_OP_Name varchar,
Try_number int,      

Time_stamp timestamp



) TABLESPACE "USR_KPITR";

CREATE INDEX idx_Sub_Op_Control_Value_Time_stamp ON Sub_Op_Control_Value (Time_stamp) TABLESPACE "IDX_KPITR";
CREATE INDEX idx_Sub_Op_Control_Value_Part_Id ON Sub_Op_Control_Value (Part_Id) TABLESPACE "IDX_KPITR";
CREATE INDEX idx_Sub_Op_Control_Value_OP_Name ON Sub_Op_Control_Value (OP_Name) TABLESPACE "IDX_KPITR";
CREATE INDEX idx_Sub_Op_Control_Value_Sous_OP_Name ON Sub_Op_Control_Value (Sous_OP_Name) TABLESPACE "IDX_KPITR";
CREATE INDEX idx_Sub_Op_Control_Value_Try_number ON Sub_Op_Control_Value (Try_number) TABLESPACE "IDX_KPITR";

DROP TABLE kpitr.Operations  ;
CREATE TABLE kpitr.Operations(
Line varchar,
MotorType varchar,
OP varchar,
Sub_OP varchar
) TABLESPACE "USR_KPITR";

DROP TABLE kpitr.ERP_OF_CACHE  ;
CREATE TABLE kpitr.ERP_OF_CACHE(
Reference varchar,
BatchNumber varchar,
Customer_Reference varchar,
End_Time_Theo timestamp,
Line_Requested varchar,
Quantity int,
Recipe_number_Requested varchar,
Software_version varchar,
Start_Time_Theo timestamp,
Status int
) TABLESPACE "USR_KPITR";