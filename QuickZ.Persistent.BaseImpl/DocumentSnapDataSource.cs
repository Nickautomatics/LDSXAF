// You can find sample updates and versions for different programming languages here:
// http://www.devexpress.com/example=T164798

using DevExpress.Xpo;

using System;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using DevExpress.ExpressApp.Utils;
using DevExpress.ExpressApp.Editors;
using DevExpress.Persistent.Validation;
using QuickZ.Persistent.Base.Documents;
using EDS.RentalManager.Express.Module.BusinessObjects.Common;

namespace QuickZ.Persistent.BaseImpl
{
    [NavigationItem(false), CreatableItem(false)]
    [MapInheritance(MapInheritanceType.ParentTable)]
    [DevExpress.ExpressApp.DC.XafDefaultProperty(nameof(Name))]
    public class DocumentSnapDataSource : BusinessDocumentDataSource, IDocumentSnapDataSource, ILocationPrivacyObject
    {
        RMXLocationCategory ILocationPrivacyObject.Location => this.HomeLocation;

        public DocumentSnapDataSource(Session session)
            : base(session)
        {
        }

        private DocumentSnap _DocumentSnap;
        [Association("DocumentSnap-DataSources")]
        public DocumentSnap DocumentSnap
        {
            get { return _DocumentSnap; }
            set { SetPropertyValue(nameof(DocumentSnap), ref _DocumentSnap, value); }
        }
    }
}
