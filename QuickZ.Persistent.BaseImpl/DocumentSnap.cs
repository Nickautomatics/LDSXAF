using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.Persistent.Base;
using QuickZ.Persistent.Base.Documents;

namespace QuickZ.Persistent.BaseImpl
{
    public partial class DocumentSnap
    {
        public DocumentSnap(Session session)
            : base(session)
        {

        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            this.DocumentType = DocumentContentType.SnapReport;
        }
    }

}
