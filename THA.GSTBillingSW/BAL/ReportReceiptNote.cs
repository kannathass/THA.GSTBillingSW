using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using THA.GSTBillingSW.Report.CustomDataSet;

namespace THA.GSTBillingSW.BAL
{
    class ReportReceiptNote
    {
        DAL.ReportReceiptNote reportReceiptNote = new DAL.ReportReceiptNote();
        public NoteDataSet BindDataSetReceiptNote(Int32 ReceiptNoteID, Int16 CompanyId)
        {
            return reportReceiptNote.BindDataSetReceiptNote(ReceiptNoteID, CompanyId);
        }

        public NoteDataSet BindDataSetReceiptNoteDetailed(DateTime FromDate, DateTime ToDate, Int16 CompanyId, string FilterValue)
        {
            return reportReceiptNote.BindDataSetReceiptNoteDetailed(FromDate, ToDate, CompanyId, FilterValue);
        }

        public NoteDataSet BindDataSetReceiptNoteConsolidated(DateTime FromDate, DateTime ToDate, Int16 CompanyId, string FilterValue)
        {
            return reportReceiptNote.BindDataSetReceiptNoteConsolidated(FromDate, ToDate, CompanyId, FilterValue);
        }

        public InvardOutwardWeightDetail BindDataSetReceiptNoteWeightInfo(DateTime FromDate, DateTime ToDate, Int16 CompanyId, string FilterValue)
        {
            return reportReceiptNote.BindDataSetReceiptNoteWeightInfo(FromDate, ToDate, CompanyId, FilterValue);
        }
    }
}
