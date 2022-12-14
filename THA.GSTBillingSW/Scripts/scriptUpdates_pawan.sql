USE [THAgstBilling]
GO
--delete MasterStockList  where CompanyID=1
--GO
--update MasterStockList set CompanyID=1 where CompanyID=0
--go
IF NOT EXISTS(SELECT * FROM sys.columns
WHERE Name = N'RoundedOffInvoiceTotalAmount' AND OBJECT_ID = OBJECT_ID(N'TranSalesInvoice'))
	BEGIN
		alter table TranSalesInvoice add RoundedOffInvoiceTotalAmount bigint
	END 
go
update TranSalesInvoice set RoundedOffInvoiceTotalAmount=ROUND(invoicetotalamount,0) where isnull(RoundedOffInvoiceTotalAmount,0)=0
go
IF NOT EXISTS(SELECT * FROM sys.columns
WHERE Name = N'RoundedOffInvoiceTotalAmount' AND OBJECT_ID = OBJECT_ID(N'TranPurchaseInvoice'))
	BEGIN
		alter table TranPurchaseInvoice add RoundedOffInvoiceTotalAmount bigint
	END 
go
update TranPurchaseInvoice set RoundedOffInvoiceTotalAmount=ROUND(invoicetotalamount,0) where isnull(RoundedOffInvoiceTotalAmount,0)=0
go
IF NOT EXISTS(SELECT * FROM sys.columns
WHERE Name = N'RoundedOffQuotationTotalAmount' AND OBJECT_ID = OBJECT_ID(N'TranQuotation'))
	BEGIN
		alter table TranQuotation add RoundedOffQuotationTotalAmount bigint
	END 
go
update TranQuotation set RoundedOffQuotationTotalAmount=ROUND(QuotationTotalAmount,0) where isnull(RoundedOffQuotationTotalAmount,0)=0
go
IF NOT EXISTS(SELECT * FROM sys.columns
WHERE Name = N'CompanyDisplayName' AND OBJECT_ID = OBJECT_ID(N'MasterCompany'))
	BEGIN
		alter table MasterCompany add CompanyDisplayName varchar(200)
	END 
go
update MasterCompany set CompanyDisplayName=CompanyName where isnull(CompanyDisplayName,'')=''
go
IF NOT EXISTS(SELECT * FROM sys.columns
WHERE Name = N'UoMDisplayName' AND OBJECT_ID = OBJECT_ID(N'MasterItem'))
	BEGIN
		alter table MasterItem add UoMDisplayName varchar(20)
	END 
go
update MasterItem set UoMDisplayName=UoM where isnull(UoMDisplayName,'')=''
go
IF NOT EXISTS(SELECT * FROM sys.columns
WHERE Name = N'UoMDisplayName' AND OBJECT_ID = OBJECT_ID(N'MasterUoM'))
	BEGIN
		alter table MasterUoM add UoMDisplayName varchar(20)
	END 
go
update MasterUoM set UoMDisplayName=Code where isnull(UoMDisplayName,'')=''
go
IF NOT EXISTS(SELECT * FROM sys.columns
WHERE Name = N'StateStatus' AND OBJECT_ID = OBJECT_ID(N'MasterState'))
	BEGIN
		alter table MasterState add StateStatus varchar(10)
	END 
go
update MasterState set StateStatus='Active' where isnull(StateStatus,'')=''
GO
IF NOT EXISTS(SELECT * FROM sys.columns
WHERE Name = N'IdSuffix' AND OBJECT_ID = OBJECT_ID(N'SettingDocument'))
	BEGIN
		alter table SettingDocument add IdSuffix varchar(50)
	END 
go
update SettingDocument set IdSuffix='' where isnull(IdSuffix,'')=''
GO
IF NOT EXISTS(SELECT * FROM sys.columns
WHERE Name = N'StateCode' AND OBJECT_ID = OBJECT_ID(N'MasterCompany'))
	BEGIN
		alter table MasterCompany add StateCode varchar(2)
	END 
go
IF NOT EXISTS(SELECT * FROM sys.columns
WHERE Name = N'StateCode' AND OBJECT_ID = OBJECT_ID(N'MasterCustomer'))
	BEGIN
		alter table MasterCustomer add StateCode varchar(2)
	END 
go
DECLARE @cnt INT = 1;
declare @stateCode varchar(2)
declare @stateName varchar(100)
WHILE @cnt <= 37
BEGIN
select @stateCode = Code, @stateName = Name from MasterState where ID=@cnt
   update MasterCustomer set StateCode=@stateCode where State=@stateName
   update MasterCompany set StateCode=@stateCode where State=@stateName
   SET @cnt = @cnt + 1;
END;
GO
update MasterState set Code = '0' + Code where LEN(Code)=1
go
IF NOT EXISTS(SELECT * FROM sys.columns
WHERE Name = N'CFItem1' AND OBJECT_ID = OBJECT_ID(N'TranReceiptNoteItems'))
	BEGIN
		alter table [TranReceiptNoteItems] add CFItem1 varchar(50)
	END 
go
IF NOT EXISTS(SELECT * FROM sys.columns
WHERE Name = N'CFItem2' AND OBJECT_ID = OBJECT_ID(N'TranReceiptNoteItems'))
	BEGIN
		alter table [TranReceiptNoteItems] add CFItem2 varchar(50)
	END 
go
IF NOT EXISTS(SELECT * FROM sys.columns
WHERE Name = N'CFItem1' AND OBJECT_ID = OBJECT_ID(N'TranDeliveryNoteItems'))
	BEGIN
		alter table [TranDeliveryNoteItems] add CFItem1 varchar(50)
	END 
go
IF NOT EXISTS(SELECT * FROM sys.columns
WHERE Name = N'CFItem2' AND OBJECT_ID = OBJECT_ID(N'TranDeliveryNoteItems'))
	BEGIN
		alter table [TranDeliveryNoteItems] add CFItem2 varchar(50)
	END 
go
IF NOT EXISTS(SELECT * FROM sys.columns
WHERE Name = N'CFItem1' AND OBJECT_ID = OBJECT_ID(N'TranSalesInvoiceItems'))
	BEGIN
		alter table [TranSalesInvoiceItems] add CFItem1 varchar(50)
	END 
go
IF NOT EXISTS(SELECT * FROM sys.columns
WHERE Name = N'CFItem2' AND OBJECT_ID = OBJECT_ID(N'TranSalesInvoiceItems'))
	BEGIN
		alter table [TranSalesInvoiceItems] add CFItem2 varchar(50)
	END 
go
IF NOT EXISTS(SELECT * FROM sys.columns
WHERE Name = N'CFItem1' AND OBJECT_ID = OBJECT_ID(N'TranPurchaseInvoiceItems'))
	BEGIN
		alter table [TranPurchaseInvoiceItems] add CFItem1 varchar(50)
	END 
go
IF NOT EXISTS(SELECT * FROM sys.columns
WHERE Name = N'CFItem2' AND OBJECT_ID = OBJECT_ID(N'TranPurchaseInvoiceItems'))
	BEGIN
		alter table [TranPurchaseInvoiceItems] add CFItem2 varchar(50)
	END 
go
IF NOT EXISTS(SELECT * FROM sys.columns
WHERE Name = N'CFItem1' AND OBJECT_ID = OBJECT_ID(N'TranQuotationItems'))
	BEGIN
		alter table [TranQuotationItems] add CFItem1 varchar(50)
	END 
go
IF NOT EXISTS(SELECT * FROM sys.columns
WHERE Name = N'CFItem2' AND OBJECT_ID = OBJECT_ID(N'TranQuotationItems'))
	BEGIN
		alter table [TranQuotationItems] add CFItem2 varchar(50)
	END 
go
IF NOT EXISTS(SELECT * FROM sys.columns
WHERE Name = N'Weight' AND OBJECT_ID = OBJECT_ID(N'MasterItem'))
	BEGIN
		alter table [MasterItem] add [Weight] varchar(50)
	END 
go
IF NOT EXISTS(SELECT * FROM sys.columns
WHERE Name = N'ItemStatus' AND OBJECT_ID = OBJECT_ID(N'MasterItem'))
	BEGIN
		alter table [MasterItem] add ItemStatus varchar(10)
	END 
go
IF NOT EXISTS(SELECT * FROM sys.columns
WHERE Name = N'Category' AND OBJECT_ID = OBJECT_ID(N'TranSalesInvoice'))
	BEGIN
		alter table [TranSalesInvoice] add Category varchar(10)
	END 
go
update TranReceiptNoteItems set CFItem1='' where isnull(CFItem1,'')=''
go
update TranReceiptNoteItems set CFItem2='' where isnull(CFItem2,'')=''
go
update TranDeliveryNoteItems set CFItem1='' where isnull(CFItem1,'')=''
go
update TranDeliveryNoteItems set CFItem2='' where isnull(CFItem2,'')=''
GO
update TranSalesInvoiceItems set CFItem1='' where isnull(CFItem1,'')=''
go
update TranSalesInvoiceItems set CFItem2='' where isnull(CFItem2,'')=''
GO
update TranPurchaseInvoiceItems set CFItem1='' where isnull(CFItem1,'')=''
go
update TranPurchaseInvoiceItems set CFItem2='' where isnull(CFItem2,'')=''
GO
update TranQuotationItems set CFItem1='' where isnull(CFItem1,'')=''
go
update TranQuotationItems set CFItem2='' where isnull(CFItem2,'')=''
GO
update MasterItem set [Weight]='' where isnull([Weight],'')=''
go
update MasterItem set ItemStatus='Active' where isnull(ItemStatus,'')=''
GO
update TranSalesInvoice set Category='B2B' where isnull(Category,'')=''
GO
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES 
           WHERE TABLE_NAME = N'MasterUoM')
	BEGIN
		CREATE TABLE [dbo].[MasterUoM](
			[ID] [int] IDENTITY(1,1) NOT NULL,
			[Code] [varchar](3) NULL,
			[UoM] [varchar](20) NULL,
			UoMStatus varchar(10)
		) 
	END
GO
-- =============================================      
-- Author:  TK      
-- Create date: 15/10/2017      
-- Description: Item insert update and delete (If ID = 0 then it inserts else updates)      
-- =============================================      
alter PROCEDURE [dbo].[Master_Item_InsertUpdate]      
 @ID int=0,       
 @ItemDescription varchar(200),      
 @Size [varchar](50),      
 @UoM [varchar](20),      
 @ItemType [varchar](10),      
 @HsnSac [varchar](10),      
 @ItemSKUcode [varchar](50),      
 @SellingPrice [decimal](10, 2),      
 @PurchasePrice [decimal](10, 2),      
 @DiscountPercent [decimal](5, 2),      
 @TaxRatePercent [decimal](5, 2),      
 @ItemNotes [varchar](500),      
 @GroupIDUnder [int],      
 @DiscountAmount [decimal](10, 2),      
 @IsTaxIncluded [bit],      
 @BrandName [varchar](200),      
 @Weight [varchar](50),   
 @ItemStatus varchar(10),     
 @ItemImage [image],
 @UoMDisplayName varchar(20)      
AS      
BEGIN      
 SET NOCOUNT ON;      
 If (@ID = 0)      
 Begin      
  INSERT INTO [MasterItem]       
  ([ItemDescription],[Size],[UoM],[ItemType],[HsnSac]       
  ,[ItemSKUcode],[SellingPrice],[PurchasePrice]       
  ,[DiscountPercent],[TaxRatePercent],[ItemNotes]        
  ,[GroupIDUnder],[DiscountAmount],[IsTaxIncluded]      
  ,[BrandName],[Weight],ItemStatus,ItemImage,UoMDisplayName)       
  VALUES       
  (@ItemDescription, @Size, @UoM, @ItemType, @HsnSac       
  , @ItemSKUcode, @SellingPrice, @PurchasePrice       
  , @DiscountPercent, @TaxRatePercent, @ItemNotes      
  ,@GroupIDUnder,@DiscountAmount,@IsTaxIncluded      
  ,@BrandName,@Weight,@ItemStatus,@ItemImage,@UoMDisplayName      
  )      
 End      
 Else      
 Begin      
  UPDATE [dbo].[MasterItem]       
  SET [ItemDescription] = @ItemDescription       
  ,[Size] = @Size       
  ,[UoM] = @UoM       
  ,[ItemType] = @ItemType       
  ,[HsnSac] = @HsnSac       
  ,[ItemSKUcode] = @ItemSKUcode       
  ,[SellingPrice] = @SellingPrice       
  ,[PurchasePrice] = @PurchasePrice       
  ,[DiscountPercent] = @DiscountPercent       
  ,[TaxRatePercent] = @TaxRatePercent       
  ,[ItemNotes] = @ItemNotes       
  ,[GroupIDUnder] = @GroupIDUnder      
  ,[DiscountAmount] = @DiscountAmount      
  ,[IsTaxIncluded] = @IsTaxIncluded      
  ,[BrandName] = @BrandName    
  ,[Weight] = @Weight      
  ,[ItemStatus] = @ItemStatus  
  ,[ItemImage] = @ItemImage   
  ,UoMDisplayName = @UoMDisplayName    
  WHERE ID = @ID      
 End      
END      
GO

--Trans_MasterStockList 0,1,0,'',0,'GetStockList'  
-- =============================================  
-- Author:  TK  
-- Create date: 14/10/2017  
-- Description: Deals with table MasterStock  
ALTER PROCEDURE [dbo].[Trans_MasterStockList]  
 @StockID int=0,  
 @ItemID int=0,  
 @CompanyID int=0,  
 @GroupName varchar(200)='',  
 @FilterValue varchar(100)='',  
 @StockInHand decimal=0,  
 @mode varchar(50)  
AS  
BEGIN  
 SET NOCOUNT ON;  
 if (@mode = 'GetStockList')  
 Begin  
  insert into  MasterStockList(CompanyID, ItemID, StockInHand)   
  select @CompanyID,ID,0 from MasterItem where ID not in(select itemid from MasterStockList where CompanyID=@CompanyID) and Del_State=0  
  
  select  
  Stock.ID, Item.ID ItemID, Grp.GroupName,Item.ItemDescription,  
  Item.Size,Item.HsnSac,Item.UoM, Stock.StockInHand  
  from MasterStockList Stock   
  inner join MasterItem Item on Stock.ItemID=Item.ID and Item.Del_State=0   
  inner join vw_MasterGroup Grp   
  on Item.GroupIDUnder=Grp.ID   
  where Stock.CompanyID=@CompanyID and   
  (Item.ItemDescription like @FilterValue or Size like @FilterValue or UoM like @FilterValue or HsnSac like @FilterValue)  
  and GroupName in (select groupname from vw_MasterGroup where GroupName like @GroupName)
  order by Grp.GroupName, Item.ItemDescription   
 End  
 if (@mode = 'UpdateStockQuantity')  
 Begin  
  UPDATE [MasterStockList]  
  SET [StockInHand] = @StockInHand  
  WHERE ID = @StockID  
 End  
 if (@mode = 'InsertStockItem')  
 Begin  
  INSERT INTO [dbo].[MasterStockList]  
           ([CompanyID]  
           ,[ItemID]  
           ,[StockInHand])  
  VALUES  
           (@CompanyID  
     ,@ItemID  
     ,@StockInHand)  
 End  
 if (@mode = 'DeleteStockItem')  
 Begin  
  DELETE [MasterStockList]   
  WHERE ID = @StockID   
 End  
END
GO
alter PROCEDURE [dbo].[Trans_InvoiceItem_Save]        
 @InvoiceHeaderId int ,         
 @ItemId int,        
 @CompanyID int,        
 @ItemDescription varchar(200),        
 @ItemCustomDescription varchar(200),        
 @ItemType varchar(10),        
    @HsnSac varchar(10),        
 @Qty DECIMAL(10,2),        
 @UoM varchar(20),        
 @RatePerItem DECIMAL(10,2),        
 @DiscountPercent DECIMAL(5,2),        
 @Discount DECIMAL(10,2),        
    @TaxableValue DECIMAL(10,2),        
 @CGSTPercent DECIMAL(5,2),        
 @CGSTValue DECIMAL(10,2),        
    @SGSTPercent DECIMAL(5,2),        
 @SGSTValue DECIMAL(10,2),        
 @IGSTPercent DECIMAL(5,2),        
    @IGSTValue DECIMAL(10,2),        
 @CESSPercent DECIMAL(5,2),        
 @CESSValue DECIMAL(10,2),        
 @TotalValue DECIMAL(16,2),        
 @StockCompanyID int=0,   
 @CFItem1 varchar(20)='',       
 @CFItem2 varchar(20)='',       
 @mode varchar(50)        
AS        
BEGIN        
 SET NOCOUNT ON;        
 if (@mode = 'SalesInvoice-Insert')        
 Begin        
  INSERT INTO [TranSalesInvoiceItems]         
    ([SalesInvoiceHeaderID],[ItemID],[ItemDescription],[ItemCustomDescription],[ItemType]         
    ,[HsnSac],[Qty],[UoM],[RatePerItem],DiscountPercent,[Discount]         
    ,[TaxableValue],[CGSTPercent],[CGSTValue]         
    ,[SGSTPercent],[SGSTValue],[IGSTPercent]         
    ,[IGSTValue],[CESSPercent],[CESSValue],[TotalValue],CFItem1,CFItem2)         
    VALUES          
    (@InvoiceHeaderID, @ItemID, @ItemDescription, @ItemCustomDescription, @ItemType         
    , @HsnSac, @Qty, @UoM, @RatePerItem,@DiscountPercent, @Discount         
    , @TaxableValue, @CGSTPercent, @CGSTValue         
    , @SGSTPercent, @SGSTValue, @IGSTPercent         
    , @IGSTValue, @CESSPercent, @CESSValue, @TotalValue,@CFItem1,@CFItem2)        
 --Setting qty to negative counterpart so that it could reduce the stock at the time of sale        
 set @Qty=-@Qty        
 End        
 else if (@mode = 'PurchaseInvoice-Insert')        
 Begin        
  INSERT INTO [TranPurchaseInvoiceItems]        
     ([PurchaseInvoiceHeaderID],[ItemID],[ItemDescription],[ItemCustomDescription],[ItemType]        
     ,[HsnSac],[Qty],[UoM],[RatePerItem],DiscountPercent,[Discount]        
     ,[TaxableValue],[CGSTPercent],[CGSTValue]        
     ,[SGSTPercent],[SGSTValue],[IGSTPercent]        
     ,[IGSTValue],[CESSPercent],[CESSValue],[TotalValue],CFItem1,CFItem2)        
  VALUES         
     (@InvoiceHeaderId, @ItemID, @ItemDescription, @ItemCustomDescription, @ItemType        
     , @HsnSac, @Qty, @UoM, @RatePerItem,@DiscountPercent, @Discount        
     , @TaxableValue, @CGSTPercent, @CGSTValue        
     , @SGSTPercent, @SGSTValue, @IGSTPercent        
     , @IGSTValue, @CESSPercent, @CESSValue, @TotalValue,@CFItem1,@CFItem2)        
 End       
 else  if (@mode = 'DeliveryNote-Insert')        
 Begin        
  INSERT INTO [TranDeliveryNoteItems]         
    ([HeaderID],[ItemID],[ItemDescription],[ItemCustomDescription],[ItemType]         
    ,[HsnSac],[Qty],[UoM],[RatePerItem],DiscountPercent,[Discount]         
    ,[TaxableValue],[CGSTPercent],[CGSTValue]         
    ,[SGSTPercent],[SGSTValue],[IGSTPercent]         
    ,[IGSTValue],[CESSPercent],[CESSValue],[TotalValue],CFItem1,CFItem2)         
    VALUES          
    (@InvoiceHeaderID, @ItemID, @ItemDescription, @ItemCustomDescription, @ItemType         
    , @HsnSac, @Qty, @UoM, @RatePerItem,@DiscountPercent, @Discount         
    , @TaxableValue, @CGSTPercent, @CGSTValue         
    , @SGSTPercent, @SGSTValue, @IGSTPercent         
    , @IGSTValue, @CESSPercent, @CESSValue, @TotalValue,@CFItem1,@CFItem2)        
 --Setting qty to negative counterpart so that it could reduce the stock at the time of sale        
 set @Qty=-@Qty        
 End        
  else  if (@mode = 'ReceiptNote-Insert')        
 Begin        
  INSERT INTO [TranReceiptNoteItems]         
    ([HeaderID],[ItemID],[ItemDescription],[ItemCustomDescription],[ItemType]         
    ,[HsnSac],[Qty],[UoM],[RatePerItem],DiscountPercent,[Discount]         
    ,[TaxableValue],[CGSTPercent],[CGSTValue]         
    ,[SGSTPercent],[SGSTValue],[IGSTPercent]         
    ,[IGSTValue],[CESSPercent],[CESSValue],[TotalValue],CFItem1,CFItem2)         
    VALUES          
    (@InvoiceHeaderID, @ItemID, @ItemDescription, @ItemCustomDescription, @ItemType         
    , @HsnSac, @Qty, @UoM, @RatePerItem,@DiscountPercent, @Discount         
    , @TaxableValue, @CGSTPercent, @CGSTValue         
    , @SGSTPercent, @SGSTValue, @IGSTPercent         
    , @IGSTValue, @CESSPercent, @CESSValue, @TotalValue,@CFItem1,@CFItem2)         
 End      
 exec Trans_StockUpdate @ItemId,@StockCompanyID, @Qty          
        
END     
GO
-- =============================================  
-- Author:  TK  
-- Create date: 15 Dec 2017  
-- Description: Transactions applicable for GSTR1  
-- =============================================  
alter PROCEDURE [dbo].[GetGSTR1Info]   
 -- Add the parameters for the stored procedure here  
 @CompanyID int = 0,   
 @FromDate DateTime,  
 @ToDate DateTime,
 @Mode varchar(20)
AS  
BEGIN  
 -- SET NOCOUNT ON added to prevent extra result sets from  
 -- interfering with SELECT statements.  
SET NOCOUNT ON; 
	IF @Mode=1 -- SalesInvoiceWiseTaxCalc=0
	BEGIN
	  -- Itemwise Tax -- GSTN Register Customers (B2B)   
	  SELECT Invoice.ID,[InvoiceNumber] ,REPLACE(CONVERT(NVARCHAR,CAST([InvoiceDate] AS DATETIME), 106), ' ', '-') InvoiceDate  
	  ,sum(IGSTValue + [CGSTValue] + [SGSTValue])  TotalTaxAmount  
	  ,sum([CESSValue]) CESSAmount,sum([TaxableValue]) TaxableAmount,sum(TotalValue) InvoiceTotalAmount  
	  ,avg(IGSTPercent + CGSTPercent + SGSTPercent) TaxRate  
	  ,Comp.GSTN CompanyGSTN,Cust.GSTN CustomerGSTN,Comp.StateCode +'-'+ Comp.State CompanyState,Cust.StateCode +'-'+ Cust.State CustomerState  
	  FROM [TranSalesInvoice] Invoice   
	  inner join (select DISTINCT SalesInvoiceHeaderID,TaxableValue, IGSTPercent  
	  ,CGSTPercent,SGSTPercent,IGSTValue,CGSTValue,SGSTValue,CESSValue,TotalValue   
	  from TranSalesInvoiceItems where Del_State=0) InvoiceItems   
	  on Invoice.ID=InvoiceItems.SalesInvoiceHeaderID and Invoice.del_state='0' and InvoiceStatus<>'Canceled'   
	  and IGSTPercentTotal + CGSTPercentTotal + SGSTPercentTotal <>0 --Excempted Goods --When SalesInvoiceWiseTaxCalc = 1  
	  and InvoiceDate between @FromDate and @ToDate  
	  inner join MasterCompany Comp on Invoice.CompanyID = Comp.ID and Comp.ID = @CompanyID  
	  inner join MasterCustomer Cust on Invoice.CustomerID = Cust.ID  
	  group by Invoice.ID,[InvoiceNumber] ,[InvoiceDate]   
	  ,Invoice.CompanyID, Invoice.CustomerID  
	  , IGSTPercent + CGSTPercent + SGSTPercent  
	  ,Comp.GSTN ,Cust.GSTN ,Comp.StateCode,Comp.State ,Cust.StateCode,Cust.State   
  
	  -- Itemwise Tax -- GSTN Un Register Customers (B2CL) - Only Other States and total Value > 250000  
	  SELECT Invoice.ID,[InvoiceNumber] ,REPLACE(CONVERT(NVARCHAR,CAST([InvoiceDate] AS DATETIME), 106), ' ', '-') InvoiceDate  
	  ,sum(IGSTValue + [CGSTValue] + [SGSTValue])  TotalTaxAmount  
	  ,sum([CESSValue]) CESSAmount,sum([TaxableValue]) TaxableAmount,sum(TotalValue) InvoiceTotalAmount  
	  ,avg(IGSTPercent + CGSTPercent + SGSTPercent) TaxRate  
	  ,Comp.GSTN CompanyGSTN,Comp.StateCode +'-'+ Comp.State CompanyState,[State].Code +'-'+ CustomerState CustomerState  
	  FROM [TranSalesInvoice] Invoice   
	  inner join (select DISTINCT SalesInvoiceHeaderID,TaxableValue, IGSTPercent  
	  ,CGSTPercent,SGSTPercent,IGSTValue,CGSTValue,SGSTValue,CESSValue,TotalValue   
	  from TranSalesInvoiceItems where Del_State=0) InvoiceItems   
	  on Invoice.ID=InvoiceItems.SalesInvoiceHeaderID and Invoice.del_state='0' and InvoiceStatus<>'Canceled'   
	  and IGSTPercentTotal + CGSTPercentTotal + SGSTPercentTotal <>0 --Excempted Goods --When SalesInvoiceWiseTaxCalc = 1  
	  and CustomerID=0  
	  and InvoiceDate between @FromDate and @ToDate  
	  inner join MasterCompany Comp on Invoice.CompanyID = Comp.ID and Comp.ID = @CompanyID  
	  inner join MasterState [State] on [State].Name=CustomerState  
	  where InvoiceTotalAmount>250000 and Comp.State<>CustomerState  
	  group by Invoice.ID,[InvoiceNumber] ,InvoiceDate   
	  ,Invoice.CompanyID, Invoice.CustomerID  
	  , IGSTPercent + CGSTPercent + SGSTPercent  
	  ,Comp.GSTN , Comp.StateCode,Comp.State ,[State].Code,CustomerState  
  
	   -- Itemwise Tax -- GSTN Un Register Customers (B2CS) - Same State Any Value or total Value <= 250000  
	  SELECT Invoice.ID  
	  ,sum(IGSTValue + [CGSTValue] + [SGSTValue])  TotalTaxAmount  
	  ,sum([CESSValue]) CESSAmount,sum([TaxableValue]) TaxableAmount,sum(TotalValue) InvoiceTotalAmount  
	  ,avg(IGSTPercent + CGSTPercent + SGSTPercent) TaxRate  
	  ,Comp.GSTN CompanyGSTN,Comp.StateCode +'-'+ Comp.State CompanyState,[State].Code +'-'+ CustomerState CustomerState  
	  FROM [TranSalesInvoice] Invoice   
	  inner join (select DISTINCT SalesInvoiceHeaderID,TaxableValue, IGSTPercent  
	  ,CGSTPercent,SGSTPercent,IGSTValue,CGSTValue,SGSTValue,CESSValue,TotalValue   
	  from TranSalesInvoiceItems where Del_State=0) InvoiceItems   
	  on Invoice.ID=InvoiceItems.SalesInvoiceHeaderID and Invoice.del_state='0' and InvoiceStatus<>'Canceled'   
	  and IGSTPercentTotal + CGSTPercentTotal + SGSTPercentTotal <>0 --Excempted Goods --When SalesInvoiceWiseTaxCalc = 1  
	  and CustomerID=0  
	  and InvoiceDate between @FromDate and @ToDate  
	  inner join MasterCompany Comp on Invoice.CompanyID = Comp.ID and Comp.ID = @CompanyID  
	  inner join MasterState [State] on [State].Name=CustomerState  
	  where InvoiceTotalAmount<=250000 or Comp.State=CustomerState  
	  group by Invoice.ID   
	  ,Invoice.CompanyID, Invoice.CustomerID  
	  , IGSTPercent + CGSTPercent + SGSTPercent  
	  ,Comp.GSTN ,Comp.StateCode,Comp.State ,[State].Code,CustomerState  
  
	   -- Summary for HSN    
	  SELECT HsnSac, case when HsnSac='' then ItemDescription else '' end ItemDescription,MasterUoM.Code +'-'+ MasterUoM.UoM UoM  
	  ,sum(Qty) TotalQty  
	  ,sum(IGSTValue) TotalIGSTAmount, sum(CGSTValue) TotalCGSTAmount, sum(SGSTValue)  TotalSGSTAmount  
	  ,sum([CESSValue]) CESSAmount,sum([TaxableValue]) TaxableAmount,sum(TotalValue) InvoiceTotalAmount  
	  FROM [TranSalesInvoice] Invoice   
	  inner join TranSalesInvoiceItems InvoiceItems   
	  on Invoice.ID=InvoiceItems.SalesInvoiceHeaderID and Invoice.del_state='0' and InvoiceItems.del_state='0' and InvoiceStatus<>'Canceled'   
	  and InvoiceDate between @FromDate and @ToDate and CompanyID = @CompanyID  
	  left Join MasterUoM on InvoiceItems.UoM = MasterUoM.UoM  
	  group by HsnSac,case when HsnSac='' then ItemDescription else '' end,MasterUoM.Code, MasterUoM.UoM   
	END
	ELSE IF @Mode=0 -- SalesInvoiceWiseTaxCalc=1 
	BEGIN
	  -- Itemwise Tax -- GSTN Register Customers (B2B)   
	  SELECT Invoice.ID,[InvoiceNumber] ,REPLACE(CONVERT(NVARCHAR,CAST([InvoiceDate] AS DATETIME), 106), ' ', '-') InvoiceDate  
	  ,sum(IGSTValue + [CGSTValue] + [SGSTValue])  TotalTaxAmount  
	  ,sum([CESSValue]) CESSAmount,sum([TaxableValue]) TaxableAmount,sum(TotalValue) InvoiceTotalAmount  
	  ,avg(IGSTPercent + CGSTPercent + SGSTPercent) TaxRate  
	  ,Comp.GSTN CompanyGSTN,Cust.GSTN CustomerGSTN,Comp.StateCode +'-'+ Comp.State CompanyState,Cust.StateCode +'-'+ Cust.State CustomerState  
	  FROM [TranSalesInvoice] Invoice   
	  inner join (select DISTINCT SalesInvoiceHeaderID,TaxableValue, IGSTPercent  
	  ,CGSTPercent,SGSTPercent,IGSTValue,CGSTValue,SGSTValue,CESSValue,TotalValue   
	  from TranSalesInvoiceItems where Del_State=0) InvoiceItems   
	  on Invoice.ID=InvoiceItems.SalesInvoiceHeaderID and Invoice.del_state='0' and InvoiceStatus<>'Canceled'   
	  and IGSTPercent + CGSTPercent + SGSTPercent <>0 --Excempted Goods - When SalesInvoiceWiseTaxCalc = 0  
	  and InvoiceDate between @FromDate and @ToDate  
	  inner join MasterCompany Comp on Invoice.CompanyID = Comp.ID and Comp.ID = @CompanyID  
	  inner join MasterCustomer Cust on Invoice.CustomerID = Cust.ID  
	  group by Invoice.ID,[InvoiceNumber] ,[InvoiceDate]   
	  ,Invoice.CompanyID, Invoice.CustomerID  
	  , IGSTPercent + CGSTPercent + SGSTPercent  
	  ,Comp.GSTN ,Cust.GSTN ,Comp.StateCode,Comp.State ,Cust.StateCode,Cust.State   
  
	  -- Itemwise Tax -- GSTN Un Register Customers (B2CL) - Only Other States and total Value > 250000  
	  SELECT Invoice.ID,[InvoiceNumber] ,REPLACE(CONVERT(NVARCHAR,CAST([InvoiceDate] AS DATETIME), 106), ' ', '-') InvoiceDate  
	  ,sum(IGSTValue + [CGSTValue] + [SGSTValue])  TotalTaxAmount  
	  ,sum([CESSValue]) CESSAmount,sum([TaxableValue]) TaxableAmount,sum(TotalValue) InvoiceTotalAmount  
	  ,avg(IGSTPercent + CGSTPercent + SGSTPercent) TaxRate  
	  ,Comp.GSTN CompanyGSTN,Comp.StateCode +'-'+ Comp.State CompanyState,[State].Code +'-'+ CustomerState CustomerState  
	  FROM [TranSalesInvoice] Invoice   
	  inner join (select DISTINCT SalesInvoiceHeaderID,TaxableValue, IGSTPercent  
	  ,CGSTPercent,SGSTPercent,IGSTValue,CGSTValue,SGSTValue,CESSValue,TotalValue   
	  from TranSalesInvoiceItems where Del_State=0) InvoiceItems   
	  on Invoice.ID=InvoiceItems.SalesInvoiceHeaderID and Invoice.del_state='0' and InvoiceStatus<>'Canceled'   
	  and IGSTPercent + CGSTPercent + SGSTPercent <>0 --Excempted Goods - When SalesInvoiceWiseTaxCalc = 0  
	  and CustomerID=0  
	  and InvoiceDate between @FromDate and @ToDate  
	  inner join MasterCompany Comp on Invoice.CompanyID = Comp.ID and Comp.ID = @CompanyID  
	  inner join MasterState [State] on [State].Name=CustomerState  
	  where InvoiceTotalAmount>250000 and Comp.State<>CustomerState  
	  group by Invoice.ID,[InvoiceNumber] ,InvoiceDate   
	  ,Invoice.CompanyID, Invoice.CustomerID  
	  , IGSTPercent + CGSTPercent + SGSTPercent  
	  ,Comp.GSTN , Comp.StateCode,Comp.State ,[State].Code,CustomerState  
  
	   -- Itemwise Tax -- GSTN Un Register Customers (B2CS) - Same State Any Value or total Value <= 250000  
	  SELECT Invoice.ID  
	  ,sum(IGSTValue + [CGSTValue] + [SGSTValue])  TotalTaxAmount  
	  ,sum([CESSValue]) CESSAmount,sum([TaxableValue]) TaxableAmount,sum(TotalValue) InvoiceTotalAmount  
	  ,avg(IGSTPercent + CGSTPercent + SGSTPercent) TaxRate  
	  ,Comp.GSTN CompanyGSTN,Comp.StateCode +'-'+ Comp.State CompanyState,[State].Code +'-'+ CustomerState CustomerState  
	  FROM [TranSalesInvoice] Invoice   
	  inner join (select DISTINCT SalesInvoiceHeaderID,TaxableValue, IGSTPercent  
	  ,CGSTPercent,SGSTPercent,IGSTValue,CGSTValue,SGSTValue,CESSValue,TotalValue   
	  from TranSalesInvoiceItems where Del_State=0) InvoiceItems   
	  on Invoice.ID=InvoiceItems.SalesInvoiceHeaderID and Invoice.del_state='0' and InvoiceStatus<>'Canceled'   
	  and IGSTPercent + CGSTPercent + SGSTPercent <>0 --Excempted Goods - When SalesInvoiceWiseTaxCalc = 0  
	  and CustomerID=0  
	  and InvoiceDate between @FromDate and @ToDate  
	  inner join MasterCompany Comp on Invoice.CompanyID = Comp.ID and Comp.ID = @CompanyID  
	  inner join MasterState [State] on [State].Name=CustomerState  
	  where InvoiceTotalAmount<=250000 or Comp.State=CustomerState  
	  group by Invoice.ID   
	  ,Invoice.CompanyID, Invoice.CustomerID  
	  , IGSTPercent + CGSTPercent + SGSTPercent  
	  ,Comp.GSTN ,Comp.StateCode,Comp.State ,[State].Code,CustomerState  
  
	   -- Summary for HSN    
	  SELECT HsnSac, case when HsnSac='' then ItemDescription else '' end ItemDescription,MasterUoM.Code +'-'+ MasterUoM.UoM UoM  
	  ,sum(Qty) TotalQty  
	  ,sum(IGSTValue) TotalIGSTAmount, sum(CGSTValue) TotalCGSTAmount, sum(SGSTValue)  TotalSGSTAmount  
	  ,sum([CESSValue]) CESSAmount,sum([TaxableValue]) TaxableAmount,sum(TotalValue) InvoiceTotalAmount  
	  FROM [TranSalesInvoice] Invoice   
	  inner join TranSalesInvoiceItems InvoiceItems   
	  on Invoice.ID=InvoiceItems.SalesInvoiceHeaderID and Invoice.del_state='0' and InvoiceItems.del_state='0' and InvoiceStatus<>'Canceled'   
	  and InvoiceDate between @FromDate and @ToDate and CompanyID = @CompanyID  
	  left Join MasterUoM on InvoiceItems.UoM = MasterUoM.UoM  
	  group by HsnSac,case when HsnSac='' then ItemDescription else '' end,MasterUoM.Code, MasterUoM.UoM   
	END
END  
  
GO

ALTER PROCEDURE [dbo].[CheckAvailabilityInvoiceNo]      
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
    select count(*) Cnt from transalesinvoice   
    where Del_State=0   
    and InvoiceNumber = @InvoiceNumber  
    and CompanyID = @CompanyID  
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
-- =============================================    
-- Author:  TK    
-- Create date: 27-Aug-2017    
-- Description: To Get auto sequnce number for Sales Invoice.    
-- =============================================    
alter PROCEDURE [dbo].[GetAutoSequenceNumber]    
 -- Add the parameters for the stored procedure here    
 @CompanyId int,    
 @DocumentType varchar(100),    
 @mode varchar(20)     
AS    
BEGIN    
 SET NOCOUNT ON;    
    
 declare @IsAutoDocumentSequanceNumber bit    
 declare @IdPrefix varchar(50)    
 declare @IdSuffix varchar(50)    
 declare @DocumentIdResetFlag bit    
    
 select @IsAutoDocumentSequanceNumber = IsAutoDocumentSequanceNumber, @IdPrefix =IdPrefix, @IdSuffix = IdSuffix, @DocumentIdResetFlag=DocumentIdResetFlag     
 from SettingDocument where CompanyId=@CompanyId and DocumentType=@DocumentType    
    
 if (@mode = 'NewInvoice')    
 Begin    
  --select @IsAutoDocumentSequanceNumber, @IdPrefix, @DocumentIdResetFlag    
    
  if (@IsAutoDocumentSequanceNumber=1)     
  Begin    
   if (@DocumentIdResetFlag=1)     
   begin    
    select convert(varchar(20),IdSeriesStart) InvoiceNumber, DocumentIdResetFlag, IdPrefix, IdSuffix from SettingDocument where CompanyId=@CompanyId and DocumentType=@DocumentType    
   end    
   else    
   begin    
    if(@DocumentType='Sales Invoice')    
     Begin    
      select convert(varchar(20),max(convert(int, replace(replace(InvoiceNumber,@IdSuffix,''),@IdPrefix,'')))+1) InvoiceNumber,    
      CONVERT(bit, 0) DocumentIdResetFlag, @IdPrefix IdPrefix, @IdSuffix IdSuffix from [TranSalesInvoice] where CompanyID=@CompanyId    
      and id>(select maxDocumentIdWhileResettingSequence from [SettingDocumentIdResetDetail] where CompanyId=@CompanyId and DocumentType=@DocumentType)    
     End    
        
    else if(@DocumentType='Purchase Invoice')    
     Begin    
      select convert(varchar(20),max(convert(int, replace(replace(InvoiceNumber,@IdSuffix,''),@IdPrefix,'')))+1) InvoiceNumber,    
      CONVERT(bit, 0) DocumentIdResetFlag, @IdPrefix IdPrefix, @IdSuffix IdSuffix from [TranPurchaseInvoice] where CompanyID=@CompanyId    
      and id>(select maxDocumentIdWhileResettingSequence from [SettingDocumentIdResetDetail] where CompanyId=@CompanyId and DocumentType=@DocumentType)    
     End    
    else if(@DocumentType='Quotation')    
     Begin    
      select convert(varchar(20),max(convert(int, replace(replace(QuotationNumber,@IdSuffix,''),@IdPrefix,'')))+1) InvoiceNumber,    
      CONVERT(bit, 0) DocumentIdResetFlag, @IdPrefix IdPrefix, @IdSuffix IdSuffix from [TranQuotation] where CompanyID=@CompanyId    
      and id>(select maxDocumentIdWhileResettingSequence from [SettingDocumentIdResetDetail] where CompanyId=@CompanyId and DocumentType=@DocumentType)    
     End    
    else if(@DocumentType='Delivery Note')    
     Begin    
      select convert(varchar(20),max(convert(int, replace(replace(NoteNumber,@IdSuffix,''),@IdPrefix,'')))+1) InvoiceNumber,    
      CONVERT(bit, 0) DocumentIdResetFlag, @IdPrefix IdPrefix, @IdSuffix IdSuffix from [TranDeliveryNote] where CompanyID=@CompanyId    
      and id>(select maxDocumentIdWhileResettingSequence from [SettingDocumentIdResetDetail] where CompanyId=@CompanyId and DocumentType=@DocumentType)    
     End    
    else if(@DocumentType='Receipt Note')    
     Begin    
      select convert(varchar(20),max(convert(int, replace(replace(NoteNumber,@IdSuffix,''),@IdPrefix,'')))+1) InvoiceNumber,    
      CONVERT(bit, 0) DocumentIdResetFlag, @IdPrefix IdPrefix, @IdSuffix IdSuffix from [TranReceiptNote] where CompanyID=@CompanyId    
      and id>(select maxDocumentIdWhileResettingSequence from [SettingDocumentIdResetDetail] where CompanyId=@CompanyId and DocumentType=@DocumentType)    
     End   
    end    
   End    
  else    
   Begin    
    select 'ManualNewInvoice' InvoiceNumber, CONVERT(bit, 0) DocumentIdResetFlag, '' IdPrefix, '' IdSuffix 
   End    
 End    
 else if (@mode = 'ExistingInvoice')    
  Begin    
if (@IsAutoDocumentSequanceNumber=1)     
   Begin    
    select 'AutoExistingInvoice' InvoiceNumber,CONVERT(bit, 0) DocumentIdResetFlag, '' IdPrefix, '' IdSuffix     
   End    
  else    
   Begin    
    select 'ManualExistingInvoice' InvoiceNumber,CONVERT(bit, 0) DocumentIdResetFlag, '' IdPrefix, '' IdSuffix     
   End    
  End    
 else    
  Begin    
   UPDATE [SettingDocument] SET [DocumentIdResetFlag] = 0 WHERE  CompanyId = @CompanyId and DocumentType = @DocumentType    
  End    
END    
GO