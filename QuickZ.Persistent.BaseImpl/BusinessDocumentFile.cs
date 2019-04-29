using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.Persistent.Base;
using QuickZ.Persistent.Base.Documents;

namespace QuickZ.Persistent.BaseImpl
{
    public partial class BusinessDocumentFile

    {
        public BusinessDocumentFile(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();            
        }

        protected override void OnSaving()
        {
            base.OnSaving();

            if (((IBusinessDocumentFile)this).Content == null)
            {
                DocumentName = "(empty file)";
            }
            else
            {
                if (String.IsNullOrEmpty(DocumentName))
                    DocumentName = ((IBusinessDocumentFile)this).FileName;
            }
        }
    }

}
