using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using THA.GSTBillingSW.Report.CustomDataSet;

namespace THA.GSTBillingSW.BAL
{
    class ReportDeliveryNote
    {
        DAL.ReportDeliveryNote reportDeliveryNote = new DAL.ReportDeliveryNote();
        public NoteDataSet BindDataSetDeliveryNote(Int32 DeliveryNoteID, Int16 CompanyId)
        {
            return reportDeliveryNote.BindDataSetDeliveryNote(DeliveryNoteID, CompanyId);
        }

        public NoteDataSet BindDataSetDeliveryNoteDetailed(DateTime FromDate, DateTime ToDate, Int16 CompanyId, string FilterValue)
        {
            return reportDeliveryNote.BindDataSetDeliveryNoteDetailed(FromDate, ToDate, CompanyId, FilterValue);
        }

        public NoteDataSet BindDataSetDeliveryNoteConsolidated(DateTime FromDate, DateTime ToDate, Int16 CompanyId, string FilterValue)
        {
            return reportDeliveryNote.BindDataSetDeliveryNoteConsolidated(FromDate, ToDate, CompanyId, FilterValue);
        }

    }
}
