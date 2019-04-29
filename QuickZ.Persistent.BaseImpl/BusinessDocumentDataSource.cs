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
    [Persistent("BusinessDocumentDataSource")]
    [DevExpress.ExpressApp.DC.XafDefaultProperty(nameof(Name))]
    public abstract class BusinessDocumentDataSource : RMXQuickAuditBaseObject, IDocumentDataSource//, ILocationPrivacyObject
    {
        //RMXLocationCategory ILocationPrivacyObject.Location => (this.HomeLocation);

        public BusinessDocumentDataSource(Session session)
            : base(session)
        {
        }
        private string _Name;
        public string Name
        {
            get { return _Name; }
            set { SetPropertyValue(nameof(Name), ref _Name, value); }
        }
        private Type _DataType;
        [TypeConverter(typeof(LocalizedClassInfoTypeConverter))]
        [ValueConverter(typeof(TypeToStringConverter))]
        [RuleRequiredField("RuleRequiredField for BusinessDocumentDataSource.DataType", DefaultContexts.Save, "A DataType must be specified", ResultType = ValidationResultType.Error)]
        public Type DataType
        {
            get { return _DataType; }
            set { SetPropertyValue(nameof(DataType), ref _DataType, value); }
        }
        private string _Criteria;
        [Delayed(true)]
        [CriteriaOptions("DataType"), Size(SizeAttribute.Unlimited)]
        public string Criteria
        {
            get { return _Criteria; }
            set { SetPropertyValue(nameof(Criteria), ref _Criteria, value); }
        }

    }
}
