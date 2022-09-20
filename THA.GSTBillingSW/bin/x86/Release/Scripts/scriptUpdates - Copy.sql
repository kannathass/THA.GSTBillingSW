USE [THAgstBilling]
GO

--if (select DATA_TYPE from INFORMATION_SCHEMA.COLUMNS IC where TABLE_NAME = 'TranSalesInvoice' and COLUMN_NAME = 'CompanyID')<>'int'
--	begin
--		alter table [TranSalesInvoice] alter column CompanyID int
--	end
--GO
--if (select DATA_TYPE from INFORMATION_SCHEMA.COLUMNS IC where TABLE_NAME = 'TranQuotation' and COLUMN_NAME = 'CompanyID')<>'int'
--	begin
--		alter table [TranQuotation] alter column CompanyID int
--	end
--GO
--if (select DATA_TYPE from INFORMATION_SCHEMA.COLUMNS IC where TABLE_NAME = 'TranPurchaseInvoice' and COLUMN_NAME = 'CompanyID')<>'int'
--	begin
--		alter table [TranPurchaseInvoice] alter column CompanyID int
--	end
--go
--IF NOT EXISTS(SELECT * FROM sys.columns
--WHERE Name = N'CustomerType' AND OBJECT_ID = OBJECT_ID(N'MasterCustomer'))
--	BEGIN
--		alter table [MasterCustomer] add CustomerType varchar(50)
--	END 
--go

--update MasterCustomer set CustomerType='All' where isnull(CustomerType,'')=''

--go

--IF NOT EXISTS(SELECT * FROM sys.columns
--WHERE Name = N'CESSPercentTotal' AND OBJECT_ID = OBJECT_ID(N'TranSalesInvoice'))
--	BEGIN
--		alter table [TranSalesInvoice] add CESSPercentTotal decimal(5,2)
--	END 
--go
--IF NOT EXISTS(SELECT * FROM sys.columns
--WHERE Name = N'SGSTPercentTotal' AND OBJECT_ID = OBJECT_ID(N'TranSalesInvoice'))
--	BEGIN
--		alter table [TranSalesInvoice] add SGSTPercentTotal decimal(5,2)
--	END 
--go
--IF NOT EXISTS(SELECT * FROM sys.columns
--WHERE Name = N'CGSTPercentTotal' AND OBJECT_ID = OBJECT_ID(N'TranSalesInvoice'))
--	BEGIN
--		alter table [TranSalesInvoice] add CGSTPercentTotal decimal(5,2)
--	END 
--go
--IF NOT EXISTS(SELECT * FROM sys.columns
--WHERE Name = N'IGSTPercentTotal' AND OBJECT_ID = OBJECT_ID(N'TranSalesInvoice'))
--	BEGIN
--		alter table [TranSalesInvoice] add IGSTPercentTotal decimal(5,2)
--	END 
--go
--IF NOT EXISTS(SELECT * FROM sys.columns
--WHERE Name = N'DiscountPercentTotal' AND OBJECT_ID = OBJECT_ID(N'TranSalesInvoice'))
--	BEGIN
--		alter table [TranSalesInvoice] add DiscountPercentTotal decimal(5,2)
--	END 
--go
--IF NOT EXISTS(SELECT * FROM sys.columns
--WHERE Name = N'DiscountAmount' AND OBJECT_ID = OBJECT_ID(N'TranSalesInvoice'))
--	BEGIN
--		alter table [TranSalesInvoice] add DiscountAmount decimal(10,2)
--	END 
--go
--IF NOT EXISTS(SELECT * FROM sys.columns
--WHERE Name = N'TaxableAmountWithDisc' AND OBJECT_ID = OBJECT_ID(N'TranSalesInvoice'))
--	BEGIN
--		alter table [TranSalesInvoice] add TaxableAmountWithDisc decimal(10,2)
--	END 
--go
--IF NOT EXISTS(SELECT * FROM sys.columns
--WHERE Name = N'IsReverseCharge' AND OBJECT_ID = OBJECT_ID(N'TranSalesInvoice'))
--	BEGIN
--		alter table [TranSalesInvoice] add IsReverseCharge bit
--	END 
--go
--IF NOT EXISTS(SELECT * FROM sys.columns
--WHERE Name = N'CF6' AND OBJECT_ID = OBJECT_ID(N'TranSalesInvoice'))
--	BEGIN
--		alter table [TranSalesInvoice] add CF6 varchar(250)
--	END 
--go

--CREATE TABLE [dbo].[TranDeliveryNote](
--	[ID] [bigint] IDENTITY(1,1) NOT NULL,
--	[CompanyID] [int] NULL,
--	[NoteNumber] [varchar](20) NULL,
--	[NoteDate] [date] NULL,
--	[CustomerID] [int] NULL,
--	[ReferenceNumber] [varchar](50) NULL,
--	[ReferenceDate] [date] NULL,
--	[NoteType] [varchar](50) NULL,
--	[CGSTAmount] [decimal](10, 2) NULL,
--	[SGSTAmount] [decimal](10, 2) NULL,
--	[IGSTAmount] [decimal](10, 2) NULL,
--	[CESSAmount] [decimal](10, 2) NULL,
--	[TaxableAmount] [decimal](16, 2) NULL,
--	[TaxableAmountWithDisc] [decimal](16, 2) NULL,
--	[CESSPercentTotal] [decimal](5, 2) NULL,
--	[SGSTPercentTotal] [decimal](5, 2) NULL,
--	[CGSTPercentTotal] [decimal](5, 2) NULL,
--	[IGSTPercentTotal] [decimal](5, 2) NULL,
--	[DiscountPercentTotal] [decimal](5, 2) NULL,
--	[DiscountAmount] [decimal](10, 2) NULL,
--	[NoteTotalAmount] [decimal](16, 2) NULL,
--	[RoundedOffNoteTotalAmount] [bigint] NULL,
--	[NoteStatus] [varchar](50) NULL,
--	[ExtraNotes] [varchar](1000) NULL,
--	[TermsAndCondition] [varchar](500) NULL,
--	[isCessApplicable] [bit] NULL,
--	[IsReverseCharge] [bit] NULL,
--	[CF1] [varchar](100) NULL,
--	[CF2] [varchar](100) NULL,
--	[CF3] [varchar](100) NULL,
--	[CF4] [varchar](100) NULL,
--	[CF5] [varchar](100) NULL,
--	[CF6] [varchar](250) NULL,
--	[CreatedBy] [varchar](50) NULL,
--	[CreatedOn] [datetime] NULL,
--	[Del_State] [bit] NULL,
--	[Del_Date] [datetime] NULL
--) ON [PRIMARY]

--GO

--ALTER TABLE [dbo].[TranDeliveryNote] ADD  CONSTRAINT [DF_TranDeliveryNote_Del_State]  DEFAULT ((0)) FOR [Del_State]
--GO
--CREATE TABLE [dbo].[TranDeliveryNoteItems](
--	[ID] [bigint] IDENTITY(1,1) NOT NULL,
--	[HeaderID] [bigint] NULL,
--	[ItemID] [int] NULL,
--	[ItemDescription] [varchar](200) NULL,
--	[ItemCustomDescription] [varchar](200) NULL,
--	[ItemType] [varchar](10) NULL,
--	[HsnSac] [varchar](10) NULL,
--	[Qty] [decimal](10, 2) NULL,
--	[UoM] [varchar](20) NULL,
--	[RatePerItem] [decimal](10, 2) NULL,
--	[DiscountPercent] [decimal](5, 2) NULL,
--	[Discount] [decimal](10, 2) NULL,
--	[TaxableValue] [decimal](10, 2) NULL,
--	[CGSTPercent] [decimal](5, 2) NULL,
--	[CGSTValue] [decimal](10, 2) NULL,
--	[SGSTPercent] [decimal](5, 2) NULL,
--	[SGSTValue] [decimal](10, 2) NULL,
--	[IGSTPercent] [decimal](5, 2) NULL,
--	[IGSTValue] [decimal](10, 2) NULL,
--	[CESSPercent] [decimal](5, 2) NULL,
--	[CESSValue] [decimal](10, 2) NULL,
--	[TotalValue] [decimal](16, 2) NULL,
--	[Del_State] [bit] NULL,
--	[Del_Date] [datetime] NULL
--) ON [PRIMARY]

--GO

--ALTER TABLE [dbo].[TranDeliveryNoteItems] ADD  CONSTRAINT [DF_TranDeliveryNoteItems_Del_State]  DEFAULT ((0)) FOR [Del_State]
--GO
--CREATE TABLE [dbo].[TranReceiptNote](
--	[ID] [bigint] IDENTITY(1,1) NOT NULL,
--	[CompanyID] [int] NULL,
--	[NoteNumber] [varchar](20) NULL,
--	[NoteDate] [date] NULL,
--	[CustomerID] [int] NULL,
--	[ReferenceNumber] [varchar](50) NULL,
--	[ReferenceDate] [date] NULL,
--	[NoteType] [varchar](50) NULL,
--	[CGSTAmount] [decimal](10, 2) NULL,
--	[SGSTAmount] [decimal](10, 2) NULL,
--	[IGSTAmount] [decimal](10, 2) NULL,
--	[CESSAmount] [decimal](10, 2) NULL,
--	[TaxableAmount] [decimal](16, 2) NULL,
--	[TaxableAmountWithDisc] [decimal](16, 2) NULL,
--	[CESSPercentTotal] [decimal](5, 2) NULL,
--	[SGSTPercentTotal] [decimal](5, 2) NULL,
--	[CGSTPercentTotal] [decimal](5, 2) NULL,
--	[IGSTPercentTotal] [decimal](5, 2) NULL,
--	[DiscountPercentTotal] [decimal](5, 2) NULL,
--	[DiscountAmount] [decimal](10, 2) NULL,
--	[NoteTotalAmount] [decimal](16, 2) NULL,
--	[RoundedOffNoteTotalAmount] [bigint] NULL,
--	[NoteStatus] [varchar](50) NULL,
--	[ExtraNotes] [varchar](1000) NULL,
--	[TermsAndCondition] [varchar](500) NULL,
--	[isCessApplicable] [bit] NULL,
--	[IsReverseCharge] [bit] NULL,
--	[CF1] [varchar](100) NULL,
--	[CF2] [varchar](100) NULL,
--	[CF3] [varchar](100) NULL,
--	[CF4] [varchar](100) NULL,
--	[CF5] [varchar](100) NULL,
--	[CF6] [varchar](250) NULL,
--	[CreatedBy] [varchar](50) NULL,
--	[CreatedOn] [datetime] NULL,
--	[Del_State] [bit] NULL,
--	[Del_Date] [datetime] NULL
--) ON [PRIMARY]

--GO

--ALTER TABLE [dbo].[TranReceiptNote] ADD  CONSTRAINT [DF_TranReceiptNote_Del_State]  DEFAULT ((0)) FOR [Del_State]
--GO
--CREATE TABLE [dbo].[TranReceiptNoteItems](
--	[ID] [bigint] IDENTITY(1,1) NOT NULL,
--	[HeaderID] [bigint] NULL,
--	[ItemID] [int] NULL,
--	[ItemDescription] [varchar](200) NULL,
--	[ItemCustomDescription] [varchar](200) NULL,
--	[ItemType] [varchar](10) NULL,
--	[HsnSac] [varchar](10) NULL,
--	[Qty] [decimal](10, 2) NULL,
--	[UoM] [varchar](20) NULL,
--	[RatePerItem] [decimal](10, 2) NULL,
--	[DiscountPercent] [decimal](5, 2) NULL,
--	[Discount] [decimal](10, 2) NULL,
--	[TaxableValue] [decimal](10, 2) NULL,
--	[CGSTPercent] [decimal](5, 2) NULL,
--	[CGSTValue] [decimal](10, 2) NULL,
--	[SGSTPercent] [decimal](5, 2) NULL,
--	[SGSTValue] [decimal](10, 2) NULL,
--	[IGSTPercent] [decimal](5, 2) NULL,
--	[IGSTValue] [decimal](10, 2) NULL,
--	[CESSPercent] [decimal](5, 2) NULL,
--	[CESSValue] [decimal](10, 2) NULL,
--	[TotalValue] [decimal](16, 2) NULL,
--	[Del_State] [bit] NULL,
--	[Del_Date] [datetime] NULL
--) ON [PRIMARY]

--GO

--ALTER TABLE [dbo].[TranReceiptNoteItems] ADD  CONSTRAINT [DF_TranReceiptNoteItems_Del_State]  DEFAULT ((0)) FOR [Del_State]

--GO
--CREATE PROCEDURE [dbo].[CheckAvailabilityInvoiceNo]    
-- @InvoiceNumber int,     
-- @HeaderID bigint,
-- @CompanyID int,       
-- @mode varchar(50)    
--AS    
--BEGIN    
-- SET NOCOUNT ON;    
-- if (@mode = 'SalesInvoice')    
--	Begin    
--		if(@HeaderID=0)
--			begin
--			--New Invoice
--				select count(*) Cnt from transalesinvoice 
--				where Del_State=0 
--				and InvoiceNumber = @InvoiceNumber
--				and CompanyID = @CompanyID
--			end
--		else
--			begin
--			--Existing Invoice
--				select count(*) Cnt from transalesinvoice 
--				where Del_State=0 
--				and InvoiceNumber = @InvoiceNumber
--				and CompanyID = @CompanyID
--				and ID <> @HeaderID
--			end
--	End   
--END 

--GO
  
---- =============================================  
---- Author:  TK  
---- Create date: 27-Aug-2017  
---- Description: To Get auto sequnce number for Sales Invoice.  
---- =============================================  
--ALTER PROCEDURE [dbo].[GetAutoSequenceNumber]  
-- -- Add the parameters for the stored procedure here  
-- @CompanyId int,  
-- @DocumentType varchar(100),  
-- @mode varchar(20)   
--AS  
--BEGIN  
-- SET NOCOUNT ON;  
  
-- declare @IsAutoDocumentSequanceNumber bit  
-- declare @IdPrefix varchar(50)  
-- declare @DocumentIdResetFlag bit  
  
-- select @IsAutoDocumentSequanceNumber = IsAutoDocumentSequanceNumber, @IdPrefix =IdPrefix, @DocumentIdResetFlag=DocumentIdResetFlag   
-- from SettingDocument where CompanyId=@CompanyId and DocumentType=@DocumentType  
  
-- if (@mode = 'NewInvoice')  
-- Begin  
--  --select @IsAutoDocumentSequanceNumber, @IdPrefix, @DocumentIdResetFlag  
  
--  if (@IsAutoDocumentSequanceNumber=1)   
--  Begin  
--   if (@DocumentIdResetFlag=1)   
--   begin  
--    select convert(varchar(20),IdSeriesStart) InvoiceNumber, DocumentIdResetFlag, IdPrefix from SettingDocument where CompanyId=@CompanyId and DocumentType=@DocumentType  
--   end  
--   else  
--   begin  
--    if(@DocumentType='Sales Invoice')  
--     Begin  
--      select convert(varchar(20),max(convert(int, replace(InvoiceNumber,@IdPrefix,'')))+1) InvoiceNumber,  
--      CONVERT(bit, 0) DocumentIdResetFlag, @IdPrefix IdPrefix from [TranSalesInvoice] where CompanyID=@CompanyId  
--      and id>(select maxDocumentIdWhileResettingSequence from [SettingDocumentIdResetDetail] where CompanyId=@CompanyId and DocumentType=@DocumentType)  
--     End  
      
--    else if(@DocumentType='Purchase Invoice')  
--     Begin  
--      select convert(varchar(20),max(convert(int, replace(InvoiceNumber,@IdPrefix,'')))+1) InvoiceNumber,  
--      CONVERT(bit, 0) DocumentIdResetFlag, @IdPrefix IdPrefix from [TranPurchaseInvoice] where CompanyID=@CompanyId  
--      and id>(select maxDocumentIdWhileResettingSequence from [SettingDocumentIdResetDetail] where CompanyId=@CompanyId and DocumentType=@DocumentType)  
--     End  
--    else if(@DocumentType='Quotation')  
--     Begin  
--      select convert(varchar(20),max(convert(int, replace(QuotationNumber,@IdPrefix,'')))+1) InvoiceNumber,  
--      CONVERT(bit, 0) DocumentIdResetFlag, @IdPrefix IdPrefix from [TranQuotation] where CompanyID=@CompanyId  
--      and id>(select maxDocumentIdWhileResettingSequence from [SettingDocumentIdResetDetail] where CompanyId=@CompanyId and DocumentType=@DocumentType)  
--     End  
--    else if(@DocumentType='Delivery Note')  
--     Begin  
--      select convert(varchar(20),max(convert(int, replace(NoteNumber,@IdPrefix,'')))+1) InvoiceNumber,  
--      CONVERT(bit, 0) DocumentIdResetFlag, @IdPrefix IdPrefix from [TranDeliveryNote] where CompanyID=@CompanyId  
--      and id>(select maxDocumentIdWhileResettingSequence from [SettingDocumentIdResetDetail] where CompanyId=@CompanyId and DocumentType=@DocumentType)  
--     End  
--    else if(@DocumentType='Receipt Note')  
--     Begin  
--      select convert(varchar(20),max(convert(int, replace(NoteNumber,@IdPrefix,'')))+1) InvoiceNumber,  
--      CONVERT(bit, 0) DocumentIdResetFlag, @IdPrefix IdPrefix from [TranReceiptNote] where CompanyID=@CompanyId  
--      and id>(select maxDocumentIdWhileResettingSequence from [SettingDocumentIdResetDetail] where CompanyId=@CompanyId and DocumentType=@DocumentType)  
--     End 
--    end  
--   End  
--  else  
--   Begin  
--    select 'ManualNewInvoice' InvoiceNumber, CONVERT(bit, 0) DocumentIdResetFlag, '' IdPrefix  
--   End  
-- End  
-- else if (@mode = 'ExistingInvoice')  
--  Begin  
--  if (@IsAutoDocumentSequanceNumber=1)   
--   Begin  
--    select 'AutoExistingInvoice' InvoiceNumber,CONVERT(bit, 0) DocumentIdResetFlag, '' IdPrefix  
--   End  
--  else  
--   Begin  
--    select 'ManualExistingInvoice' InvoiceNumber,CONVERT(bit, 0) DocumentIdResetFlag, '' IdPrefix  
--   End  
--  End  
-- else  
--  Begin  
--   UPDATE [SettingDocument] SET [DocumentIdResetFlag] = 0 WHERE  CompanyId = @CompanyId and DocumentType = @DocumentType  
--  End  
--END  
  
--GO
  
---- =============================================  
---- Author:  TK  
---- Create date: 27-Aug-2017  
---- Description: To Get auto sequnce number for Sales Invoice.  
---- =============================================  
--ALTER PROCEDURE [dbo].[GetAutoSequenceNumber]  
-- -- Add the parameters for the stored procedure here  
-- @CompanyId int,  
-- @DocumentType varchar(100),  
-- @mode varchar(20)   
--AS  
--BEGIN  
-- SET NOCOUNT ON;  
  
-- declare @IsAutoDocumentSequanceNumber bit  
-- declare @IdPrefix varchar(50)  
-- declare @DocumentIdResetFlag bit  
  
-- select @IsAutoDocumentSequanceNumber = IsAutoDocumentSequanceNumber, @IdPrefix =IdPrefix, @DocumentIdResetFlag=DocumentIdResetFlag   
-- from SettingDocument where CompanyId=@CompanyId and DocumentType=@DocumentType  
  
-- if (@mode = 'NewInvoice')  
-- Begin  
--  --select @IsAutoDocumentSequanceNumber, @IdPrefix, @DocumentIdResetFlag  
  
--  if (@IsAutoDocumentSequanceNumber=1)   
--  Begin  
--   if (@DocumentIdResetFlag=1)   
--   begin  
--    select convert(varchar(20),IdSeriesStart) InvoiceNumber, DocumentIdResetFlag, IdPrefix from SettingDocument where CompanyId=@CompanyId and DocumentType=@DocumentType  
--   end  
--   else  
--   begin  
--    if(@DocumentType='Sales Invoice')  
--     Begin  
--      select convert(varchar(20),max(convert(int, replace(InvoiceNumber,@IdPrefix,'')))+1) InvoiceNumber,  
--      CONVERT(bit, 0) DocumentIdResetFlag, @IdPrefix IdPrefix from [TranSalesInvoice] where CompanyID=@CompanyId  
--      and id>(select maxDocumentIdWhileResettingSequence from [SettingDocumentIdResetDetail] where CompanyId=@CompanyId and DocumentType=@DocumentType)  
--     End  
      
--    else if(@DocumentType='Purchase Invoice')  
--     Begin  
--      select convert(varchar(20),max(convert(int, replace(InvoiceNumber,@IdPrefix,'')))+1) InvoiceNumber,  
--      CONVERT(bit, 0) DocumentIdResetFlag, @IdPrefix IdPrefix from [TranPurchaseInvoice] where CompanyID=@CompanyId  
--      and id>(select maxDocumentIdWhileResettingSequence from [SettingDocumentIdResetDetail] where CompanyId=@CompanyId and DocumentType=@DocumentType)  
--     End  
--    else if(@DocumentType='Quotation')  
--     Begin  
--      select convert(varchar(20),max(convert(int, replace(QuotationNumber,@IdPrefix,'')))+1) InvoiceNumber,  
--      CONVERT(bit, 0) DocumentIdResetFlag, @IdPrefix IdPrefix from [TranQuotation] where CompanyID=@CompanyId  
--      and id>(select maxDocumentIdWhileResettingSequence from [SettingDocumentIdResetDetail] where CompanyId=@CompanyId and DocumentType=@DocumentType)  
--     End  
--    else if(@DocumentType='Delivery Note')  
--     Begin  
--      select convert(varchar(20),max(convert(int, replace(NoteNumber,@IdPrefix,'')))+1) InvoiceNumber,  
--      CONVERT(bit, 0) DocumentIdResetFlag, @IdPrefix IdPrefix from [TranDeliveryNote] where CompanyID=@CompanyId  
--      and id>(select maxDocumentIdWhileResettingSequence from [SettingDocumentIdResetDetail] where CompanyId=@CompanyId and DocumentType=@DocumentType)  
--     End  
--    else if(@DocumentType='Receipt Note')  
--     Begin  
--      select convert(varchar(20),max(convert(int, replace(NoteNumber,@IdPrefix,'')))+1) InvoiceNumber,  
--      CONVERT(bit, 0) DocumentIdResetFlag, @IdPrefix IdPrefix from [TranReceiptNote] where CompanyID=@CompanyId  
--      and id>(select maxDocumentIdWhileResettingSequence from [SettingDocumentIdResetDetail] where CompanyId=@CompanyId and DocumentType=@DocumentType)  
--     End 
--    end  
--   End  
--  else  
--   Begin  
--    select 'ManualNewInvoice' InvoiceNumber, CONVERT(bit, 0) DocumentIdResetFlag, '' IdPrefix  
--   End  
-- End  
-- else if (@mode = 'ExistingInvoice')  
--  Begin  
--  if (@IsAutoDocumentSequanceNumber=1)   
--   Begin  
--    select 'AutoExistingInvoice' InvoiceNumber,CONVERT(bit, 0) DocumentIdResetFlag, '' IdPrefix  
--   End  
--  else  
--   Begin  
--    select 'ManualExistingInvoice' InvoiceNumber,CONVERT(bit, 0) DocumentIdResetFlag, '' IdPrefix  
--   End  
--  End  
-- else  
--  Begin  
--   UPDATE [SettingDocument] SET [DocumentIdResetFlag] = 0 WHERE  CompanyId = @CompanyId and DocumentType = @DocumentType  
--  End  
--END  
  
--GO

---- =============================================  
---- Author:  TK  
---- Create date: 13/10/2017  
---- Description: Delete invoices with inventory updates  
---- =============================================  
--ALTER PROCEDURE [dbo].[Trans_Invoice_Delete]  
-- @InvoiceHeaderId int,   
-- @CompanyID int,  
-- @StockCompanyID int=0,  
-- @mode varchar(50)  
--AS  
--BEGIN  
-- SET NOCOUNT ON;  
-- DECLARE @InvoiceItemID INT, @MasterItemID INT, @UpdateInStock decimal  
-- --Delete (Del_State=1) invoie and its items along with inventory updates  
-- if (@mode = 'SalesInvoice-Delete')  
-- Begin  
--  --Cursor to delete invoice items  
  
--  DECLARE @DeleteSalesInvoice CURSOR  
--  SET @DeleteSalesInvoice = CURSOR FOR  
--  select ID, ItemID from [TranSalesInvoiceItems] where SalesInvoiceHeaderID=@InvoiceHeaderId  
--   OPEN @DeleteSalesInvoice  
--   FETCH NEXT  
--   FROM @DeleteSalesInvoice INTO @InvoiceItemID, @MasterItemID  
--   WHILE @@FETCH_STATUS = 0  
--   BEGIN  
--    Select @UpdateInStock=Qty from TranSalesInvoiceItems where ID=@InvoiceItemID  
  
--    exec Trans_StockUpdate @MasterItemID,@StockCompanyID, @UpdateInStock    
  
--    --update MasterItem set StockInHand = StockInHand + (Select Qty from TranSalesInvoiceItems where ID=@InvoiceItemID) where ID=@MasterItemID  
--    Update [TranSalesInvoiceItems] set Del_State=1, Del_Date=GetDate() Where ID=@InvoiceItemID   
  
--   FETCH NEXT  
--   FROM @DeleteSalesInvoice INTO @InvoiceItemID, @MasterItemID  
--   END  
--   CLOSE @DeleteSalesInvoice  
--  DEALLOCATE @DeleteSalesInvoice  
--  --Delete Header  
--        Update [TranSalesInvoice] set Del_State=1, Del_Date=GetDate() Where ID=@InvoiceHeaderId  
-- End  
--  --Delete invoice items while updating invoice header along with inventory updates  
-- else if (@mode = 'SalesInvoice-Reset')  
-- Begin  
--  --Cursor to delete invoice items  
--  DECLARE @ResetSalesInvoiceItems CURSOR  
--  SET @ResetSalesInvoiceItems = CURSOR FOR  
--  select ID, ItemID from [TranSalesInvoiceItems] where SalesInvoiceHeaderID=@InvoiceHeaderId  
--   OPEN @ResetSalesInvoiceItems  
--   FETCH NEXT  
--   FROM @ResetSalesInvoiceItems INTO @InvoiceItemID, @MasterItemID  
--   WHILE @@FETCH_STATUS = 0  
--   BEGIN  
--    Select @UpdateInStock=Qty from TranSalesInvoiceItems where ID=@InvoiceItemID  
  
--    exec Trans_StockUpdate @MasterItemID,@StockCompanyID, @UpdateInStock    
  
--    delete [TranSalesInvoiceItems] Where ID=@InvoiceItemID   
  
--   FETCH NEXT  
--   FROM @ResetSalesInvoiceItems INTO @InvoiceItemID, @MasterItemID  
--   END  
--   CLOSE @ResetSalesInvoiceItems  
--  DEALLOCATE @ResetSalesInvoiceItems  
-- End  
-- if (@mode = 'PurchaseInvoice-Delete')  
-- Begin  
--  --Cursor to delete invoice items  
  
--  DECLARE @DeletePurchaseInvoice CURSOR  
--  SET @DeletePurchaseInvoice = CURSOR FOR  
--  select ID, ItemID from [TranPurchaseInvoiceItems] where PurchaseInvoiceHeaderID=@InvoiceHeaderId  
--   OPEN @DeletePurchaseInvoice  
--   FETCH NEXT  
--   FROM @DeletePurchaseInvoice INTO @InvoiceItemID, @MasterItemID  
--   WHILE @@FETCH_STATUS = 0  
--   BEGIN  
--    Select @UpdateInStock=Qty from TranPurchaseInvoiceItems where ID=@InvoiceItemID  
--    --Setting UpdateInStock to its negative counterpart so that it could reduce the stock at the time of deleting purchase invoice items  
--    set @UpdateInStock=-@UpdateInStock  
--    exec Trans_StockUpdate @MasterItemID,@StockCompanyID, @UpdateInStock    
  
--    Update [TranPurchaseInvoiceItems] set Del_State=1, Del_Date=GetDate() Where ID=@InvoiceItemID   
  
--   FETCH NEXT  
--   FROM @DeletePurchaseInvoice INTO @InvoiceItemID, @MasterItemID  
--   END  
--   CLOSE @DeletePurchaseInvoice  
--  DEALLOCATE @DeletePurchaseInvoice  
--  --Delete Header  
--        Update [TranPurchaseInvoice] set Del_State=1, Del_Date=GetDate() Where ID=@InvoiceHeaderId  
-- End  
--  --Delete invoice items while updating invoice header along with inventory updates  
-- else if (@mode = 'PurchaseInvoice-Reset')  
-- Begin  
--  --Cursor to delete invoice items  
--  DECLARE @ResetPurchaseInvoiceItems CURSOR  
--  SET @ResetPurchaseInvoiceItems = CURSOR FOR  
--  select ID, ItemID from [TranPurchaseInvoiceItems] where PurchaseInvoiceHeaderID=@InvoiceHeaderId  
--   OPEN @ResetPurchaseInvoiceItems  
--   FETCH NEXT  
--   FROM @ResetPurchaseInvoiceItems INTO @InvoiceItemID, @MasterItemID  
--   WHILE @@FETCH_STATUS = 0  
--   BEGIN  
--    Select @UpdateInStock=Qty from TranPurchaseInvoiceItems where ID=@InvoiceItemID  
--    --Setting UpdateInStock to its negative counterpart so that it could reduce the stock at the time of deleting purchase invoice items  
--    set @UpdateInStock=-@UpdateInStock  
--    exec Trans_StockUpdate @MasterItemID,@StockCompanyID, @UpdateInStock    
  
--    delete [TranPurchaseInvoiceItems] Where ID=@InvoiceItemID   
  
--   FETCH NEXT  
--   FROM @ResetPurchaseInvoiceItems INTO @InvoiceItemID, @MasterItemID  
--   END  
--   CLOSE @ResetPurchaseInvoiceItems  
--  DEALLOCATE @ResetPurchaseInvoiceItems  
-- End  
-- else if (@mode = 'DeliveryNote-Delete')  
-- Begin  
--  --Cursor to delete invoice items  
  
--  DECLARE @DeleteDeliveryNote CURSOR  
--  SET @DeleteDeliveryNote = CURSOR FOR  
--  select ID, ItemID from [TranDeliveryNoteItems] where HeaderID=@InvoiceHeaderId  
--   OPEN @DeleteDeliveryNote  
--   FETCH NEXT  
--   FROM @DeleteDeliveryNote INTO @InvoiceItemID, @MasterItemID  
--   WHILE @@FETCH_STATUS = 0  
--   BEGIN  
--    Select @UpdateInStock=Qty from TranDeliveryNoteItems where ID=@InvoiceItemID  
  
--    exec Trans_StockUpdate @MasterItemID,@StockCompanyID, @UpdateInStock    
  
--    --update MasterItem set StockInHand = StockInHand + (Select Qty from TranDeliveryNoteItems where ID=@InvoiceItemID) where ID=@MasterItemID  
--    Update [TranDeliveryNoteItems] set Del_State=1, Del_Date=GetDate() Where ID=@InvoiceItemID   
  
--   FETCH NEXT  
--   FROM @DeleteDeliveryNote INTO @InvoiceItemID, @MasterItemID  
--   END  
--   CLOSE @DeleteDeliveryNote  
--  DEALLOCATE @DeleteDeliveryNote  
--  --Delete Header  
--        Update [TranDeliveryNote] set Del_State=1, Del_Date=GetDate() Where ID=@InvoiceHeaderId  
-- End  
--  --Delete invoice items while updating invoice header along with inventory updates  
-- else if (@mode = 'DeliveryNote-Reset')  
-- Begin  
--  --Cursor to delete invoice items  
--  DECLARE @ResetDeliveryNoteItems CURSOR  
--  SET @ResetDeliveryNoteItems = CURSOR FOR  
--  select ID, ItemID from [TranDeliveryNoteItems] where HeaderID=@InvoiceHeaderId  
--   OPEN @ResetDeliveryNoteItems  
--   FETCH NEXT  
--   FROM @ResetDeliveryNoteItems INTO @InvoiceItemID, @MasterItemID  
--   WHILE @@FETCH_STATUS = 0  
--   BEGIN  
--    Select @UpdateInStock=Qty from TranDeliveryNoteItems where ID=@InvoiceItemID  
  
--    exec Trans_StockUpdate @MasterItemID,@StockCompanyID, @UpdateInStock    
  
--    delete [TranDeliveryNoteItems] Where ID=@InvoiceItemID   
  
--   FETCH NEXT  
--   FROM @ResetDeliveryNoteItems INTO @InvoiceItemID, @MasterItemID  
--   END  
--   CLOSE @ResetDeliveryNoteItems  
--  DEALLOCATE @ResetDeliveryNoteItems  
-- End  
-- if (@mode = 'ReceiptNote-Delete')  
-- Begin  
--  --Cursor to delete invoice items  
  
--  DECLARE @DeleteReceiptNote CURSOR  
--  SET @DeleteReceiptNote = CURSOR FOR  
--  select ID, ItemID from [TranReceiptNoteItems] where HeaderID=@InvoiceHeaderId  
--   OPEN @DeleteReceiptNote  
--   FETCH NEXT  
--   FROM @DeleteReceiptNote INTO @InvoiceItemID, @MasterItemID  
--   WHILE @@FETCH_STATUS = 0  
--   BEGIN  
--    Select @UpdateInStock=Qty from TranReceiptNoteItems where ID=@InvoiceItemID  
--    --Setting UpdateInStock to its negative counterpart so that it could reduce the stock at the time of deleting purchase invoice items  
--    set @UpdateInStock=-@UpdateInStock  
--    exec Trans_StockUpdate @MasterItemID,@StockCompanyID, @UpdateInStock    
  
--    Update [TranReceiptNoteItems] set Del_State=1, Del_Date=GetDate() Where ID=@InvoiceItemID   
  
--   FETCH NEXT  
--   FROM @DeleteReceiptNote INTO @InvoiceItemID, @MasterItemID  
--   END  
--   CLOSE @DeleteReceiptNote  
--  DEALLOCATE @DeleteReceiptNote  
--  --Delete Header  
--        Update [TranReceiptNote] set Del_State=1, Del_Date=GetDate() Where ID=@InvoiceHeaderId  
-- End  
--  --Delete invoice items while updating invoice header along with inventory updates  
-- else if (@mode = 'ReceiptNote-Reset')  
-- Begin  
--  --Cursor to delete invoice items  
--  DECLARE @ResetReceiptNoteItems CURSOR  
--  SET @ResetReceiptNoteItems = CURSOR FOR  
--  select ID, ItemID from [TranReceiptNoteItems] where HeaderID=@InvoiceHeaderId  
--   OPEN @ResetReceiptNoteItems  
--   FETCH NEXT  
--   FROM @ResetReceiptNoteItems INTO @InvoiceItemID, @MasterItemID  
--   WHILE @@FETCH_STATUS = 0  
--   BEGIN  
--    Select @UpdateInStock=Qty from TranReceiptNoteItems where ID=@InvoiceItemID  
--    --Setting UpdateInStock to its negative counterpart so that it could reduce the stock at the time of deleting purchase invoice items  
--    set @UpdateInStock=-@UpdateInStock  
--    exec Trans_StockUpdate @MasterItemID,@StockCompanyID, @UpdateInStock    
  
--    delete [TranReceiptNoteItems] Where ID=@InvoiceItemID   
  
--   FETCH NEXT  
--   FROM @ResetReceiptNoteItems INTO @InvoiceItemID, @MasterItemID  
--   END  
--   CLOSE @ResetReceiptNoteItems  
--  DEALLOCATE @ResetReceiptNoteItems  
-- End  
--END  

--GO

--ALTER PROCEDURE [dbo].[Trans_InvoiceItem_Save]    
-- @InvoiceHeaderId int ,     
-- @ItemId int,    
-- @CompanyID int,    
-- @ItemDescription varchar(200),    
-- @ItemCustomDescription varchar(200),    
-- @ItemType varchar(10),    
--    @HsnSac varchar(10),    
-- @Qty DECIMAL(10,2),    
-- @UoM varchar(20),    
-- @RatePerItem DECIMAL(10,2),    
-- @DiscountPercent DECIMAL(5,2),    
-- @Discount DECIMAL(10,2),    
--    @TaxableValue DECIMAL(10,2),    
-- @CGSTPercent DECIMAL(5,2),    
-- @CGSTValue DECIMAL(10,2),    
--    @SGSTPercent DECIMAL(5,2),    
-- @SGSTValue DECIMAL(10,2),    
-- @IGSTPercent DECIMAL(5,2),    
--    @IGSTValue DECIMAL(10,2),    
-- @CESSPercent DECIMAL(5,2),    
-- @CESSValue DECIMAL(10,2),    
-- @TotalValue DECIMAL(16,2),    
-- @StockCompanyID int=0,    
-- @mode varchar(50)    
--AS    
--BEGIN    
-- SET NOCOUNT ON;    
-- if (@mode = 'SalesInvoice-Insert')    
-- Begin    
--  INSERT INTO [TranSalesInvoiceItems]     
--    ([SalesInvoiceHeaderID],[ItemID],[ItemDescription],[ItemCustomDescription],[ItemType]     
--    ,[HsnSac],[Qty],[UoM],[RatePerItem],DiscountPercent,[Discount]     
--    ,[TaxableValue],[CGSTPercent],[CGSTValue]     
--    ,[SGSTPercent],[SGSTValue],[IGSTPercent]     
--    ,[IGSTValue],[CESSPercent],[CESSValue],[TotalValue])     
--    VALUES      
--    (@InvoiceHeaderID, @ItemID, @ItemDescription, @ItemCustomDescription, @ItemType     
--    , @HsnSac, @Qty, @UoM, @RatePerItem,@DiscountPercent, @Discount     
--    , @TaxableValue, @CGSTPercent, @CGSTValue     
--    , @SGSTPercent, @SGSTValue, @IGSTPercent     
--    , @IGSTValue, @CESSPercent, @CESSValue, @TotalValue)    
-- --Setting qty to negative counterpart so that it could reduce the stock at the time of sale    
-- set @Qty=-@Qty    
-- End    
-- else if (@mode = 'PurchaseInvoice-Insert')    
-- Begin    
--  INSERT INTO [TranPurchaseInvoiceItems]    
--     ([PurchaseInvoiceHeaderID],[ItemID],[ItemDescription],[ItemCustomDescription],[ItemType]    
--     ,[HsnSac],[Qty],[UoM],[RatePerItem],DiscountPercent,[Discount]    
--     ,[TaxableValue],[CGSTPercent],[CGSTValue]    
--     ,[SGSTPercent],[SGSTValue],[IGSTPercent]    
--     ,[IGSTValue],[CESSPercent],[CESSValue],[TotalValue])    
--  VALUES     
--     (@InvoiceHeaderId, @ItemID, @ItemDescription, @ItemCustomDescription, @ItemType    
--     , @HsnSac, @Qty, @UoM, @RatePerItem,@DiscountPercent, @Discount    
--     , @TaxableValue, @CGSTPercent, @CGSTValue    
--     , @SGSTPercent, @SGSTValue, @IGSTPercent    
--     , @IGSTValue, @CESSPercent, @CESSValue, @TotalValue)    
-- End   
-- else  if (@mode = 'DeliveryNote-Insert')    
-- Begin    
--  INSERT INTO [TranDeliveryNoteItems]     
--    ([HeaderID],[ItemID],[ItemDescription],[ItemCustomDescription],[ItemType]     
--    ,[HsnSac],[Qty],[UoM],[RatePerItem],DiscountPercent,[Discount]     
--    ,[TaxableValue],[CGSTPercent],[CGSTValue]     
--    ,[SGSTPercent],[SGSTValue],[IGSTPercent]     
--    ,[IGSTValue],[CESSPercent],[CESSValue],[TotalValue])     
--    VALUES      
--    (@InvoiceHeaderID, @ItemID, @ItemDescription, @ItemCustomDescription, @ItemType     
--    , @HsnSac, @Qty, @UoM, @RatePerItem,@DiscountPercent, @Discount     
--    , @TaxableValue, @CGSTPercent, @CGSTValue     
--    , @SGSTPercent, @SGSTValue, @IGSTPercent     
--    , @IGSTValue, @CESSPercent, @CESSValue, @TotalValue)    
-- --Setting qty to negative counterpart so that it could reduce the stock at the time of sale    
-- set @Qty=-@Qty    
-- End    
--  else  if (@mode = 'ReceiptNote-Insert')    
-- Begin    
--  INSERT INTO [TranReceiptNoteItems]     
--    ([HeaderID],[ItemID],[ItemDescription],[ItemCustomDescription],[ItemType]     
--    ,[HsnSac],[Qty],[UoM],[RatePerItem],DiscountPercent,[Discount]     
--    ,[TaxableValue],[CGSTPercent],[CGSTValue]     
--    ,[SGSTPercent],[SGSTValue],[IGSTPercent]     
--    ,[IGSTValue],[CESSPercent],[CESSValue],[TotalValue])     
--    VALUES      
--    (@InvoiceHeaderID, @ItemID, @ItemDescription, @ItemCustomDescription, @ItemType     
--    , @HsnSac, @Qty, @UoM, @RatePerItem,@DiscountPercent, @Discount     
--    , @TaxableValue, @CGSTPercent, @CGSTValue     
--    , @SGSTPercent, @SGSTValue, @IGSTPercent     
--    , @IGSTValue, @CESSPercent, @CESSValue, @TotalValue)     
-- End  
-- exec Trans_StockUpdate @ItemId,@StockCompanyID, @Qty      
    
--END 
--GO
----Trans_MasterStockList 0,1,0,'',0,'GetStockList'  
---- =============================================  
---- Author:  TK  
---- Create date: 14/10/2017  
---- Description: Deals with table MasterStock  
--alter PROCEDURE [dbo].[Trans_MasterStockList]  
-- @StockID int=0,  
-- @ItemID int=0,  
-- @CompanyID int=0,  
-- @GroupName varchar(200)='',  
-- @FilterValue varchar(100)='',  
-- @StockInHand decimal=0,  
-- @mode varchar(50)  
--AS  
--BEGIN  
-- SET NOCOUNT ON;  
-- if (@mode = 'GetStockList')  
-- Begin  
--  insert into  MasterStockList(CompanyID, ItemID, StockInHand)   
--  select @CompanyID,ID,0 from MasterItem where ID not in(select itemid from MasterStockList where CompanyID=@CompanyID) and Del_State=0  
  
--  select  
--  Stock.ID, Item.ID ItemID, Grp.GroupName,Item.ItemDescription,  
--  Item.Size,Item.HsnSac,Item.UoM, Stock.StockInHand  
--  from MasterStockList Stock   
--  inner join MasterItem Item on Stock.ItemID=Item.ID and Item.Del_State=0   
--  inner join vw_MasterGroup Grp   
--  on Item.GroupIDUnder=Grp.ID   
--  where Stock.CompanyID=@CompanyID and   
--  (Item.ItemDescription like @FilterValue or Size like @FilterValue or UoM like @FilterValue or HsnSac like @FilterValue)  
--  and GroupName in (select groupname from vw_MasterGroup where GroupName like @GroupName)
--  order by Grp.GroupName, Item.ItemDescription   
-- End  
-- if (@mode = 'UpdateStockQuantity')  
-- Begin  
--  UPDATE [MasterStockList]  
--  SET [StockInHand] = @StockInHand  
--  WHERE ID = @StockID  
-- End  
-- if (@mode = 'InsertStockItem')  
-- Begin  
--  INSERT INTO [dbo].[MasterStockList]  
--           ([CompanyID]  
--           ,[ItemID]  
--           ,[StockInHand])  
--  VALUES  
--           (@CompanyID  
--     ,@ItemID  
--     ,@StockInHand)  
-- End  
-- if (@mode = 'DeleteStockItem')  
-- Begin  
--  DELETE [MasterStockList]   
--  WHERE ID = @StockID   
-- End  
--END
--GO
--alter PROCEDURE [dbo].[CheckAvailabilityInvoiceNo]      
-- @InvoiceNumber varchar(20),       
-- @HeaderID bigint,  
-- @CompanyID int,         
-- @mode varchar(50)      
--AS      
--BEGIN      
-- SET NOCOUNT ON;      
-- if (@mode = 'SalesInvoice')      
-- Begin      
--  if(@HeaderID=0)  
--   begin  
--   --New Invoice  
--    select count(*) Cnt from transalesinvoice   
--    where Del_State=0   
--    and InvoiceNumber = @InvoiceNumber  
--    and CompanyID = @CompanyID  
--   end  
--  else  
--   begin  
--   --Existing Invoice  
--    select count(*) Cnt from transalesinvoice   
--    where Del_State=0   
--    and InvoiceNumber = @InvoiceNumber  
--    and CompanyID = @CompanyID  
--    and ID <> @HeaderID  
--   end  
-- End     
--END   

--GO  

----update MasterItem set DiscountAmount=0,DiscountPercent=0 where ID in( select ID from MasterItem where DiscountPercent>0.00 or DiscountAmount>0.00)
----go
----update MasterItem set TaxRatePercent=18 where ID in( select ID from MasterItem where TaxRatePercent=28)
----go



