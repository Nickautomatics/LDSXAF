using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.Persistent.Base;
using DevExpress.XtraReports.UI;
using System.Drawing;
using QuickZ.Persistent.Base.Documents;
namespace QuickZ.Persistent.BaseImpl
{   
    
    public partial class BusinessDocumentReport
    {
        public BusinessDocumentReport(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            this.DocumentType = DocumentContentType.XtraReport;
        }


    }

}
