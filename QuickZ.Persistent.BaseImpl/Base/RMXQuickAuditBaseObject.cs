using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System;
using System.ComponentModel;
using System.Drawing;
using DevExpress.ExpressApp.Xpo;
using QuickZ.Persistent.Base.Core;
using DevExpress.ExpressApp.Model;
using QuickZ.Persistent.Base.General;
using DevExpress.ExpressApp.DC;

namespace EDS.RentalManager.Express.Module.BusinessObjects.Common
{
    public enum AccessStatus
    {
        Draft = 0,
        Published = 1,
        Archived = 99
    }

    /// <summary>
    /// Provides a base class responsible for common properties Audit logging.
    /// </summary>
    /// <remarks>
    /// * All audited objects must have a Name and it's automatically defined in this class.
    /// * Employee reference is NOT used in the Audit entries, to allow Employees to be deleted.   
    /// </remarks>    
    [NonPersistent]
    //[Appearance("Display Name Bold", AppearanceItemType = "ViewItem", TargetItems = "DisplayName", FontStyle = FontStyle.Bold)]
    //[Appearance("Name Bold", AppearanceItemType = "ViewItem", TargetItems = "Name", FontStyle = FontStyle.Bold)]
    [ImageName("BOAuditBase")]
    public interface IQuickAuditObject
    {
        string CreatedBy { get; set; }
        Guid CreatedByUserId { get; set; }
        DateTime? CreatedOn { get; set; }
        string ModifiedBy { get; set; }
        DateTime? ModifiedOn { get; set; }
        Guid ModifiedByUserId { get; set; }
        string Tag { get; set; }
        void AfterConstruction();
        AccessStatus AccessStatus { get; set; }
    }

    [NonPersistent]
    [DevExpress.ExpressApp.DC.XafDefaultProperty(nameof(CreatedBy))]
    public abstract class RMXQuickAuditBaseObject : RMXLocationBoundBaseObject, IQuickAuditObject
    {

        
     

        public RMXQuickAuditBaseObject(Session session)
            : base(session)
        {
        }
        public RMXQuickAuditBaseObject(Session session, Guid oidOverride)
            : base(session, oidOverride)
        {

        }

        private string _CreatedBy;
        [VisibleInDetailView(false),
        VisibleInListView(false)]
        public string CreatedBy {
            get => _CreatedBy;
            set => SetPropertyValue("CreatedBy", ref _CreatedBy, value);
        }
        Guid createdByUserId;
        [VisibleInDetailView(false), VisibleInListView(false),
        VisibleInLookupListView(false)]
        public Guid CreatedByUserId {
            get => createdByUserId;
            set => SetPropertyValue(nameof(CreatedByUserId), ref createdByUserId, value);
        }
        private DateTime? _CreatedOn;
        [VisibleInDetailView(false),
        VisibleInListView(false)]
        //[ModelDefault("DisplayFormat", ConstantResources.DefaultDateAndTimeOfDayEditMask)]
        //[ModelDefault("EditMask", ConstantResources.DefaultDateAndTimeOfDayEditMask)]
        [ModelDefault("EditMask", "G")]
        [ModelDefault("DisplayFormat", "{0:G}")]
        public DateTime? CreatedOn {
            get => _CreatedOn;
            set => SetPropertyValue<DateTime?>(nameof(CreatedOn), ref _CreatedOn, value);
        }

        private string _ModifiedBy;
        [VisibleInDetailView(false),
        VisibleInListView(false)]
        public string ModifiedBy {
            get => _ModifiedBy;
            set => SetPropertyValue(nameof(ModifiedBy), ref _ModifiedBy, value);
        }
        Guid modifiedByUserId;
        [VisibleInDetailView(false),
        VisibleInListView(false), VisibleInLookupListView(false)]
        public Guid ModifiedByUserId {
            get => modifiedByUserId;
            set => SetPropertyValue(nameof(ModifiedByUserId), ref modifiedByUserId, value);
        }
        private DateTime? _ModifiedOn;
        [VisibleInDetailView(false),
        VisibleInListView(false)]
        [ModelDefault("DisplayFormat", ConstantResources.DefaultDateAndTimeOfDayEditMask)]
        [ModelDefault("EditMask", ConstantResources.DefaultDateAndTimeOfDayEditMask)]
        public DateTime? ModifiedOn {
            get => _ModifiedOn;
            set => SetPropertyValue<DateTime?>(nameof(ModifiedOn), ref _ModifiedOn, value);
        }

        private string _Tag;
        [Size(SizeAttribute.Unlimited)]
        [VisibleInDetailView(false),
        VisibleInListView(false)]
        //[Browsable(false)]
        public string Tag {
            get => _Tag;
            set => SetPropertyValue("Tag", ref _Tag, value);
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            IsActive = true;

            CreatedBy = SecuritySystem.CurrentUserName;
            CreatedByUserId = (Guid)(SecuritySystem.CurrentUserId ?? Guid.Empty);
            CreatedOn = DateTime.Now;
        }
        protected override void OnLoaded()
        {
            base.OnLoaded();
        }
        protected override void OnSaving()
        {
            base.OnSaving();
            if (!Session.IsNewObject(this))
            {
                ModifiedBy = SecuritySystem.CurrentUserName;
                ModifiedByUserId = (Guid)(SecuritySystem.CurrentUserId ?? Guid.Empty);
                ModifiedOn = DateTime.Now;
            }

            UpdateOrganizationAndLocation();
        }

        private RMXOrganizationBase _Organization;
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public override RMXOrganizationBase Organization {
            get => _Organization;
            set => SetPropertyValue("Organization", ref _Organization, value);
        }

        private RMXLocationCategory _HomeLocation;
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(true)]
        [XafDisplayName("Home Location")]
        public override RMXLocationCategory HomeLocation {
            get => _HomeLocation;
            set => SetPropertyValue(nameof(HomeLocation), ref _HomeLocation, value);
        }

        private AccessStatus _AccessStatus;
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        [VisibleInLookupListView(false)]
        //[Browsable(false)]
        public AccessStatus AccessStatus {
            get => _AccessStatus;
            set => SetPropertyValue("AccessStatus", ref _AccessStatus, value);
        }
    }
}
