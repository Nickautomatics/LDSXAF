using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using QuickZ.Persistent.Base.Documents;
using QuickZ.Persistent.Base.Mails;
using QuickZ.Persistent.Base.Core;
using System.Collections;
using EDS.RentalManager.Express.Module.BusinessObjects.Common;

namespace QuickZ.Persistent.BaseImpl.Mailings
{
    //[NonPersistent]
    [ImageName("BOMailMergeEmbeddedTemplate")]
    [DevExpress.ExpressApp.DC.XafDefaultProperty(nameof(TemplateName))]
    public abstract class MailMergeTemplateBase : RMXQuickAuditBaseObject, IMailMergeTemplate, IObjectWithMailMergeTemplate, ILocationPrivacyObject
    {
        RMXLocationCategory ILocationPrivacyObject.Location => (this.HomeLocation);

        public MailMergeTemplateBase(Session session)
            : base(session)
        {
            
        }

        public bool AlwaysSyncTemplate
        {
            get
            {
                return true;
            }
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            AlwaysMergeData = true;            
        }

        IMailMergeTemplateEditObject IObjectWithMailMergeTemplate.MailMergeTemplateEditObject
        {
            get
            {
                return (IMailMergeTemplateEditObject)MailMergeTemplateEditObject;
            }
            set
            {
                MailMergeTemplateEditObject = (MailMergeTemplateEditObject)value;
            }
        }

        private bool _AlwaysMergeData;
        private MailMergeTemplateEditObject _MailMergeTemplateEditObject;
        [VisibleInListView(false)]
        [NonPersistent]
        [ImmediatePostData]
        [EditorAlias(BusinessWriterEditorAliases.MailMergeTemplateEditor)]
        //[Browsable(false)]
        public MailMergeTemplateEditObject MailMergeTemplateEditObject
        {
            get { return _MailMergeTemplateEditObject; }
            set
            {
                _MailMergeTemplateEditObject = value;
            }
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();

            SelectedTemplate = (IMailMergeTemplate)this;
        }

        // Fields...
        private BusinessDataOwnership _OwnerType;
        public BusinessDataOwnership OwnerType
        {
            get
            {
                return _OwnerType;
            }
            set
            {
                SetPropertyValue("OwnerType", ref _OwnerType, value);
            }
        }

        private IMailMergeTemplate _SelectedTemplate;
        [VisibleInListView(false)]
        [NonPersistent]
        [ImmediatePostData]
        public IMailMergeTemplate SelectedTemplate
        {
            get
            {
                return _SelectedTemplate;
            }
            set
            {
                _SelectedTemplate = value;
            }
        }

        private string _TemplateName;
        [Size(128)]
        public string TemplateName
        {
            get
            {
                return _TemplateName;
            }
            set
            {
                SetPropertyValue("TemplateName", ref _TemplateName, value);
            }
        }

        private Type _DataType;
        [TypeConverter(typeof(LocalizedClassInfoTypeConverter))]
        [ValueConverter(typeof(TypeToStringConverter))]
        public Type TargetObjectType
        {
            get { return _DataType; }
            set { SetPropertyValue("TargetObjectType", ref _DataType, value); }
        }

        
        //[Delayed(true), Persistent("Content"), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        //public byte[] Content

        private string _Content;
        [Persistent("Content"), Size(SizeAttribute.Unlimited)]
        public string Content
        {
            get
            {
                return _Content;
            }
            set
            {
                SetPropertyValue("Content", ref _Content, value);
            }
        }

        private string _HtmlContent;
        [NonPersistent]
        public string HtmlContent
        {
            get
            {
                return _HtmlContent;
            }
            set
            {
                SetPropertyValue("HtmlContent", ref _HtmlContent, value);
            }
        }

        public string TemplateRtfText
        {
            get
            {
                if (Content == null)
                    return "";
                return Content;
            }
        }

        public string TemplateHtmlText
        {
            get
            {
                if (HtmlContent == null)
                    return "";
                return HtmlContent;
            }
        }

        //[Browsable(false)]
        //IList IMailMergeTemplate.DocumentGroups
        //{
        //    get
        //    {
        //        return BusinessDocumentGroups;
        //    }
        //}

        [Browsable(false)]
        IList IMailMergeTemplate.Categories
        {
            get
            {
                return MailMergeTemplateCategories;
            }
        }
        
        //[DevExpress.ExpressApp.DC.XafDisplayName("Document Groups")]
        //[Association("BusinessDocumentGroups-MailMergeTemplateBases")]
        //public XPCollection<BusinessDocumentGroup> BusinessDocumentGroups
        //{
        //    get
        //    {
        //        return GetCollection<BusinessDocumentGroup>("BusinessDocumentGroups");
        //    }
        //}
        
        [DevExpress.ExpressApp.DC.XafDisplayName("Template Categories")]
        [Association("MailMergeTemplateCategories-MailMergeTemplateBases")]
        public XPCollection<MailMergeTemplateCategory> MailMergeTemplateCategories
        {
            get
            {
                return GetCollection<MailMergeTemplateCategory>("MailMergeTemplateCategories");
            }
        }

        [NonPersistent]
        [ImmediatePostData]
        public bool AlwaysMergeData
        {
            get
            {
                return _AlwaysMergeData;
            }
            set
            {
                SetPropertyValue("AlwaysMergeData", ref _AlwaysMergeData, value);
            }
        }
    }
}
