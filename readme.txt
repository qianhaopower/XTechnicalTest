XeroTechnicalTest consists of a CSharp console application created using Visual Studio 2013.

If you don't have Visual Studio available download the community edition from https://www.visualstudio.com/en-us/downloads/download-visual-studio-vs.aspx

Instructions for test are listed in the comments at the top of Program.cs.

Good luck!


*******************************************************************************************
Changes summary 
1. Change Invoice.InvoiceNumber from int to string. This will allow more flexible format.
2. Change InvoiceLine.Cost from double to decimal. All currency related property should use decimal as it has more accuracy.
3. Create private variables in Invoice. This will allow initialising like Invoice.LineItems.
4. Create base entity for both Invoice and Invoice line. 
5. Use Autofac to create an IOC. Two type of service (InvoiceBasicService and InvoiceAdvancedService) has been registered on application start up via their interface.
   Program.cs and UnitTest perform all action via those two type of services.
   Basic service performs all original calculation. AdvancedService is similar but allow passing in Invoices/InvoiceLines as parameter. They all inherits from a abstract base service.
6. IInvoiceBasicService and IInvoiceAdvancedService both inherits from IBaseService, which is a IDisposal. See XeroClassDiagram.cd.
7. Add Exception handling. For business logic error we throw XeroException, while other errors we throw normal exception.
8. Add logging using standard .NET logging facility. Setup is in App.config. Log.txt is the log file.
9. Add UnitTest project for all scenarios used for Program.cs. Add Several negative test case.
10. Add some text display to the console to show the progress of the application.
