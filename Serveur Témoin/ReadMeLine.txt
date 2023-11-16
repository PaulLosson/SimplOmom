Line Server Sprint 6 28-09-2021

Versions 

Binaries in Delivery :
	- Core server 1.0.6.0
	- VpiLine 1.4.0.0
	- VpiUaClient 1.0.2.0
	- Structures eMotors 1.3.0.0

Environment :
	- OPC UA Model 2.9
	- Ximulator 1.0.1.9v1.0.2.8
	- ConfigManager 0.9.8
	- PLCSIM scenario v5
---------------------------------------------------------------------------

Functionalities 

	
	
	Data Collect for storage in a single ErpOf Erp_Of_List_To_Do (maxed out at 40 PartProduces)

	Collect of ERP OFs from Plant by updating ERP OF Counter
	Collect of Recipes from the Plant when new recipes are available or at server line startup.
 	Data Collect for storage in an ERP OF that has come from the Plant



	Supported Data Collects : Init_Part, Init_Sub_Assembly, Traceability, Traceability_Sub_Assembly, Traceability_EmotorsId, Traceability_CustomerId, Part_Declaration
	Data Request on the PartProduces that are still in Address Space
	Traceability in PostGres Database (Traceability/KPI database)
-------------------------------------------------------------------
Setup 

	VpiLine mapping and setup
	
		In Project folder, there is a config file named Opc.Ua.SubSystem.VpiLineDebug. 
		It already contains the mapping with 5 Data Collect FB and 1 Data Request FB. 
		In case you want to add a new Data Collect or Request FB: 
			Instantiate an instance of the FB in a new namespace following the model of already present instances
			In VpiLine config file, add the new namespace and mapping for the new instance with index -LineX. 
			For the DataCollect FB, make sure to link the concerned MachineType Variable with the -MacX attribute in the attributes address 

		In project folder there is a file called VpiLine-01000.dat which configures the VpiLine:

			TRACE_LEVEL (DEBUG, ERROR or NONE) Specifies the trace level of the VpliLine
			ACTIVATE_TRACEABILITY (TRUE or FALSE) Specifies if traceability in KpiTr Postgres Database is activated
			ACTIVATE_RECIPE_MANAGEMENT (TRUE or FALSE) Specifies if VpiLine verifies that Recipe is present and that the OPs are in correct order
			SECURITY_PURGE (TRUE or FALSE) Specifies if the ServerLine deletes PartProduces in ErpOfs and SubAssambly list if bigger than 40
			ACTIVATE_STANDALONE_RECIPE (TRUE or FALSE) Specifies if some recipes should be simulated
			ACTIVATE_STANDALONE_ERPOF (TRUE or FALSE) Specifies if some ERP OFs should be simulated and if all PartProduces should be stored in this first ERP Ofs
			

	VpiUaClnt mapping

		In Project folder, there is 3 config files named UaClnt-001-00301.dat, UaClnt-001-00301-0.xml and SubSystem.Uaclient-001_Release.xml
		
		- UaClnt-001-00301.dat contains the name of the server connection configuration file (UaClnt-001-00301-0.xml) 
		- UaClnt-001-00301-0.xml contains the connection information and subscriptions 
		- SubSystem.Uaclient-001_Release.xml contains the mapping with the server instances
		
		- For the last two files, make sure that the trigger variables ( DataAvailable and AcknowledgeData for FBDC) and ( DataRequested and DataPresent for FBDR)
		are at the end of the mappings for each instances. 
	
	Database Traceability Setup

		Traceability can be activated for storing in a Postgres Database. 
		- Configuration script for database is KPITAB_V7.sql 
		- Database connection information for the VpiLine is in a file called url.txt 
		  Format : host=<host Machine ip or hostname> port=<port number for postgres> dbname=<database name> user=<user name> password=<user password> connect_timeout=<in seconds, period for connection timeout>


------------------------------------------------------------------
Startup

Launch StartServerDebug.bat 

For Dump analysis in case of crash Launch ProcDump.bat after launching StartServerDebug.bat
