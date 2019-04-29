using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using QuickZ.Persistent.Base.Core;
using QuickZ.Persistent.Base.Documents;
using QuickZ.Persistent.BaseImpl;
using EDS.RentalManager.Express.Module.BusinessObjects.Common;
using DevExpress.ExpressApp.DC;

namespace QuickZ.Persistent.Base.Mails
{
    public interface IDocumentRequirementType
    {
        string RequirementName { get; set; }
        // XPCollection<BusinessDocumentData> BusinessDocuments { get; }
    }

    /// <summary>
    /// This is a flag and description that a particular document is a required attachment to a Business Object
    /// <remarks>Will used as a basis for checking completion of required documents</remarks>
    /// </summary>
    [NavigationItem(false), CreatableItem(false)]
    [DevExpress.ExpressApp.DC.XafDefaultProperty(nameof(RequirementName))]
    [ImageName("BODocumentRequirementType")]
    [DevExpress.ExpressApp.DC.XafDisplayName("Requirement Type")]
    [Persistent("BusinessDocumentRequirementType")]
    public class DocumentRequirementType : RMXQuickAuditBaseObject, IDocumentRequirementType, ILocationPrivacyObject
    {
        RMXLocationCategory ILocationPrivacyObject.Location => (this.HomeLocation);

        private string _RequirementName;
        public DocumentRequirementType(Session session)
            : base(session)
        {

        }

        public DocumentRequirementType(Session session, Guid oidOverride) : base(session, oidOverride)
        {

        }

        [VisibleInDetailView(true)]
        [Size(255)]
        [ImmediatePostData]
        [RuleRequiredField("RuleRequiredField for DocumentRequirementType.RequirementName", DefaultContexts.Save, "A RequirementName must be specified", ResultType = ValidationResultType.Error)]
        public string RequirementName
        {
            get
            {
                return _RequirementName;
            }
            set
            {
                SetPropertyValue("RequirementName", ref _RequirementName, value);
            }
        }

        private Type _DataType;
        [TypeConverter(typeof(LocalizedClassInfoTypeConverter))]
        [ValueConverter(typeof(TypeToStringConverter))]
        [RuleRequiredField("RuleRequiredField for DocumentRequirementType.TargetObjectType", DefaultContexts.Save, "A TargetObjectType must be specified", ResultType = ValidationResultType.Error)]
        public Type TargetObjectType
        {
            get { return _DataType; }
            set { SetPropertyValue("TargetObjectType", ref _DataType, value); }
        }

        [Association("DocumentRequirementType-BusinessDocuments")]
        public XPCollection<BusinessDocumentData> BusinessDocuments
        {
            get
            {
                return GetCollection<BusinessDocumentData>("BusinessDocuments");
            }
        }

    }
}
