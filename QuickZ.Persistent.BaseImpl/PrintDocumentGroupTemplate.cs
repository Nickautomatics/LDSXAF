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
using EDS.RentalManager.Express.Module.BusinessObjects;
using EDS.RentalManager.Express.Module.BusinessObjects.Common;

namespace QuickZ.Persistent.BaseImpl
{
    //[DomainComponent]
//#if DEBUG
//    [DefaultClassOptions]
//#endif
   //[NavigationItem(true, GroupName = "Home")]
    //[CreatableItem(true),
    //DeferredDeletion(false)]
    [MapInheritance(MapInheritanceType.ParentTable)]
    [DevExpress.ExpressApp.DC.XafDefaultProperty(nameof(GroupName))]
    // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
    public class PrintDocumentGroupTemplate : PrintDocumentGroupTemplateBase, IDocumentPrintWithDocuments, ITagAsSystemObject
    { 
        IObjectSpace os;

        public PrintDocumentGroupTemplate(Session session)
            : base(session)
        {

        }
        
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
        [ImmediatePostData]
        [Association("PrintDocumentGroupDocument-BusinessDocuments")]
        public XPCollection<PrintDocumentGroupDocument> Documents
        {
            get
            {
                
               return GetCollection<PrintDocumentGroupDocument>("Documents");
            }
        }
        [Browsable(false)]
        IList IDocumentPrintWithDocuments.Documents
        {
            get { return Documents as IList; }
        }
    }
}