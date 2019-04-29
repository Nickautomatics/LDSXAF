using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Base.General;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDS.RentalManager.Express.Module.BusinessObjects.Common.Base
{
    [DevExpress.ExpressApp.DC.XafDefaultProperty(nameof(Title))]
    [NonPersistent]
    public abstract class RMXSequenceDocumentBase : RMXSequenceBaseObject//, IResource
    {
        public RMXSequenceDocumentBase(Session session) : base(session)
        {

        }
        public RMXSequenceDocumentBase(Session session, Guid oidOverride)
            : base(session, oidOverride)
        {

        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // GetSequenceId();
        }

        private string _Title;
        [XafDisplayName("Document Title")]
        public string Title {
            get => _Title;
            set => SetPropertyValue(nameof(Title), ref _Title, value);
        }

        #region IWITSSequencedObject Members
        [RuleUniqueValue]
        [RuleRequiredField]
        [DevExpress.ExpressApp.DC.XafDisplayName("Record ID")]
        public override string FriendlyId {
            get => GetPropertyValue<string>(nameof(FriendlyId));
            set => SetPropertyValue<string>(nameof(FriendlyId), value);
        }

        [RuleUniqueValue]
        [RuleRequiredField]
        [Browsable(false)]
        public override long? SequenceId {
            get => GetPropertyValue<long?>(nameof(SequenceId));
            set => SetPropertyValue<long?>(nameof(SequenceId), value);
        }

        public override string GetSequenceId() {
            var prefix = "";
            if (SequenceId != null)
                return prefix + String.Format("{0:D6}", SequenceId);

            SequenceId = DevExpress.Persistent.BaseImpl
                        .DistributedIdGeneratorHelper
                        .Generate(this.Session.DataLayer, this.GetType().FullName, string.Empty) + 1;
            return prefix + String.Format("{0:D6}", SequenceId);
        }

        #endregion

        //#region Resource Properties

        //private int _Color;

        //[VisibleInDetailView(false), VisibleInListView(false)]
        //[ImmediatePostData]
        //public int Color { get { return _Color; } set { SetPropertyValue("Color", ref _Color, value); } }

        //[Browsable(false), NonPersistent]
        //public string Caption { get { return Title; } set { Title = value; } }

        //[Browsable(false)]
        //public object Id 
        //{ get { return Oid; }
        //}

        //[Browsable(false)]
        //public int OleColor 
        //{ get { return System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(Color)); }
        //}
        //#endregion


    }
}
