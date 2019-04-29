using System;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using EDS.RentalManager.Express.Module.BusinessObjects.Common;
using QuickZ.Persistent.Base.Documents;
using QuickZ.Persistent.Base.Core;
using System.Collections;
using DevExpress.ExpressApp.Utils;
using QuickZ.Persistent.BaseImpl.Mailings;

namespace QuickZ.Persistent.BaseImpl
{
  
    public abstract class PrintDocumentTemplateBase : RMXQuickAuditBaseObject, IDocumentPrintGroup, IObjectWithDocumentPrintMergeTemplate
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public PrintDocumentTemplateBase(Session session)
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

      

        private string _GroupName;
        [Size(128)]
        public string GroupName
        {
            get
            {
                return _GroupName;
            }
            set
            {
                SetPropertyValue("GroupName", ref _GroupName, value);
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
        private bool _AlwaysMergeData;
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
        [Browsable(false)]
        IList IDocumentPrintGroup.Categories
        {
            get
            {
                return PrintDocumnetTemplateCategories;
            }
        }
        [DevExpress.ExpressApp.DC.XafDisplayName("Categories")]
        [Association("PrintDocumnetTemplateCategories-PrintDocumentTemplateBases")]
        public XPCollection<PrintDocumentTemplateCategory> PrintDocumnetTemplateCategories
        {
            get
            {
                return GetCollection<PrintDocumentTemplateCategory>("PrintDocumnetTemplateCategories");
            }
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
            AlwaysMergeData = true;
        }
        protected override void OnLoaded()
        {
            base.OnLoaded();
        }
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
    }
}