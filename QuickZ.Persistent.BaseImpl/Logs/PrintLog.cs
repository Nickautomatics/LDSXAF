using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using QuickZ.Persistent.Base.Core;
using QuickZ.Persistent.BaseImpl;
using DevExpress.ExpressApp.DC;

namespace EDS.RentalManager.Express.Module.BusinessObjects
{
    // [DefaultClassOptions]
    public interface IPrintLog
    {
        void AfterConstruction();
        string PrintedOn { get; }
        bool IsPrintingFailed { get; set; }
    }
    [NonPersistent]
    [DevExpress.ExpressApp.DC.XafDefaultProperty(nameof(PrintedOn))]
    [ImageName("BOPrintLog")]
    public abstract class PrintLog : SavedDocument, ISavedDocument, IPrintLog
    {
        public PrintLog(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction();

            IsPrintingFailed = true;
        }

        public string PrintedOn => (CreatedOn == null) ? "** missing file **" : CreatedOn.Value.ToString("MM/dd/yyyy hh:mm:ss");

        //public string PrintedOn
        //{
        //    get {
        //        if (CreatedAt == null)
        //            return "** missing file **";
        //        return CreatedAt.ToString("MM/dd/yyyy hh:mm:ss"); }
        //}


        // Fields...
        private bool _IsSendingFailed;
        public bool IsPrintingFailed
        {
            get { return _IsSendingFailed; }
            set { SetPropertyValue("IsSendingFailed", ref _IsSendingFailed, value); }
        }
    }

}
