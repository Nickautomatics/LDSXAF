using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using EDS.RentalManager.Express.Module.BusinessObjects;

using DevExpress.ExpressApp;
using QuickZ.Persistent.Base.Core;

namespace EDS.RentalManager.Express.Module.BusinessObjects.Common
{


    // DevEx coed. See https://www.devexpress.com/Support/Center/Example/Details/E2829
    [NonPersistent]
    // [ImageName("BOSequentialNumberObject")]
    [DevExpress.ExpressApp.DC.XafDefaultProperty(nameof(FriendlyId))]
    public abstract class RMXSequenceBaseObject : RMXQuickAuditBaseObject, IRMXSequenceObject
    {
        public RMXSequenceBaseObject(Session session)
            : base(session)
        {
        }
        public RMXSequenceBaseObject(Session session, Guid oidOverride)
            : base(session, oidOverride)
        {

        }

        #region Variables and Constants

        #endregion

        #region XPO Overrides
        public override void AfterConstruction()
        {
            base.AfterConstruction();

            // if (!(Session is NestedUnitOfWork) && Session.IsNewObject(this))
            if (Session.IsNewObject(this))
                    FriendlyId = GetSequenceId();
        }
        #endregion


        #region Methods

        public virtual string GetSequenceId()
        {
            var prefix = "";
            if (SequenceId != null)
                return prefix + String.Format("{0:D6}", SequenceId);

            SequenceId = DevExpress.Persistent.BaseImpl
                        .DistributedIdGeneratorHelper
                        .Generate(this.Session.DataLayer, this.GetType().FullName, string.Empty);
            return prefix + String.Format("{0:D6}", SequenceId);
        }

        #endregion

        [Browsable(false)]
        [NonCloneable]
        public abstract string FriendlyId { get; set; }

        [Browsable(false)]
        [NonCloneable]
        public abstract long? SequenceId { get; set; }



    }
}