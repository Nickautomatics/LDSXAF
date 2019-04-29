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
using QuickZ.Persistent.Base.Core;
using QuickZ.Persistent.Base.Documents;
using System.Collections;
using EDS.RentalManager.Express.Module.BusinessObjects.Common;

namespace QuickZ.Persistent.BaseImpl
{
//#if DEBUG
//    [DefaultClassOptions]

//#endif
    [DevExpress.ExpressApp.DC.XafDefaultProperty(nameof(PrintDocumentGroup))]
    public class PrintDocumentGroupDocument : RMXQuickAuditBaseObject, ILocationPrivacyObject
    {// Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        RMXLocationCategory ILocationPrivacyObject.Location => (this.HomeLocation);
        
        public PrintDocumentGroupDocument(Session session)
            : base(session)
        {
        }
      
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
        
       
        private PrintDocumentGroupTemplate printDocumentGroup;
        [Association("PrintDocumentGroupDocument-BusinessDocuments")]
        [ImmediatePostData]
        public PrintDocumentGroupTemplate PrintDocumentGroup
        {
            get { return printDocumentGroup; }
            set { SetPropertyValue("PrintDocumentGroup", ref printDocumentGroup, value); }
        }

        private BusinessDocumentData _DocumentData;
        [ImmediatePostData]
        [Association("BusinessDocumentData-PrintGroups")]
        public BusinessDocumentData DocumentData 
        {
            get
            {
                return _DocumentData;
            }
            set
            {
                SetPropertyValue<BusinessDocumentData>("DocumentData",ref _DocumentData, value);
            }
        }

       private int pageSequence;
        [ImmediatePostData]
        public int PageSequence
        {
            get
            {
                return pageSequence;
            }
            set
            {
                SetPropertyValue("PageSequence", ref pageSequence, value);
            }
        }
    }
}