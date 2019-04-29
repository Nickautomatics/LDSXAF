using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.Persistent.Base;
namespace EDS.RentalManager.Express.Module.BusinessObjects
{
    [DefaultClassOptions]
    public partial class ActivityLog
    {
        public ActivityLog(Session session) : base(session) { }
        public ActivityLog(Session session, string activity) : base(session) {
            ActivityDescription = activity;
        }

        public override void AfterConstruction() { base.AfterConstruction(); 
        
        }
    }

}
