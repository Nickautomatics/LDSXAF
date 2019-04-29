using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using QuickZ.Persistent.Base.Documents;
using QuickZ.Persistent.Base.Mails;
using QuickZ.Persistent.BaseImpl.Mailings;
using System.IO;
using DevExpress.ExpressApp;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Snap;
using EDS.RentalManager.Express.Module.BusinessObjects.Common;
using DevExpress.ExpressApp.DC;

namespace QuickZ.Persistent.BaseImpl
{
    [NavigationItem(false), CreatableItem(true)]
    [DevExpress.ExpressApp.DC.XafDefaultProperty(nameof(DocumentName))]
    [ImageName("BOBusinessDocumentData")]
    [DevExpress.ExpressApp.DC.XafDisplayName("Business Document")]
    [Persistent("BusinessDocument")]
    public abstract class BusinessDocumentData : RMXQuickAuditBaseObject, IBusinessDocument, ILocationPrivacyObject
    {
        RMXLocationCategory ILocationPrivacyObject.Location => (this.HomeLocation);

        
        
        private string _DocumentName;
        private DocumentContentFormat _DocumentFormat;
        private DocumentContentType documentType;

        public BusinessDocumentData(Session session)
            : base(session)
        {

        }
        public BusinessDocumentData(Session session, Guid oidOverride)
            : base(session, oidOverride)
        {
            
        }

        public abstract string FileNameAsAttachment { get; set; }

        [VisibleInDetailView(false)]
        [Size(255)]
        [ImmediatePostData]
        public string DocumentName
        {
            get
            {
                return _DocumentName;
            }
            set
            {
                SetPropertyValue(nameof(DocumentName), ref _DocumentName, value);
            }
        }

        [VisibleInDetailView(false)]
        [ImmediatePostData]
        public DocumentContentType DocumentType
        {
            get { return documentType; }
            set { SetPropertyValue(nameof(DocumentType), ref documentType, value); }

        }


        bool isFranchiseDocument;
        public bool IsFranchiseDocument {
            get {
                return isFranchiseDocument;
            }
            set {
                SetPropertyValue(nameof(IsFranchiseDocument), ref isFranchiseDocument, value);
            }
        }
        /// <summary>
        /// Refers to the output format that will be generated
        /// </summary>
        [VisibleInDetailView(false)]
        [ImmediatePostData]
        public DocumentContentFormat DocumentFormat
        {
            get
            {
                return _DocumentFormat;
            }
            set
            {
                SetPropertyValue(nameof(DocumentFormat), ref _DocumentFormat, value);
            }
        }

        [Association("MailMergeRegularTemplates-AdditionalDocuments")]
        [Browsable(false)]
        public XPCollection<MailMergeRegularTemplateBase> MailMergeRegularTemplates
        {
            get
            {
                return GetCollection<MailMergeRegularTemplateBase>(nameof(MailMergeRegularTemplates));
            }
        }

        //[Association("PrintDocumentGroupTemplates-BusinessDocuments")]
        //[Browsable(false)]
        //public XPCollection<PrintDocumentGroupTemplate> PrintDocumentGroupS
        //{
        //    get
        //    {
        //        return GetCollection<PrintDocumentGroupTemplate>(nameof(PrintDocumentGroupS));
        //    }
        //}

        private bool _IsSignatureRequired;
        [ImmediatePostData]
        public bool IsSignatureRequired
        {
            get
            {
                return _IsSignatureRequired;
            }
            set
            {
                SetPropertyValue(nameof(IsSignatureRequired), ref _IsSignatureRequired, value);
            }
        }

        [ImmediatePostData]
        public string RequiredAs
        {
            get
            {
                if (DocumentRequirementType == null) return "";
                return DocumentRequirementType.RequirementName;
            }
        }

        private DocumentRequirementType _DocumentRequirementType;
        [Association("DocumentRequirementType-BusinessDocuments")]
        [ImmediatePostData]
        public DocumentRequirementType DocumentRequirementType
        {
            get
            {
                return _DocumentRequirementType;
            }
            set
            {
                SetPropertyValue(nameof(DocumentRequirementType), ref _DocumentRequirementType, value);
            }
        }

        private string _Description;
        [Size(2048)]
        public string Description
        {
            get
            {
                return _Description;
            }
            set

            {
                SetPropertyValue(nameof(Description), ref _Description, value);
            }
        }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);

            switch (propertyName)
            {
                case nameof(DocumentName):
                    if (String.IsNullOrEmpty(FileNameAsAttachment))
                        FileNameAsAttachment = DocumentName + ".pdf";
                    break;
            }
        }

        private bool _ShowAsInplaceDocument;
        /// <summary>
        /// Filters which documents are shown in a Print List option
        /// </summary>
        public bool ShowAsInplaceDocument
        {
            get
            {
                return _ShowAsInplaceDocument;
            }
            set
            {
                SetPropertyValue(nameof(ShowAsInplaceDocument), ref _ShowAsInplaceDocument, value);
            }
        }

        //protected override void OnSaving()
        //{
        //    base.OnSaving();            
        //}

        [Association("BusinessDocumentData-PrintGroups")]
        public XPCollection<PrintDocumentGroupDocument> PrintGroups
        {
            get
            {
                return GetCollection<PrintDocumentGroupDocument>(nameof(PrintGroups));
            }
        }
    }
}
