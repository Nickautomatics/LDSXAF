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

namespace QuickZ.Persistent.BaseImpl.Mailings
{
    [DevExpress.ExpressApp.DC.XafDefaultProperty(nameof(CategoryName))]
    [NavigationItem(true, GroupName = "Email Templates")]
    [DevExpress.ExpressApp.DC.XafDisplayName("Stock Template Category")]
    [CreatableItem(true), DeferredDeletion(false)]
    [ImageName("BOMailMergeTemplateCategory")]
    public partial class MailMergeTemplateCategory : RMXQuickAuditBaseObject, IMailMergeTemplateCategory, ILocationPrivacyObject
    {
        RMXLocationCategory ILocationPrivacyObject.Location => (this.HomeLocation);

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
        IList<IMailMergeTemplate> IMailMergeTemplateCategory.Templates
        {
            get
            {
                return Templates as IList<IMailMergeTemplate>;
            }
        }

        [DevExpress.ExpressApp.DC.XafDisplayName("Templates")]
        [Association("MailMergeTemplateCategories-MailMergeTemplateBases")]
        public XPCollection<MailMergeTemplateBase> Templates
        {
            get
            {
                return GetCollection<MailMergeTemplateBase>("Templates");
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
