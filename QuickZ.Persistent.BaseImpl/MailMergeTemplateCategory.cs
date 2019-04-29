using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.Persistent.Base;

namespace QuickZ.Persistent.BaseImpl.Mailings
{
    public partial class MailMergeTemplateCategory
    {
        public  MailMergeTemplateCategory(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
