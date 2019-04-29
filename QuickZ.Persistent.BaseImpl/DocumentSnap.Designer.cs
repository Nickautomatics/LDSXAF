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
using QuickZ.Persistent.BaseImpl;
using System.Collections;
using DevExpress.Persistent.Base;
using QuickZ.Persistent.Base.Documents;
using DevExpress.ExpressApp.ConditionalAppearance;
using EDS.RentalManager.Express.Module.BusinessObjects;
using EDS.RentalManager.Express.Module.BusinessObjects.Common;
using DevExpress.ExpressApp.DC;
using QuickZ.Persistent.Base;

namespace QuickZ.Persistent.BaseImpl
{
    [DevExpress.ExpressApp.DC.DomainComponent]
    [NavigationItem(false), CreatableItem(true)]
    [DevExpress.ExpressApp.DC.XafDefaultProperty(nameof(DocumentName))]
    //[ImageName("BO_List")]
    [ImageName("ViewDocumentSnap")]
    [DevExpress.ExpressApp.DC.XafDisplayName("Snap Document")]
    [MapInheritance(MapInheritanceType.ParentTable)]
    [Appearance("ExistingReport", AppearanceItemType = "ViewItem",
    TargetItems = "ShowAsInplaceDocument,FileNameAsAttachment,DocumentName,Description,DocumentRequirementType", Criteria = "", Context = "ListView", BackColor = "White",
    FontColor = "Gray", Priority = 0)]
    public partial class DocumentSnap : BusinessDocumentData, IBusinessDocumentSnap, IBusinessDocument, ITagAsSystemObject, ICloneObjectToLocation, IDocumentFiltering
    {
        RMXLocationCategory IDocumentFiltering.Location => (this.HomeLocation);

        #region IDocumentSnap Properties

        [Browsable(false)]
        private bool _IsShowOnWeb;
        public bool IsShowOnWeb {
            get {
                return _IsShowOnWeb;
            }
            set {
                SetPropertyValue(nameof(IsShowOnWeb), ref _IsShowOnWeb, value);
            }
        }


        [Delayed(true), Browsable(false)]
        public byte[] Content
        {
            get { return GetDelayedPropertyValue<byte[]>("Content"); }
            set { SetDelayedPropertyValue<byte[]>("Content", value); }
        }

        [Association("DocumentSnap-DataSources"), DevExpress.Xpo.Aggregated]
        public XPCollection<DocumentSnapDataSource> DataSources
        {
            get
            {
                return GetCollection<DocumentSnapDataSource>(nameof(DataSources));
            }
        }

        private DocumentSnap _BackPage;
        private DocumentSnapDataSource _InplaceDocumentDataSource;
        [DataSourceProperty("DataSources")]
        public DocumentSnapDataSource InplaceDocumentDataSource
        {
            get { return _InplaceDocumentDataSource; }
            set { SetPropertyValue("InplaceDocumentDataSource", ref _InplaceDocumentDataSource, value); }
        }

        IList IBusinessDocumentSnap.DataSources
        {
            get
            {
                return DataSources;
            }
        }

        IDocumentSnapDataSource IBusinessDocumentSnap.InplaceDocumentDataSource
        {
            get
            {
                return InplaceDocumentDataSource as IDocumentSnapDataSource;
            }
        }

        private string _FileNameAsAttachment;
        public override string FileNameAsAttachment
        {
            get
            {
                if (String.IsNullOrEmpty(_FileNameAsAttachment))
                    _FileNameAsAttachment = DocumentName + ".pdf";
                return _FileNameAsAttachment;
            }
            set
            {
                SetPropertyValue("FileNameAsAttachment", ref _FileNameAsAttachment, value);
            }
        }


        IBusinessDocumentSnap IBusinessDocumentSnap.BackPage
        {
            get
            {
                return (IBusinessDocumentSnap)BackPage;
            }
            set
            {
                BackPage = (DocumentSnap)value;
            }
        }

        [DataSourceProperty("BackPageCandidates")]
        public DocumentSnap BackPage
        {
            get
            {
                return _BackPage;
            }
            set
            {
                SetPropertyValue("BackPage", ref _BackPage, value);
            }
        }
        XPCollection<DocumentSnap> _BackPageCandidates = null;
        XPCollection<DocumentSnap> BackPageCandidates
        {
            get
            {
                if(_BackPageCandidates == null)
                       _BackPageCandidates = new XPCollection<DocumentSnap>(PersistentCriteriaEvaluationBehavior.InTransaction, Session, CriteriaOperator.Parse("Oid <> ?", this.Oid));
                return _BackPageCandidates;
            }
        }

        private BusinessDocumentData _LinkedBusinessDocument;
        [NoForeignKey]
        public BusinessDocumentData LinkedBusinessDocument
        {
            get
            {
                return _LinkedBusinessDocument;
            }
            set
            {
                SetPropertyValue(nameof(LinkedBusinessDocument), ref _LinkedBusinessDocument, value);
            }
        }

        #endregion
    }

}
