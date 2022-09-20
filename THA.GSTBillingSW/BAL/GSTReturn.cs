using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace THA.GSTBillingSW.BAL
{
    class GSTReturn
    {
        DAL.GSTReturn gstReturn = new DAL.GSTReturn();

        public DataSet BindDataSetGSTR1(DateTime FromDate, DateTime ToDate, Int16 CompanyId, string Mode)
        {
            return gstReturn.BindDataSetGSTR1(FromDate, ToDate, CompanyId, Mode);
        }
        //public void CreateGSTReturnOutputFile(string GSTRType, string TargetFileNameWithPath, DateTime FromDate, DateTime ToDate, Int16 CompanyId)
        //{
        //    //DirectoryInfo outputDir = new DirectoryInfo(ExportFileLocation);
        //    //if (!outputDir.Exists) throw new Exception("Output Directory doesn't exists");
        //    // This is the Second Release of the Article:
        //    // http://www.codeproject.com/Articles/680421/Create-Read-Edit-Advance-Excel-2007-2010-Report-in

        //    // template file
        //    FileInfo templateFile = new FileInfo(@Application.StartupPath + @"\Export\ExcelTemplates\GSTR1_Excel_Workbook_Template-V1.3.xlsx");
        //    // Making a new file based on template
        //    FileInfo newFile = new FileInfo(TargetFileNameWithPath);

        //    // If there is any file having same name as newFile, then delete it first
        //    if (newFile.Exists)
        //    {
        //        newFile.Delete();
        //        newFile = new FileInfo(TargetFileNameWithPath);
        //    }
        //    using (DataSet ds = gstReturn.BindDataSetGSTR1(FromDate, ToDate, CompanyId))
        //    {

        //        using (ExcelPackage package = new ExcelPackage(newFile, templateFile))
        //        {
        //            // Openning b2b Worksheet of the template file
        //            ExcelWorksheet worksheet = package.Workbook.Worksheets["b2b"];
        //            //worksheet.InsertRow(5, 2);

        //            int rowID = 0;
        //            foreach (DataRow dr in ds.Tables[0].Rows)
        //            {
        //                int columnId = rowID + 5;
        //                worksheet.Cells["A" + columnId].Value = Convert.ToString(dr["CustomerGSTN"]);
        //                worksheet.Cells["B" + columnId].Value = Convert.ToString(dr["InvoiceNumber"]);
        //                worksheet.Cells["C" + columnId].Value = Convert.ToDateTime(dr["InvoiceDate"]);
        //                worksheet.Cells["D" + columnId].Value = Convert.ToDecimal(dr["InvoiceTotalAmount"]);
        //                worksheet.Cells["E" + columnId].Value = Convert.ToString(dr["CustomerState"]);
        //                worksheet.Cells["F" + columnId].Value = "N";
        //                worksheet.Cells["G" + columnId].Value = "Regular";
        //                worksheet.Cells["H" + columnId].Value = "";
        //                worksheet.Cells["I" + columnId].Value = Convert.ToDecimal(dr["TaxRate"]);
        //                worksheet.Cells["J" + columnId].Value = Convert.ToDecimal(dr["TaxableAmount"]);
        //                worksheet.Cells["K" + columnId].Value = Convert.ToDecimal(dr["CESSAmount"]);
        //                rowID++;
        //            }
        //            worksheet = package.Workbook.Worksheets["b2cl"];

        //            rowID = 0;
        //            foreach (DataRow dr in ds.Tables[1].Rows)
        //            {
        //                int columnId = rowID + 5;
        //                worksheet.Cells["A" + columnId].Value = Convert.ToString(dr["InvoiceNumber"]);
        //                worksheet.Cells["B" + columnId].Value = Convert.ToDateTime(dr["InvoiceDate"]);
        //                worksheet.Cells["C" + columnId].Value = Convert.ToDecimal(dr["InvoiceTotalAmount"]);
        //                worksheet.Cells["D" + columnId].Value = Convert.ToString(dr["CustomerState"]);
        //                worksheet.Cells["E" + columnId].Value = Convert.ToDecimal(dr["TaxRate"]);
        //                worksheet.Cells["F" + columnId].Value = Convert.ToDecimal(dr["TaxableAmount"]);
        //                worksheet.Cells["G" + columnId].Value = Convert.ToDecimal(dr["CESSAmount"]);
        //                worksheet.Cells["H" + columnId].Value = "";
        //                rowID++;
        //            }

        //            worksheet = package.Workbook.Worksheets["b2cs"];

        //            rowID = 0;
        //            foreach (DataRow dr in ds.Tables[2].Rows)
        //            {
        //                int columnId = rowID + 5;
        //                worksheet.Cells["A" + columnId].Value = "OE";
        //                worksheet.Cells["B" + columnId].Value = Convert.ToString(dr["CustomerState"]);
        //                worksheet.Cells["C" + columnId].Value = Convert.ToDecimal(dr["TaxRate"]);
        //                worksheet.Cells["D" + columnId].Value = Convert.ToDecimal(dr["TaxableAmount"]);
        //                worksheet.Cells["E" + columnId].Value = Convert.ToDecimal(dr["CESSAmount"]);
        //                worksheet.Cells["F" + columnId].Value = "";
        //                rowID++;
        //            }

        //            worksheet = package.Workbook.Worksheets["hsn"];

        //            rowID = 0;
        //            foreach (DataRow dr in ds.Tables[3].Rows)
        //            {
        //                int columnId = rowID + 5;
        //                worksheet.Cells["A" + columnId].Value = Convert.ToString(dr["HsnSac"]);
        //                worksheet.Cells["B" + columnId].Value = Convert.ToString(dr["ItemDescription"]);
        //                worksheet.Cells["C" + columnId].Value = Convert.ToString(dr["UoM"]);
        //                worksheet.Cells["D" + columnId].Value = Convert.ToDecimal(dr["TotalQty"]);
        //                worksheet.Cells["E" + columnId].Value = Convert.ToDecimal(dr["TaxableAmount"]);
        //                worksheet.Cells["F" + columnId].Value = Convert.ToDecimal(dr["InvoiceTotalAmount"]);
        //                worksheet.Cells["G" + columnId].Value = Convert.ToDecimal(dr["TotalIGSTAmount"]);
        //                worksheet.Cells["H" + columnId].Value = Convert.ToDecimal(dr["TotalCGSTAmount"]);
        //                worksheet.Cells["I" + columnId].Value = Convert.ToDecimal(dr["TotalSGSTAmount"]);
        //                worksheet.Cells["J" + columnId].Value = Convert.ToDecimal(dr["CESSAmount"]);
        //                rowID++;
        //            }
        //            //// Adding formula for the 'E' column i.e. 'Value' column
        //            //// Now see how you write R1C1 formula
        //            //// About FORMULA R1C1: http://msdn.microsoft.com/en-us/library/office/bb213527%28v=office.12%29.aspx
        //            //worksheet.Cells["E2:E6"].FormulaR1C1 = "RC[-2]*RC[-1]";

        //            //// Now adding SUBTOTAL() function as was in Sample 1
        //            //// But here you'll see how to add 'Named Range'
        //            //var name = worksheet.Names.Add("SubTotalName", worksheet.Cells["C7:E7"]);
        //            //name.Formula = "SUBTOTAL(9, C2:C6)";

        //            //// Formatting newly added rows i.e. Row 5th and 6th
        //            //worksheet.Cells["C5:C6"].Style.Numberformat.Format = "#,##0";
        //            //worksheet.Cells["D5:E6"].Style.Numberformat.Format = "#,##0.00";

        //            //// Now we are going to create the Pie Chart
        //            //// Read about Pie Chart: http://office.microsoft.com/en-in/excel-help/present-your-data-in-a-pie-chart-HA010211848.aspx
        //            //var chart = (worksheet.Drawings.AddChart("PieChart", OfficeOpenXml.Drawing.Chart.eChartType.Pie3D) as ExcelPieChart);

        //            //// Setting title text of the Chart
        //            //chart.Title.Text = "Total";

        //            //// Setting 5 pixel offset from 5th column of the first row
        //            //chart.SetPosition(0, 0, 5, 5);

        //            //// Setting width & height of the chart area
        //            //chart.SetSize(600, 300);

        //            ////In the Pie Chart value will come from 'Value' column
        //            ////and show in the form of percentage
        //            ////Now I'll show you how to take values from the 'Value' column
        //            //ExcelAddress valueAddress = new ExcelAddress(2, 5, 6, 5);
        //            //// Setting Series and XSeries of the chart
        //            //// Product name will be the item of the Chart
        //            //var ser = (chart.Series.Add(valueAddress.Address, "B2:B6") as ExcelPieChartSerie);

        //            //// Setting chart properties
        //            //chart.DataLabel.ShowCategory = true;
        //            //chart.DataLabel.ShowPercent = true;
        //            //// Formatting Looks of the Chart
        //            //chart.Legend.Border.LineStyle = eLineStyle.Solid;
        //            //chart.Legend.Border.Fill.Style = eFillStyle.SolidFill;
        //            //chart.Legend.Border.Fill.Color = Color.DarkBlue;

        //            // Switch the page layout view back to the normal
        //            worksheet.View.PageLayoutView = false;

        //            // Save our new workbook, we are done
        //            package.Save();

        //            //  Step 3:  Let's open our new Excel file and shut down this application.
        //            Process p = new Process();
        //            p.StartInfo = new ProcessStartInfo(TargetFileNameWithPath);
        //            p.Start();
        //        }
        //    }
        //}
    }
}
