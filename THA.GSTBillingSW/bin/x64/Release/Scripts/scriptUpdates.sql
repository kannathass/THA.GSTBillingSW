USE [THAgstBilling]
GO
--CREATE TABLE [dbo].[MasterBank](
--	[ID] [int] IDENTITY(1,1) NOT NULL,
--	[CompanyID] [int] NULL,
--	[BankName] [varchar](100) NULL,
--	[Branch] [varchar](100) NULL,
--	[AccountNumber] [varchar](20) NULL,
--	[IFSC] [varchar](20) NULL,
--	BankStatus varchar(10),
--	[Del_State] [bit] NULL,
--	[Del_Date] [datetime] NULL
--) ON [PRIMARY]

--GO

--ALTER TABLE [dbo].[MasterBank] ADD  CONSTRAINT [DF_MasterBank_Del_State]  DEFAULT ((0)) FOR [Del_State]
--GO

--CREATE TABLE [dbo].[TranPaymentCollection](
--	[ID] [bigint] IDENTITY(1,1) NOT NULL,
--	[CompanyID] [int] NULL,
--	[ReceiptNumber] [varchar](20) NULL,
--	[ReceiptDate] [date] NULL,
--	[CustomerID] [int] NULL,
--	[PaymentMode] [varchar](50) NULL,
--	[PaymentReference] [varchar](100) NULL,
--	[TotalPayment] [decimal](16, 2) NULL,
--	[PaidToAccountID] [int] NULL, --Not used
--	[Notes] [varchar](500) NULL,
--	[CreatedBy] [varchar](50) NULL,
--	[CreatedOn] [datetime] NULL,
--	[Del_State] [bit] NULL,
--	[Del_Date] [datetime] NULL
--) ON [PRIMARY]

--GO

--ALTER TABLE [dbo].[TranPaymentCollection] ADD  CONSTRAINT [DF_TranPaymentCollection_Del_State]  DEFAULT ((0)) FOR [Del_State]
--GO

--CREATE TABLE [dbo].[TranPaymentCollectionList](
--	[ID] [bigint] IDENTITY(1,1) NOT NULL,
--	[HeaderID] [bigint] NULL,
--	[InvoiceID] [bigint] NULL,
--	[InvoiceNumber] [varchar](20) NULL,
--	[InvoiceDate] date,
--	[InvoiceAmount] [decimal](16, 2) NULL,
--	[Balance] [decimal](16, 2) NULL,
--	[AmountReceived] [decimal](16, 2) NULL,
--	[Discount] [decimal](10, 2) NULL,
--	[Interest] [decimal](10, 2) NULL,
--	[CFItem1] [varchar](20) NULL,
--	[CFItem2] [varchar](20) NULL,
--	[CFItem3] [varchar](20) NULL,
--	[CFItem4] [varchar](20) NULL,
--	[Del_State] [bit] NULL,
--	[Del_Date] [datetime] NULL
--) ON [PRIMARY]

--GO

--ALTER TABLE [dbo].[TranPaymentCollectionList] ADD  CONSTRAINT [DF_TranPaymentCollectionList_Del_State]  DEFAULT ((0)) FOR [Del_State]
--GO

---- =============================================      
---- Author:  TK      
---- Create date: 22-Feb-2017  
---- Description: To Get auto sequnce number for Accounts.      
---- =============================================      
--CREATE PROCEDURE [dbo].[GetAutoSequenceNumber_Accounts]      
-- @CompanyId int,      
-- @DocumentType varchar(100)     
--AS      
--BEGIN      
-- SET NOCOUNT ON;      
--	if(@DocumentType='Payment Collection')      
--    Begin      
--      select ISNULL(max(ID),0)+1 AutoNumber from [TranPaymentCollection] where CompanyID=1     
--	End          
--END 

--GO
--CREATE PROCEDURE [dbo].[Trans_PaymentList_Save]            
--(@HeaderID bigint  
--,@InvoiceID bigint  
--,@InvoiceNumber varchar(20)  
--,@InvoiceDate date  
--,@InvoiceAmount DECIMAL(16,2)  
--,@Balance DECIMAL(16,2)  
--,@AmountReceived DECIMAL(16,2)  
--,@Discount DECIMAL(10,2)  
--,@Interest DECIMAL(10,2)  
--,@CFItem1 varchar(20)=''  
--,@CFItem2 varchar(20)=''  
--,@CFItem3 varchar(20)=''  
--,@CFItem4 varchar(20)=''             
--,@mode varchar(50)      )      
--AS            
--BEGIN            
-- SET NOCOUNT ON;            
-- if (@mode = 'PaymentCollection-Insert')            
-- Begin            
  
--INSERT INTO [dbo].[TranPaymentCollectionList]  
--           ([HeaderID],[InvoiceID],[InvoiceNumber]  
--           ,[InvoiceDate],[InvoiceAmount],[Balance]  
--           ,[AmountReceived],[Discount],[Interest]  
--           ,[CFItem1],[CFItem2],[CFItem3],[CFItem4])  
--     VALUES  
--           (@HeaderID,@InvoiceID,@InvoiceNumber  
--           ,@InvoiceDate,@InvoiceAmount,@Balance  
--           ,@AmountReceived,@Discount,@Interest  
--           ,@CFItem1,@CFItem2,@CFItem3,@CFItem4)         
-- End                          
            
--END         
  
--GO
---- =============================================  
---- Author:  TK  
---- Create date: 23/03/2018  
---- Description: Delete Payment Collection 
---- =============================================  
--CREATE PROCEDURE [dbo].[Trans_Payment_Delete]  
-- @HeaderId int,   
-- @mode varchar(50)  
--AS  
--BEGIN  
-- SET NOCOUNT ON;  
--if (@mode = 'PaymentCollection-Delete')  
--	Begin  
--		Update [TranPaymentCollection] set Del_State=1, Del_Date=GetDate() Where ID=@HeaderId
--		Update [TranPaymentCollectionList] set Del_State=1, Del_Date=GetDate() Where HeaderID=@HeaderId
--	End  
--End

--GO  
--IF NOT EXISTS(SELECT * FROM sys.columns
--WHERE Name = N'AgentID' AND OBJECT_ID = OBJECT_ID(N'TranSalesInvoice'))
--	BEGIN
--		alter table [TranSalesInvoice] add AgentID int
--	END 
--go

--update TranSalesInvoice set AgentID='' where isnull(AgentID,'')=''

--GO

CREATE TABLE [dbo].[SettingFinancialYear](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [int] NOT NULL,
	[StartDate] date,
	[EndDate] date,
	[IsActive] bit,
	CreatedBy  varchar(50),
	CreatedOn datetime
) ON [PRIMARY]

GO

alter PROCEDURE [dbo].[CheckAvailabilityInvoiceNo]        
 @InvoiceNumber VARCHAR(20),         
 @HeaderID bigint,    
 @CompanyID int,           
 @mode varchar(50)        
AS        
BEGIN        
 SET NOCOUNT ON;        
 if (@mode = 'SalesInvoice')        
 Begin        
  if(@HeaderID=0)    
   begin    
   --New Invoice    
   declare @FinancialYearStartDate date
   declare @FinancialYearEndDate date

   select @FinancialYearStartDate = StartDate, @FinancialYearEndDate = EndDate 
   from SettingFinancialYear where IsActive = 1 and CompanyId = @CompanyId

    select count(*) Cnt from transalesinvoice     
    where Del_State=0     
    and InvoiceNumber = @InvoiceNumber    
    and CompanyID = @CompanyID    
	and InvoiceDate between @FinancialYearStartDate and @FinancialYearEndDate

   end    
  else    
   begin    
   --Existing Invoice    
    select count(*) Cnt from transalesinvoice     
    where Del_State=0     
    and InvoiceNumber = @InvoiceNumber    
    and CompanyID = @CompanyID    
    and ID <> @HeaderID    
   end    
 End       
END     

GO

create PROCEDURE Setting_FinancialYear  
@CompanyId int  
,@StartDate date = null  
,@EndDate date = null  
,@CreatedBy varchar(50) = null  
,@mode varchar(20)  
AS  
BEGIN  
if @mode = 'InsertYear'  
Begin  
 update SettingFinancialYear set IsActive = 0 where CompanyId=@CompanyId
  
 delete SettingFinancialYear where StartDate = @StartDate and EndDate = @EndDate and CompanyId=@CompanyId
  
 INSERT INTO [SettingFinancialYear]  
           ([CompanyId]  
           ,[StartDate]  
           ,[EndDate]  
     ,[IsActive]  
           ,[CreatedBy]  
           ,[CreatedOn])  
     VALUES  
           (@CompanyId  
           ,@StartDate  
           ,@EndDate  
     ,1  
           ,@CreatedBy  
           ,getdate())  
end  
else if @mode = 'SelectActiveYear'  
begin  
 select StartDate, EndDate from SettingFinancialYear where IsActive = 1 and CompanyId = @CompanyId  
end  
END  
go
IF NOT EXISTS(SELECT * FROM sys.columns
WHERE Name = N'ReceiptId' AND OBJECT_ID = OBJECT_ID(N'TranPaymentCollection'))
	BEGIN
		alter table TranPaymentCollection add ReceiptId bigint
	END 
go
-- =============================================        
-- Author:  TK        
-- Create date: 22-Feb-2017    
-- Description: To Get auto sequnce number for Accounts.        
-- =============================================        
-- GetAutoSequenceNumber_Accounts 2, 'Payment Collection'
alter PROCEDURE [dbo].[GetAutoSequenceNumber_Accounts]        
 @CompanyId int,        
 @DocumentType varchar(100)       
AS        
BEGIN        
 SET NOCOUNT ON;        
 if(@DocumentType='Payment Collection')        
    Begin        
      select ISNULL(max(ReceiptId),0)+1 AutoNumber from [TranPaymentCollection] where CompanyID=@CompanyId    
 End            
END   
  
go

--- Need to update agentID into direct party agent id wherever agent id = 0
 update TranSalesInvoice set AgentID = (select ID from MasterCustomer where CustomerName = 'Direct Party' and Del_State=0 and CustomerType ='Agent') where AgentID = 0

 go

 

--Customized for Manjunath
--update MasterCustomer set CustomerType ='Buyer' where CustomerType ='All'
--Go
--insert into MasterCustomer 
--(CustomerName, BillingAddress,State, StateCode, CustomerStatus, CustomerType, Del_State)
--values
--('Direct Party',  '','TAMIL NADU','33','Active','Agent',0),
--('Bhansali Textile Agency',  'Silchar','TAMIL NADU','33','Active','Agent',0),
--('Bhawani Textile Agency',  'Dhubri','TAMIL NADU','33','Active','Agent',0),
--('Bhawani Textile Agency',  'Jorhat','TAMIL NADU','33','Active','Agent',0),
--('Dayal Agency',  'Ambala','TAMIL NADU','33','Active','Agent',0),
--('Himachal Handloom Agency',  'Una','TAMIL NADU','33','Active','Agent',0),
--('Murali',  'Kolkata','TAMIL NADU','33','Active','Agent',0),
--('Rafi Textile Agency',  '','TAMIL NADU','33','Active','Agent',0),
--('Shree Mahabir Agency',  'Guwahati','TAMIL NADU','33','Active','Agent',0),
--('Sri Textile Agency',  'Gangashahar','TAMIL NADU','33','Active','Agent',0),
--('STA',  '','TAMIL NADU','33','Active','Agent',0)

--update TranSalesInvoice set AgentID = 
--(Select Id from MasterCustomer where CustomerName = 'Bhansali Textile Agency')
--where CF6  like '%Bhansali Textile Agency%'
--go
--update TranSalesInvoice set AgentID = 
--(Select Id from MasterCustomer where CustomerName = 'Bhawani Textile Agency' and BillingAddress = 'Dhubri')
--where CF6 like '%Bhawani Textile Agency%' and CF6 like '%Dhubri%'
--go
--update TranSalesInvoice set AgentID = 
--(Select Id from MasterCustomer where CustomerName = 'Bhawani Textile Agency' and BillingAddress = 'Jorhat')
--where CF6 like '%Bhawani Textile Agency%' and CF6 like '%Jorhat%'
--go
--update TranSalesInvoice set AgentID = 
--(Select Id from MasterCustomer where CustomerName = 'Dayal Agency')
--where CF6 like '%Dayal Agency%'
--go
--update TranSalesInvoice set AgentID = 
--(Select Id from MasterCustomer where CustomerName = 'Himachal Handloom Agency')
--where CF6 like '%Himachal Handloom Agency%'
--go
--update TranSalesInvoice set AgentID = 
--(Select Id from MasterCustomer where CustomerName = 'Murali')
--where CF6 like '%Murali%'
--go
--update TranSalesInvoice set AgentID = 
--(Select Id from MasterCustomer where CustomerName = 'Rafi Textile Agency')
--where CF6 like '%Rafi Textile Agency%'
--go
--update TranSalesInvoice set AgentID = 
--(Select Id from MasterCustomer where CustomerName = 'Shree Mahabir Agency')
--where CF6 like '%Shree Mahabir Agency%'
--go
--update TranSalesInvoice set AgentID = 
--(Select Id from MasterCustomer where CustomerName = 'Sri Textile Agency')
--where CF6 like '%Sri Textile Agency%'
--go
--update TranSalesInvoice set AgentID = 
--(Select Id from MasterCustomer where CustomerName = 'STA')
--where CF6 like '%STA%'
--go
--update TranSalesInvoice set AgentID = 
--(Select Id from MasterCustomer where CustomerName = 'Direct Party')
--where CF6 like '%Direct Party%'
--go
--update TranSalesInvoice set AgentID =0
--where CF6 =''
--go
--update TranSalesInvoice set CF6 =''
--go