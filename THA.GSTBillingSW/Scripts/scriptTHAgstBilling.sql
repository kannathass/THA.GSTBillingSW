USE [master]
GO
/****** Object:  Database [THAgstBilling]    Script Date: 05-Oct-2017 9:26:09 PM ******/
CREATE DATABASE [THAgstBilling]
GO
----------------------------------------------------------------------------------------------------------------------------------------------------------------------
USE [THAgstBilling]
GO
/****** Object:  Table [dbo].[MasterGroup]    Script Date: 15-Feb-2018 3:58:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MasterGroup](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[GroupName] [varchar](200) NULL,
	[GroupDescription] [varchar](500) NULL,
	[GroupIDUnder] [int] NULL,
	[Location] [varchar](200) NULL,
	[Del_State] [bit] NULL,
	[Del_Date] [datetime] NULL
) ON [PRIMARY]

GO
/****** Object:  View [dbo].[vw_MasterGroup]    Script Date: 15-Feb-2018 3:58:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE view [dbo].[vw_MasterGroup]
as
Select 0 ID,'Primary' GroupName
UNION
Select ID,GroupName from MasterGroup where Del_State=0

GO
/****** Object:  Table [dbo].[MasterCompany]    Script Date: 15-Feb-2018 3:58:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MasterCompany](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CompanyName] [varchar](200) NOT NULL,
	[Address] [varchar](500) NULL,
	[State] [varchar](100) NULL,
	[PinCode] [varchar](50) NULL,
	[PAN] [varchar](50) NULL,
	[GSTN] [varchar](50) NULL,
	[GSTNPortalUserName] [varchar](100) NULL,
	[Email] [varchar](100) NULL,
	[Phone] [varchar](100) NULL,
	[Mobile] [varchar](100) NULL,
	[Website] [varchar](100) NULL,
	[BankName] [varchar](100) NULL,
	[Branch] [varchar](100) NULL,
	[AccountNumber] [varchar](20) NULL,
	[IFSC] [varchar](20) NULL,
	[Del_State] [bit] NULL,
	[Del_Date] [datetime] NULL,
	[StateCode] [varchar](2) NULL,
	[CompanyDisplayName] [varchar](200) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MasterCustomer]    Script Date: 15-Feb-2018 3:58:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MasterCustomer](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CustomerName] [varchar](200) NOT NULL,
	[BillingAddress] [varchar](500) NULL,
	[ShippingAddress] [varchar](500) NULL,
	[State] [varchar](100) NULL,
	[PinCode] [varchar](50) NULL,
	[PAN] [varchar](50) NULL,
	[GSTN] [varchar](50) NULL,
	[GSTNPortalUserName] [varchar](100) NULL,
	[ContactPerson] [varchar](100) NULL,
	[Email] [varchar](100) NULL,
	[Phone] [varchar](100) NULL,
	[Mobile] [varchar](100) NULL,
	[Website] [varchar](100) NULL,
	[BankName] [varchar](100) NULL,
	[Branch] [varchar](100) NULL,
	[AccountNumber] [varchar](20) NULL,
	[IFSC] [varchar](20) NULL,
	[CustomerStatus] [varchar](10) NULL,
	[Comments] [varchar](500) NULL,
	[Del_State] [bit] NULL,
	[Del_Date] [datetime] NULL,
	[CustomerType] [varchar](50) NULL,
	[StateCode] [varchar](2) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MasterItem]    Script Date: 15-Feb-2018 3:58:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MasterItem](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ItemDescription] [varchar](200) NOT NULL,
	[Size] [varchar](50) NULL,
	[UoM] [varchar](20) NULL,
	[ItemType] [varchar](10) NULL,
	[HsnSac] [varchar](10) NULL,
	[ItemSKUcode] [varchar](50) NULL,
	[SellingPrice] [decimal](10, 2) NULL,
	[PurchasePrice] [decimal](10, 2) NULL,
	[DiscountPercent] [decimal](5, 2) NULL,
	[TaxRatePercent] [decimal](5, 2) NULL,
	[ItemNotes] [varchar](500) NULL,
	[ItemImage] [image] NULL,
	[Del_State] [bit] NULL,
	[Del_Date] [datetime] NULL,
	[GroupIDUnder] [int] NULL,
	[DiscountAmount] [decimal](10, 2) NULL,
	[IsTaxIncluded] [bit] NULL,
	[BrandName] [varchar](200) NULL,
	[Weight] [varchar](50) NULL,
	[ItemStatus] [varchar](10) NULL,
	[UoMDisplayName] [varchar](20) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MasterState]    Script Date: 15-Feb-2018 3:58:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MasterState](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Code] [varchar](2) NULL,
	[Name] [varchar](100) NULL,
	[StateStatus] [varchar](10) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MasterStockList]    Script Date: 15-Feb-2018 3:58:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MasterStockList](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CompanyID] [int] NOT NULL,
	[ItemID] [int] NOT NULL,
	[StockInHand] [decimal](10, 2) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MasterUoM]    Script Date: 15-Feb-2018 3:58:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MasterUoM](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Code] [varchar](3) NULL,
	[UoM] [varchar](20) NULL,
	[UoMStatus] [varchar](10) NULL,
	[UoMDisplayName] [varchar](20) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SettingCustomFields]    Script Date: 15-Feb-2018 3:58:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SettingCustomFields](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [int] NOT NULL,
	[DocumentType] [varchar](100) NULL,
	[CF1] [varchar](100) NULL,
	[CF2] [varchar](100) NULL,
	[CF3] [varchar](100) NULL,
	[CF4] [varchar](100) NULL,
	[CF5] [varchar](100) NULL,
	[CF6] [varchar](100) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SettingDetce]    Script Date: 15-Feb-2018 3:58:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SettingDetce](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](200) NULL,
	[TeLaire] [varchar](50) NULL,
	[MoLaire] [varchar](50) NULL,
	[EaLaire] [varchar](50) NULL,
	[Latus] [varchar](50) NULL,
	[Usnu] [varchar](50) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SettingDocument]    Script Date: 15-Feb-2018 3:58:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SettingDocument](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [int] NOT NULL,
	[DocumentType] [varchar](100) NULL,
	[IsAutoDocumentSequanceNumber] [bit] NULL,
	[IdPrefix] [varchar](50) NULL,
	[IdSeriesStart] [int] NULL,
	[DocumentIdResetFlag] [bit] NULL,
	[IdSuffix] [varchar](50) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SettingDocumentIdResetDetail]    Script Date: 15-Feb-2018 3:58:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SettingDocumentIdResetDetail](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [int] NOT NULL,
	[DocumentType] [varchar](100) NULL,
	[MaxDocumentIdWhileResettingSequence] [bigint] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SettingLogo]    Script Date: 15-Feb-2018 3:58:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SettingLogo](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [int] NOT NULL,
	[Companylogo] [image] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SettingTerms]    Script Date: 15-Feb-2018 3:58:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SettingTerms](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [int] NOT NULL,
	[DocumentType] [varchar](100) NULL,
	[TermsAndConditions] [varchar](500) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SettingUser]    Script Date: 15-Feb-2018 3:58:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SettingUser](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](50) NULL,
	[Password] [varchar](50) NULL,
	[PasswordHint] [varchar](50) NULL,
	[Role] [varchar](50) NULL,
	[Privilege] [varchar](50) NULL,
	[LastLoggedIn] [datetime] NULL,
	[IsLoggedIn] [bit] NULL,
	[IsUserActive] [bit] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TranDeliveryNote]    Script Date: 15-Feb-2018 3:58:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TranDeliveryNote](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[CompanyID] [int] NULL,
	[NoteNumber] [varchar](20) NULL,
	[NoteDate] [date] NULL,
	[CustomerID] [int] NULL,
	[ReferenceNumber] [varchar](50) NULL,
	[ReferenceDate] [date] NULL,
	[NoteType] [varchar](50) NULL,
	[CGSTAmount] [decimal](10, 2) NULL,
	[SGSTAmount] [decimal](10, 2) NULL,
	[IGSTAmount] [decimal](10, 2) NULL,
	[CESSAmount] [decimal](10, 2) NULL,
	[TaxableAmount] [decimal](16, 2) NULL,
	[TaxableAmountWithDisc] [decimal](16, 2) NULL,
	[CESSPercentTotal] [decimal](5, 2) NULL,
	[SGSTPercentTotal] [decimal](5, 2) NULL,
	[CGSTPercentTotal] [decimal](5, 2) NULL,
	[IGSTPercentTotal] [decimal](5, 2) NULL,
	[DiscountPercentTotal] [decimal](5, 2) NULL,
	[DiscountAmount] [decimal](10, 2) NULL,
	[NoteTotalAmount] [decimal](16, 2) NULL,
	[RoundedOffNoteTotalAmount] [bigint] NULL,
	[NoteStatus] [varchar](50) NULL,
	[ExtraNotes] [varchar](1000) NULL,
	[TermsAndCondition] [varchar](500) NULL,
	[isCessApplicable] [bit] NULL,
	[IsReverseCharge] [bit] NULL,
	[CF1] [varchar](100) NULL,
	[CF2] [varchar](100) NULL,
	[CF3] [varchar](100) NULL,
	[CF4] [varchar](100) NULL,
	[CF5] [varchar](100) NULL,
	[CF6] [varchar](250) NULL,
	[CreatedBy] [varchar](50) NULL,
	[CreatedOn] [datetime] NULL,
	[Del_State] [bit] NULL,
	[Del_Date] [datetime] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TranDeliveryNoteItems]    Script Date: 15-Feb-2018 3:58:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TranDeliveryNoteItems](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[HeaderID] [bigint] NULL,
	[ItemID] [int] NULL,
	[ItemDescription] [varchar](200) NULL,
	[ItemCustomDescription] [varchar](200) NULL,
	[ItemType] [varchar](10) NULL,
	[HsnSac] [varchar](10) NULL,
	[Qty] [decimal](10, 2) NULL,
	[UoM] [varchar](20) NULL,
	[RatePerItem] [decimal](10, 2) NULL,
	[DiscountPercent] [decimal](5, 2) NULL,
	[Discount] [decimal](10, 2) NULL,
	[TaxableValue] [decimal](10, 2) NULL,
	[CGSTPercent] [decimal](5, 2) NULL,
	[CGSTValue] [decimal](10, 2) NULL,
	[SGSTPercent] [decimal](5, 2) NULL,
	[SGSTValue] [decimal](10, 2) NULL,
	[IGSTPercent] [decimal](5, 2) NULL,
	[IGSTValue] [decimal](10, 2) NULL,
	[CESSPercent] [decimal](5, 2) NULL,
	[CESSValue] [decimal](10, 2) NULL,
	[TotalValue] [decimal](16, 2) NULL,
	[Del_State] [bit] NULL,
	[Del_Date] [datetime] NULL,
	[CFItem1] [varchar](50) NULL,
	[CFItem2] [varchar](50) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TranPurchaseInvoice]    Script Date: 15-Feb-2018 3:58:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TranPurchaseInvoice](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[CompanyID] [int] NULL,
	[InvoiceNumber] [varchar](20) NULL,
	[InvoiceDate] [date] NULL,
	[InvoiceDueDate] [date] NULL,
	[CustomerID] [int] NULL,
	[PONumber] [varchar](50) NULL,
	[PODate] [date] NULL,
	[CGSTAmount] [decimal](10, 2) NULL,
	[SGSTAmount] [decimal](10, 2) NULL,
	[IGSTAmount] [decimal](10, 2) NULL,
	[CESSAmount] [decimal](10, 2) NULL,
	[TaxableAmount] [decimal](16, 2) NULL,
	[InvoiceTotalAmount] [decimal](16, 2) NULL,
	[InvoiceStatus] [varchar](50) NULL,
	[CustomerNotes] [varchar](1000) NULL,
	[isCessApplicable] [bit] NULL,
	[CF1] [varchar](100) NULL,
	[CF2] [varchar](100) NULL,
	[CF3] [varchar](100) NULL,
	[CF4] [varchar](100) NULL,
	[CF5] [varchar](100) NULL,
	[CreatedBy] [varchar](50) NULL,
	[CreatedOn] [datetime] NULL,
	[Del_State] [bit] NULL,
	[Del_Date] [datetime] NULL,
	[RoundedOffInvoiceTotalAmount] [bigint] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TranPurchaseInvoiceItems]    Script Date: 15-Feb-2018 3:58:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TranPurchaseInvoiceItems](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[PurchaseInvoiceHeaderID] [bigint] NULL,
	[ItemID] [int] NULL,
	[ItemDescription] [varchar](200) NULL,
	[ItemCustomDescription] [varchar](200) NULL,
	[ItemType] [varchar](10) NULL,
	[HsnSac] [varchar](10) NULL,
	[Qty] [decimal](10, 2) NULL,
	[UoM] [varchar](20) NULL,
	[RatePerItem] [decimal](10, 2) NULL,
	[Discount] [decimal](10, 2) NULL,
	[DiscountPercent] [decimal](5, 2) NULL,
	[TaxableValue] [decimal](10, 2) NULL,
	[CGSTPercent] [decimal](5, 2) NULL,
	[CGSTValue] [decimal](10, 2) NULL,
	[SGSTPercent] [decimal](5, 2) NULL,
	[SGSTValue] [decimal](10, 2) NULL,
	[IGSTPercent] [decimal](5, 2) NULL,
	[IGSTValue] [decimal](10, 2) NULL,
	[CESSPercent] [decimal](5, 2) NULL,
	[CESSValue] [decimal](10, 2) NULL,
	[TotalValue] [decimal](16, 2) NULL,
	[Del_State] [bit] NULL,
	[Del_Date] [datetime] NULL,
	[CFItem1] [varchar](50) NULL,
	[CFItem2] [varchar](50) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TranQuotation]    Script Date: 15-Feb-2018 3:58:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TranQuotation](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[CompanyID] [int] NULL,
	[QuotationNumber] [varchar](20) NULL,
	[QuotationDate] [date] NULL,
	[QuotationDueDate] [date] NULL,
	[CustomerID] [int] NULL,
	[CustomerName] [varchar](200) NULL,
	[CustomerAddress] [varchar](500) NULL,
	[ContactPerson] [varchar](100) NULL,
	[CustomerMobile] [varchar](100) NULL,
	[CustomerPhone] [varchar](100) NULL,
	[CustomerEmail] [varchar](100) NULL,
	[CustomerState] [varchar](100) NULL,
	[PONumber] [varchar](50) NULL,
	[PODate] [date] NULL,
	[CGSTAmount] [decimal](10, 2) NULL,
	[SGSTAmount] [decimal](10, 2) NULL,
	[IGSTAmount] [decimal](10, 2) NULL,
	[CESSAmount] [decimal](10, 2) NULL,
	[TaxableAmount] [decimal](16, 2) NULL,
	[QuotationTotalAmount] [decimal](16, 2) NULL,
	[QuotationStatus] [varchar](50) NULL,
	[CustomerNotes] [varchar](1000) NULL,
	[TermsAndCondition] [varchar](1000) NULL,
	[isCessApplicable] [bit] NULL,
	[CF1] [varchar](100) NULL,
	[CF2] [varchar](100) NULL,
	[CF3] [varchar](100) NULL,
	[CF4] [varchar](100) NULL,
	[CF5] [varchar](100) NULL,
	[CreatedBy] [varchar](50) NULL,
	[CreatedOn] [datetime] NULL,
	[Del_State] [bit] NULL,
	[Del_Date] [datetime] NULL,
	[RoundedOffQuotationTotalAmount] [bigint] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TranQuotationItems]    Script Date: 15-Feb-2018 3:58:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TranQuotationItems](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[HeaderID] [bigint] NULL,
	[ItemID] [int] NULL,
	[ItemDescription] [varchar](200) NULL,
	[ItemCustomDescription] [varchar](200) NULL,
	[ItemType] [varchar](10) NULL,
	[HsnSac] [varchar](10) NULL,
	[Qty] [decimal](10, 2) NULL,
	[UoM] [varchar](20) NULL,
	[RatePerItem] [decimal](10, 2) NULL,
	[Discount] [decimal](10, 2) NULL,
	[DiscountPercent] [decimal](5, 2) NULL,
	[TaxableValue] [decimal](10, 2) NULL,
	[CGSTPercent] [decimal](5, 2) NULL,
	[CGSTValue] [decimal](10, 2) NULL,
	[SGSTPercent] [decimal](5, 2) NULL,
	[SGSTValue] [decimal](10, 2) NULL,
	[IGSTPercent] [decimal](5, 2) NULL,
	[IGSTValue] [decimal](10, 2) NULL,
	[CESSPercent] [decimal](5, 2) NULL,
	[CESSValue] [decimal](10, 2) NULL,
	[TotalValue] [decimal](16, 2) NULL,
	[Del_State] [bit] NULL,
	[Del_Date] [datetime] NULL,
	[CFItem1] [varchar](50) NULL,
	[CFItem2] [varchar](50) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TranReceiptNote]    Script Date: 15-Feb-2018 3:58:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TranReceiptNote](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[CompanyID] [int] NULL,
	[NoteNumber] [varchar](20) NULL,
	[NoteDate] [date] NULL,
	[CustomerID] [int] NULL,
	[ReferenceNumber] [varchar](50) NULL,
	[ReferenceDate] [date] NULL,
	[NoteType] [varchar](50) NULL,
	[CGSTAmount] [decimal](10, 2) NULL,
	[SGSTAmount] [decimal](10, 2) NULL,
	[IGSTAmount] [decimal](10, 2) NULL,
	[CESSAmount] [decimal](10, 2) NULL,
	[TaxableAmount] [decimal](16, 2) NULL,
	[TaxableAmountWithDisc] [decimal](16, 2) NULL,
	[CESSPercentTotal] [decimal](5, 2) NULL,
	[SGSTPercentTotal] [decimal](5, 2) NULL,
	[CGSTPercentTotal] [decimal](5, 2) NULL,
	[IGSTPercentTotal] [decimal](5, 2) NULL,
	[DiscountPercentTotal] [decimal](5, 2) NULL,
	[DiscountAmount] [decimal](10, 2) NULL,
	[NoteTotalAmount] [decimal](16, 2) NULL,
	[RoundedOffNoteTotalAmount] [bigint] NULL,
	[NoteStatus] [varchar](50) NULL,
	[ExtraNotes] [varchar](1000) NULL,
	[TermsAndCondition] [varchar](500) NULL,
	[isCessApplicable] [bit] NULL,
	[IsReverseCharge] [bit] NULL,
	[CF1] [varchar](100) NULL,
	[CF2] [varchar](100) NULL,
	[CF3] [varchar](100) NULL,
	[CF4] [varchar](100) NULL,
	[CF5] [varchar](100) NULL,
	[CF6] [varchar](250) NULL,
	[CreatedBy] [varchar](50) NULL,
	[CreatedOn] [datetime] NULL,
	[Del_State] [bit] NULL,
	[Del_Date] [datetime] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TranReceiptNoteItems]    Script Date: 15-Feb-2018 3:58:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TranReceiptNoteItems](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[HeaderID] [bigint] NULL,
	[ItemID] [int] NULL,
	[ItemDescription] [varchar](200) NULL,
	[ItemCustomDescription] [varchar](200) NULL,
	[ItemType] [varchar](10) NULL,
	[HsnSac] [varchar](10) NULL,
	[Qty] [decimal](10, 2) NULL,
	[UoM] [varchar](20) NULL,
	[RatePerItem] [decimal](10, 2) NULL,
	[DiscountPercent] [decimal](5, 2) NULL,
	[Discount] [decimal](10, 2) NULL,
	[TaxableValue] [decimal](10, 2) NULL,
	[CGSTPercent] [decimal](5, 2) NULL,
	[CGSTValue] [decimal](10, 2) NULL,
	[SGSTPercent] [decimal](5, 2) NULL,
	[SGSTValue] [decimal](10, 2) NULL,
	[IGSTPercent] [decimal](5, 2) NULL,
	[IGSTValue] [decimal](10, 2) NULL,
	[CESSPercent] [decimal](5, 2) NULL,
	[CESSValue] [decimal](10, 2) NULL,
	[TotalValue] [decimal](16, 2) NULL,
	[Del_State] [bit] NULL,
	[Del_Date] [datetime] NULL,
	[CFItem1] [varchar](50) NULL,
	[CFItem2] [varchar](50) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TranSalesInvoice]    Script Date: 15-Feb-2018 3:58:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TranSalesInvoice](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[CompanyID] [int] NULL,
	[InvoiceNumber] [varchar](20) NULL,
	[InvoiceDate] [date] NULL,
	[InvoiceDueDate] [date] NULL,
	[CustomerID] [int] NULL,
	[PONumber] [varchar](50) NULL,
	[PODate] [date] NULL,
	[CF5] [varchar](100) NULL,
	[CF2] [varchar](100) NULL,
	[CF4] [varchar](100) NULL,
	[CF1] [varchar](100) NULL,
	[CGSTAmount] [decimal](10, 2) NULL,
	[SGSTAmount] [decimal](10, 2) NULL,
	[IGSTAmount] [decimal](10, 2) NULL,
	[CESSAmount] [decimal](10, 2) NULL,
	[TaxableAmount] [decimal](16, 2) NULL,
	[InvoiceTotalAmount] [decimal](16, 2) NULL,
	[InvoiceStatus] [varchar](50) NULL,
	[CustomerNotes] [varchar](1000) NULL,
	[TermsAndCondition] [varchar](500) NULL,
	[Del_State] [bit] NULL,
	[Del_Date] [datetime] NULL,
	[CF3] [varchar](100) NULL,
	[CreatedBy] [varchar](50) NULL,
	[CreatedOn] [datetime] NULL,
	[isCessApplicable] [bit] NULL,
	[CustomerName] [varchar](200) NULL,
	[CustomerAddress] [varchar](500) NULL,
	[ContactPerson] [varchar](100) NULL,
	[CustomerMobile] [varchar](100) NULL,
	[CustomerPhone] [varchar](100) NULL,
	[CustomerEmail] [varchar](100) NULL,
	[CustomerState] [varchar](100) NULL,
	[CF6] [varchar](250) NULL,
	[CESSPercentTotal] [decimal](5, 2) NULL,
	[SGSTPercentTotal] [decimal](5, 2) NULL,
	[CGSTPercentTotal] [decimal](5, 2) NULL,
	[IGSTPercentTotal] [decimal](5, 2) NULL,
	[DiscountPercentTotal] [decimal](5, 2) NULL,
	[DiscountAmount] [decimal](10, 2) NULL,
	[TaxableAmountWithDisc] [decimal](10, 2) NULL,
	[IsReverseCharge] [bit] NULL,
	[Category] [varchar](10) NULL,
	[RoundedOffInvoiceTotalAmount] [bigint] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TranSalesInvoiceItems]    Script Date: 15-Feb-2018 3:58:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TranSalesInvoiceItems](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[SalesInvoiceHeaderID] [bigint] NULL,
	[ItemID] [int] NULL,
	[ItemDescription] [varchar](200) NULL,
	[ItemType] [varchar](10) NULL,
	[HsnSac] [varchar](10) NULL,
	[Qty] [decimal](10, 2) NULL,
	[UoM] [varchar](20) NULL,
	[RatePerItem] [decimal](10, 2) NULL,
	[Discount] [decimal](10, 2) NULL,
	[TaxableValue] [decimal](10, 2) NULL,
	[CGSTPercent] [decimal](5, 2) NULL,
	[CGSTValue] [decimal](10, 2) NULL,
	[SGSTPercent] [decimal](5, 2) NULL,
	[SGSTValue] [decimal](10, 2) NULL,
	[IGSTPercent] [decimal](5, 2) NULL,
	[IGSTValue] [decimal](10, 2) NULL,
	[CESSPercent] [decimal](5, 2) NULL,
	[CESSValue] [decimal](10, 2) NULL,
	[TotalValue] [decimal](16, 2) NULL,
	[Del_State] [bit] NULL,
	[Del_Date] [datetime] NULL,
	[DiscountPercent] [decimal](5, 2) NULL,
	[ItemCustomDescription] [varchar](200) NULL,
	[CFItem1] [varchar](50) NULL,
	[CFItem2] [varchar](50) NULL
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[MasterCompany] ADD  CONSTRAINT [DF_MasterCompany_Del_State]  DEFAULT ((0)) FOR [Del_State]
GO
ALTER TABLE [dbo].[MasterCustomer] ADD  CONSTRAINT [DF_MasterCustomer_Del_State]  DEFAULT ((0)) FOR [Del_State]
GO
ALTER TABLE [dbo].[MasterGroup] ADD  CONSTRAINT [DF_MasterGroup_Del_State]  DEFAULT ((0)) FOR [Del_State]
GO
ALTER TABLE [dbo].[MasterItem] ADD  CONSTRAINT [DF_MasterItem_Del_State]  DEFAULT ((0)) FOR [Del_State]
GO
ALTER TABLE [dbo].[SettingDocument] ADD  CONSTRAINT [DF_SettingDocument_DocumentIdResetFlag]  DEFAULT ((0)) FOR [DocumentIdResetFlag]
GO
ALTER TABLE [dbo].[TranDeliveryNote] ADD  CONSTRAINT [DF_TranDeliveryNote_Del_State]  DEFAULT ((0)) FOR [Del_State]
GO
ALTER TABLE [dbo].[TranDeliveryNoteItems] ADD  CONSTRAINT [DF_TranDeliveryNoteItems_Del_State]  DEFAULT ((0)) FOR [Del_State]
GO
ALTER TABLE [dbo].[TranPurchaseInvoice] ADD  CONSTRAINT [DF_TranPurchaseInvoice_Del_State]  DEFAULT ((0)) FOR [Del_State]
GO
ALTER TABLE [dbo].[TranPurchaseInvoiceItems] ADD  CONSTRAINT [DF_TranPurchaseInvoiceItems_Del_State]  DEFAULT ((0)) FOR [Del_State]
GO
ALTER TABLE [dbo].[TranQuotation] ADD  CONSTRAINT [DF_TranPurchaseQuotation_Del_State]  DEFAULT ((0)) FOR [Del_State]
GO
ALTER TABLE [dbo].[TranQuotationItems] ADD  CONSTRAINT [DF_TranQuotationItems_Del_State]  DEFAULT ((0)) FOR [Del_State]
GO
ALTER TABLE [dbo].[TranReceiptNote] ADD  CONSTRAINT [DF_TranReceiptNote_Del_State]  DEFAULT ((0)) FOR [Del_State]
GO
ALTER TABLE [dbo].[TranReceiptNoteItems] ADD  CONSTRAINT [DF_TranReceiptNoteItems_Del_State]  DEFAULT ((0)) FOR [Del_State]
GO
ALTER TABLE [dbo].[TranSalesInvoice] ADD  CONSTRAINT [DF_TranSalesInvoice_Del_State]  DEFAULT ((0)) FOR [Del_State]
GO
ALTER TABLE [dbo].[TranSalesInvoiceItems] ADD  CONSTRAINT [DF_TranSalesInvoiceItems_Del_State]  DEFAULT ((0)) FOR [Del_State]
GO
/****** Object:  StoredProcedure [dbo].[CheckAvailabilityInvoiceNo]    Script Date: 15-Feb-2018 3:58:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[CheckAvailabilityInvoiceNo]      
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
/****** Object:  StoredProcedure [dbo].[GetAutoSequenceNumber]    Script Date: 15-Feb-2018 3:58:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================    
-- Author:  TK    
-- Create date: 27-Aug-2017    
-- Description: To Get auto sequnce number for Sales Invoice.    
-- =============================================    
CREATE PROCEDURE [dbo].[GetAutoSequenceNumber]    
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
/****** Object:  StoredProcedure [dbo].[GetGSTR1Info]    Script Date: 15-Feb-2018 3:58:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================  
-- Author:  TK  
-- Create date: 15 Dec 2017  
-- Description: Transactions applicable for GSTR1  
-- =============================================  
CREATE PROCEDURE [dbo].[GetGSTR1Info]   
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
/****** Object:  StoredProcedure [dbo].[Master_Group_InsertUpdate]    Script Date: 15-Feb-2018 3:58:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		TK
-- Create date: 15/10/2017
-- Description:	Group insert update and delete (If ID = 0 then it inserts else updates)
-- =============================================
create PROCEDURE [dbo].[Master_Group_InsertUpdate]
	@ID int=0, 
	@GroupName varchar(200),
	@GroupDescription varchar(500),
	@GroupIDUnder int,
	@Location varchar(200)
AS
BEGIN
	SET NOCOUNT ON;
	If (@ID = 0)
	Begin
		INSERT INTO [dbo].[MasterGroup]
		([GroupName],[GroupDescription],[GroupIDUnder],[Location])
		VALUES
		(@GroupName, @GroupDescription, @GroupIDUnder, @Location)
	End
	Else
	Begin
		UPDATE [dbo].[MasterGroup]
		SET[GroupName] = @GroupName
		,[GroupDescription] = @GroupDescription
		,[GroupIDUnder] = @GroupIDUnder
		,[Location] = @Location
		WHERE ID = @ID
	End
END


GO
/****** Object:  StoredProcedure [dbo].[Master_Item_InsertUpdate]    Script Date: 15-Feb-2018 3:58:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================      
-- Author:  TK      
-- Create date: 15/10/2017      
-- Description: Item insert update and delete (If ID = 0 then it inserts else updates)      
-- =============================================      
CREATE PROCEDURE [dbo].[Master_Item_InsertUpdate]      
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
/****** Object:  StoredProcedure [dbo].[Trans_Invoice_Delete]    Script Date: 15-Feb-2018 3:58:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================  
-- Author:  TK  
-- Create date: 13/10/2017  
-- Description: Delete invoices with inventory updates  
-- =============================================  
CREATE PROCEDURE [dbo].[Trans_Invoice_Delete]  
 @InvoiceHeaderId int,   
 @CompanyID int,  
 @StockCompanyID int=0,  
 @mode varchar(50)  
AS  
BEGIN  
 SET NOCOUNT ON;  
 DECLARE @InvoiceItemID INT, @MasterItemID INT, @UpdateInStock decimal  
 --Delete (Del_State=1) invoie and its items along with inventory updates  
 if (@mode = 'SalesInvoice-Delete')  
 Begin  
  --Cursor to delete invoice items  
  
  DECLARE @DeleteSalesInvoice CURSOR  
  SET @DeleteSalesInvoice = CURSOR FOR  
  select ID, ItemID from [TranSalesInvoiceItems] where SalesInvoiceHeaderID=@InvoiceHeaderId  
   OPEN @DeleteSalesInvoice  
   FETCH NEXT  
   FROM @DeleteSalesInvoice INTO @InvoiceItemID, @MasterItemID  
   WHILE @@FETCH_STATUS = 0  
   BEGIN  
    Select @UpdateInStock=Qty from TranSalesInvoiceItems where ID=@InvoiceItemID  
  
    exec Trans_StockUpdate @MasterItemID,@StockCompanyID, @UpdateInStock    
  
    --update MasterItem set StockInHand = StockInHand + (Select Qty from TranSalesInvoiceItems where ID=@InvoiceItemID) where ID=@MasterItemID  
    Update [TranSalesInvoiceItems] set Del_State=1, Del_Date=GetDate() Where ID=@InvoiceItemID   
  
   FETCH NEXT  
   FROM @DeleteSalesInvoice INTO @InvoiceItemID, @MasterItemID  
   END  
   CLOSE @DeleteSalesInvoice  
  DEALLOCATE @DeleteSalesInvoice  
  --Delete Header  
        Update [TranSalesInvoice] set Del_State=1, Del_Date=GetDate() Where ID=@InvoiceHeaderId  
 End  
  --Delete invoice items while updating invoice header along with inventory updates  
 else if (@mode = 'SalesInvoice-Reset')  
 Begin  
  --Cursor to delete invoice items  
  DECLARE @ResetSalesInvoiceItems CURSOR  
  SET @ResetSalesInvoiceItems = CURSOR FOR  
  select ID, ItemID from [TranSalesInvoiceItems] where SalesInvoiceHeaderID=@InvoiceHeaderId  
   OPEN @ResetSalesInvoiceItems  
   FETCH NEXT  
   FROM @ResetSalesInvoiceItems INTO @InvoiceItemID, @MasterItemID  
   WHILE @@FETCH_STATUS = 0  
   BEGIN  
    Select @UpdateInStock=Qty from TranSalesInvoiceItems where ID=@InvoiceItemID  
  
    exec Trans_StockUpdate @MasterItemID,@StockCompanyID, @UpdateInStock    
  
    delete [TranSalesInvoiceItems] Where ID=@InvoiceItemID   
  
   FETCH NEXT  
   FROM @ResetSalesInvoiceItems INTO @InvoiceItemID, @MasterItemID  
   END  
   CLOSE @ResetSalesInvoiceItems  
  DEALLOCATE @ResetSalesInvoiceItems  
 End  
 if (@mode = 'PurchaseInvoice-Delete')  
 Begin  
  --Cursor to delete invoice items  
  
  DECLARE @DeletePurchaseInvoice CURSOR  
  SET @DeletePurchaseInvoice = CURSOR FOR  
  select ID, ItemID from [TranPurchaseInvoiceItems] where PurchaseInvoiceHeaderID=@InvoiceHeaderId  
   OPEN @DeletePurchaseInvoice  
   FETCH NEXT  
   FROM @DeletePurchaseInvoice INTO @InvoiceItemID, @MasterItemID  
   WHILE @@FETCH_STATUS = 0  
   BEGIN  
    Select @UpdateInStock=Qty from TranPurchaseInvoiceItems where ID=@InvoiceItemID  
    --Setting UpdateInStock to its negative counterpart so that it could reduce the stock at the time of deleting purchase invoice items  
    set @UpdateInStock=-@UpdateInStock  
    exec Trans_StockUpdate @MasterItemID,@StockCompanyID, @UpdateInStock    
  
    Update [TranPurchaseInvoiceItems] set Del_State=1, Del_Date=GetDate() Where ID=@InvoiceItemID   
  
   FETCH NEXT  
   FROM @DeletePurchaseInvoice INTO @InvoiceItemID, @MasterItemID  
   END  
   CLOSE @DeletePurchaseInvoice  
  DEALLOCATE @DeletePurchaseInvoice  
  --Delete Header  
        Update [TranPurchaseInvoice] set Del_State=1, Del_Date=GetDate() Where ID=@InvoiceHeaderId  
 End  
  --Delete invoice items while updating invoice header along with inventory updates  
 else if (@mode = 'PurchaseInvoice-Reset')  
 Begin  
  --Cursor to delete invoice items  
  DECLARE @ResetPurchaseInvoiceItems CURSOR  
  SET @ResetPurchaseInvoiceItems = CURSOR FOR  
  select ID, ItemID from [TranPurchaseInvoiceItems] where PurchaseInvoiceHeaderID=@InvoiceHeaderId  
   OPEN @ResetPurchaseInvoiceItems  
   FETCH NEXT  
   FROM @ResetPurchaseInvoiceItems INTO @InvoiceItemID, @MasterItemID  
   WHILE @@FETCH_STATUS = 0  
   BEGIN  
    Select @UpdateInStock=Qty from TranPurchaseInvoiceItems where ID=@InvoiceItemID  
    --Setting UpdateInStock to its negative counterpart so that it could reduce the stock at the time of deleting purchase invoice items  
    set @UpdateInStock=-@UpdateInStock  
    exec Trans_StockUpdate @MasterItemID,@StockCompanyID, @UpdateInStock    
  
    delete [TranPurchaseInvoiceItems] Where ID=@InvoiceItemID   
  
   FETCH NEXT  
   FROM @ResetPurchaseInvoiceItems INTO @InvoiceItemID, @MasterItemID  
   END  
   CLOSE @ResetPurchaseInvoiceItems  
  DEALLOCATE @ResetPurchaseInvoiceItems  
 End  
 else if (@mode = 'DeliveryNote-Delete')  
 Begin  
  --Cursor to delete invoice items  
  
  DECLARE @DeleteDeliveryNote CURSOR  
  SET @DeleteDeliveryNote = CURSOR FOR  
  select ID, ItemID from [TranDeliveryNoteItems] where HeaderID=@InvoiceHeaderId  
   OPEN @DeleteDeliveryNote  
   FETCH NEXT  
   FROM @DeleteDeliveryNote INTO @InvoiceItemID, @MasterItemID  
   WHILE @@FETCH_STATUS = 0  
   BEGIN  
    Select @UpdateInStock=Qty from TranDeliveryNoteItems where ID=@InvoiceItemID  
  
    exec Trans_StockUpdate @MasterItemID,@StockCompanyID, @UpdateInStock    
  
    --update MasterItem set StockInHand = StockInHand + (Select Qty from TranDeliveryNoteItems where ID=@InvoiceItemID) where ID=@MasterItemID  
    Update [TranDeliveryNoteItems] set Del_State=1, Del_Date=GetDate() Where ID=@InvoiceItemID   
  
   FETCH NEXT  
   FROM @DeleteDeliveryNote INTO @InvoiceItemID, @MasterItemID  
   END  
   CLOSE @DeleteDeliveryNote  
  DEALLOCATE @DeleteDeliveryNote  
  --Delete Header  
        Update [TranDeliveryNote] set Del_State=1, Del_Date=GetDate() Where ID=@InvoiceHeaderId  
 End  
  --Delete invoice items while updating invoice header along with inventory updates  
 else if (@mode = 'DeliveryNote-Reset')  
 Begin  
  --Cursor to delete invoice items  
  DECLARE @ResetDeliveryNoteItems CURSOR  
  SET @ResetDeliveryNoteItems = CURSOR FOR  
  select ID, ItemID from [TranDeliveryNoteItems] where HeaderID=@InvoiceHeaderId  
   OPEN @ResetDeliveryNoteItems  
   FETCH NEXT  
   FROM @ResetDeliveryNoteItems INTO @InvoiceItemID, @MasterItemID  
   WHILE @@FETCH_STATUS = 0  
   BEGIN  
    Select @UpdateInStock=Qty from TranDeliveryNoteItems where ID=@InvoiceItemID  
  
    exec Trans_StockUpdate @MasterItemID,@StockCompanyID, @UpdateInStock    
  
    delete [TranDeliveryNoteItems] Where ID=@InvoiceItemID   
  
   FETCH NEXT  
   FROM @ResetDeliveryNoteItems INTO @InvoiceItemID, @MasterItemID  
   END  
   CLOSE @ResetDeliveryNoteItems  
  DEALLOCATE @ResetDeliveryNoteItems  
 End  
 if (@mode = 'ReceiptNote-Delete')  
 Begin  
  --Cursor to delete invoice items  
  
  DECLARE @DeleteReceiptNote CURSOR  
  SET @DeleteReceiptNote = CURSOR FOR  
  select ID, ItemID from [TranReceiptNoteItems] where HeaderID=@InvoiceHeaderId  
   OPEN @DeleteReceiptNote  
   FETCH NEXT  
   FROM @DeleteReceiptNote INTO @InvoiceItemID, @MasterItemID  
   WHILE @@FETCH_STATUS = 0  
   BEGIN  
    Select @UpdateInStock=Qty from TranReceiptNoteItems where ID=@InvoiceItemID  
    --Setting UpdateInStock to its negative counterpart so that it could reduce the stock at the time of deleting purchase invoice items  
    set @UpdateInStock=-@UpdateInStock  
    exec Trans_StockUpdate @MasterItemID,@StockCompanyID, @UpdateInStock    
  
    Update [TranReceiptNoteItems] set Del_State=1, Del_Date=GetDate() Where ID=@InvoiceItemID   
  
   FETCH NEXT  
   FROM @DeleteReceiptNote INTO @InvoiceItemID, @MasterItemID  
   END  
   CLOSE @DeleteReceiptNote  
  DEALLOCATE @DeleteReceiptNote  
  --Delete Header  
        Update [TranReceiptNote] set Del_State=1, Del_Date=GetDate() Where ID=@InvoiceHeaderId  
 End  
  --Delete invoice items while updating invoice header along with inventory updates  
 else if (@mode = 'ReceiptNote-Reset')  
 Begin  
  --Cursor to delete invoice items  
  DECLARE @ResetReceiptNoteItems CURSOR  
  SET @ResetReceiptNoteItems = CURSOR FOR  
  select ID, ItemID from [TranReceiptNoteItems] where HeaderID=@InvoiceHeaderId  
   OPEN @ResetReceiptNoteItems  
   FETCH NEXT  
   FROM @ResetReceiptNoteItems INTO @InvoiceItemID, @MasterItemID  
   WHILE @@FETCH_STATUS = 0  
   BEGIN  
    Select @UpdateInStock=Qty from TranReceiptNoteItems where ID=@InvoiceItemID  
    --Setting UpdateInStock to its negative counterpart so that it could reduce the stock at the time of deleting purchase invoice items  
    set @UpdateInStock=-@UpdateInStock  
    exec Trans_StockUpdate @MasterItemID,@StockCompanyID, @UpdateInStock    
  
    delete [TranReceiptNoteItems] Where ID=@InvoiceItemID   
  
   FETCH NEXT  
   FROM @ResetReceiptNoteItems INTO @InvoiceItemID, @MasterItemID  
   END  
   CLOSE @ResetReceiptNoteItems  
  DEALLOCATE @ResetReceiptNoteItems  
 End  
END  


GO
/****** Object:  StoredProcedure [dbo].[Trans_InvoiceItem_Save]    Script Date: 15-Feb-2018 3:58:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Trans_InvoiceItem_Save]        
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
/****** Object:  StoredProcedure [dbo].[Trans_MasterStockList]    Script Date: 15-Feb-2018 3:58:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--Trans_MasterStockList 0,1,0,'',0,'GetStockList'  
-- =============================================  
-- Author:  TK  
-- Create date: 14/10/2017  
-- Description: Deals with table MasterStock  
CREATE PROCEDURE [dbo].[Trans_MasterStockList]  
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
/****** Object:  StoredProcedure [dbo].[Trans_StockUpdate]    Script Date: 15-Feb-2018 3:58:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--Trans_StockManagement 1,-4
--select * from MasterItem
-- =============================================
-- Author:		TK
-- Create date: 13/10/2017
-- Description:	Stock managment
-- =============================================

CREATE PROCEDURE [dbo].[Trans_StockUpdate] 
	-- Add the parameters for the stored procedure here
	@ItemId int, 
	@CompanyID int,
	@UpdateInStock decimal = 0
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE [MasterStockList]
		SET [StockInHand] = StockInHand+@UpdateInStock
		where ItemID=@ItemId and CompanyID=@CompanyID

	if (@@ROWCOUNT=0)
	BEGIN
		INSERT INTO [MasterStockList]
           ([CompanyID]
           ,[ItemID]
           ,[StockInHand])
		VALUES
           (@CompanyID
		   ,@ItemID
		   ,@UpdateInStock)
	END
END


GO
USE [master]
GO
ALTER DATABASE [THAgstBilling] SET  READ_WRITE 
GO
----------------------------------------------------------------------------------------------------------------------------------------------------------------------
USE [THAgstBilling]
GO
INSERT INTO [dbo].[SettingUser]
           ([UserName]
           ,[Password]
           ,[PasswordHint]
           ,[Role]
           ,[Privilege]
           ,[IsUserActive])
     VALUES
           ('Admin','YWRtaW4=','','Admin','Administration','1')
GO