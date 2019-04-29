//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using QuickZ.Persistent.Base.Mails;
using EDS.RentalManager.Express.Module.BusinessObjects.Common;
using QuickZ.Persistent.Base.Documents;

namespace QuickZ.Persistent.BaseImpl.Mailings
{
   // [DevExpress.ExpressApp.DC.XafDefaultProperty("CategoryName")]
   // [NavigationItem(true, GroupName = "Document Templates")]
    [DevExpress.ExpressApp.DC.XafDisplayName("Document Template Category")]
    [CreatableItem(true), DeferredDeletion(false)]
    [ImageName("BOMailMergeTemplateCategory")]
    public partial class PrintDocumentTemplateCategory : RMXQuickAuditBaseObject, IPrintDocumentMergeTemplateCategory
    {
        // Fields...
        private int _SortIndex;
        private string _CategoryName;

        [Size(128)]
        public string CategoryName
        {
            get
            {
                return _CategoryName;
            }
            set
            {
                SetPropertyValue("CategoryName", ref _CategoryName, value);
            }
        }


        public int SortIndex
        {
            get
            {
                return _SortIndex;
            }
            set
            {
                SetPropertyValue("SortIndex", ref _SortIndex, value);
            }
        }

        [Browsable(false)]
        IList<IDocumentPrintGroup> IPrintDocumentMergeTemplateCategory.DocumentTemplates
        {
            get
            {
                return DocumentTemplates as IList<IDocumentPrintGroup>;
            }
        }

        [DevExpress.ExpressApp.DC.XafDisplayName("Documents")]
        [Association("PrintDocumnetTemplateCategories-PrintDocumentTemplateBases")]
        public XPCollection<PrintDocumentTemplateBase> DocumentTemplates
        {
            get
            {
                return GetCollection<PrintDocumentTemplateBase>("DocumentTemplates");
            }
        }

        //[Association("MailMergeTemplateCategory-UserTemplates")]
        //public XPCollection<MailMergeRegularUserTemplate> UserTemplates
        //{
        //    get
        //    {
        //        return GetCollection<MailMergeRegularUserTemplate>("UserTemplates");
        //    }
        //}

    }

}