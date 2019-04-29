using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Base.General;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.ComponentModel;
using DevExpress.ExpressApp.ConditionalAppearance;
using EDS.RentalManager.Express.Module.BusinessObjects;
using DevExpress.Persistent.BaseImpl;
using System.Linq;
using QuickZ.BusinessEmail;
using QuickZ.Persistent.Base.Mails;
using QuickZ.Persistent.Base.Core;
using QuickZ.Persistent.Base.General;
using DevExpress.ExpressApp.Model;

namespace EDS.RentalManager.Express.Module.BusinessObjects.Common
{
    public interface ILocationCategory : ITreeNode
    {
        //string Name { get; set; }

        //ITreeNode Parent { get; set; }
    }

    public interface IDocumentFiltering
    {
        [ImmediatePostData]
        RMXLocationCategory Location { get; }
    }

    public interface ILocationPrivacyObject
    {
        [ImmediatePostData]
        RMXLocationCategory Location { get; }
    }

    public enum RMXLocationTypeEnum
    {
        Location = 0,
        Country = 1,
        State = 2,
        City = 3,
        Company = 4,
        Unknown = 9999
    }

    /// <summary>
    /// Used as a based class to enable displaying Locations in a Hierarchical/Tree View
    /// </summary>
    /// TOFollow: IsCurrentUserAnAdministrator property was removed
    //[Appearance("RMXLocationCategoryDisableOrganizationAndLocation", AppearanceItemType = "ViewItem", Enabled = false,
    //TargetItems = "Organization,OrganizationName", Criteria = "NOT IsCurrentUserAnAdministrator", Context = "DetailView",
    //         FontColor = "Blue")]
    [NavigationItem(false)]
    [CreatableItem(false)]
    [Persistent("Location")]
    [XafDisplayName("Location")]
    [DevExpress.ExpressApp.DC.XafDefaultProperty(nameof(Name))]
    public abstract class RMXLocationCategory : RMXBaseObject, ILocationCategory, ITreeNode, IRMXOrganizationBoundObject, IRMXSequenceObject
    {
        public RMXLocationCategory(Session session, string name)
    : this(session)
        {
            this.name = name;
        }
        public RMXLocationCategory(Session session)
            : base(session)
        {

        }
        public RMXLocationCategory(Session session, Guid oidOverride)
            : base(session, oidOverride)
        {

        }

        public RMXLocationCategory(Session session, Guid oidOverride, RMXOrganizationBase organization)
            : base(session, oidOverride)
        {
            Organization = organization;
        }
        //static XPCollection<RMXLocationCategory> _GetLocations = null;

        //public static XPCollection<RMXLocationCategory> GetLocations(Session session)
        //{
        //    if(_GetLocations == null)
        //       _GetLocations = new XPCollection<RMXLocationCategory>(session, null, new SortProperty(nameof(LocationName), DevExpress.Xpo.DB.SortingDirection.Ascending));
        //    return _GetLocations;
        //}
        string _VehicleWebUniqueID;
        public string VehicleWebUniqueID {
            get => _VehicleWebUniqueID;
            set => _VehicleWebUniqueID = value;
        }
        //[NonPersistent]
        [RuleRequiredField("RuleRequiredField for RMXLocationCategory.LocationName", DefaultContexts.Save, "A LocationName must be specified", ResultType = ValidationResultType.Error)]
        [ImmediatePostData]
        public string LocationName
        {
            get => Name;

            //#if !DEBUG
            //            private set
            //#else
            set => Name = value;
        }

        private string name;

        [NonPersistent]
        [RuleFromBoolProperty("LocationCategoryCircularReferences", DefaultContexts.Save, "Circular reference detected. To correct this error, set the Parent property to another value.", UsedProperties = "Parent")]
        [Browsable(false)]
        public bool IsValid
        {
            get
            {
                ITreeNode currentObj = (ITreeNode)Parent;
                while (currentObj != null)
                {
                    if (currentObj == this)
                    {
                        return false;
                    }
                    currentObj = currentObj.Parent;
                }
                return true;
            }
        }

        public ITreeNode Parent
        {
            get { return null; }
        }
        public IBindingList Children
        {
            get { return new BindingList<object>(); }
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            // --- Generate Sequence Object
            if (String.IsNullOrEmpty(FriendlyId) || FriendlyId == "0")
                FriendlyId = GetSequenceId();

            if (Organization == null)
                UpdateOrganization();

            IsActive = true;

            CreatedBy = SecuritySystem.CurrentUserName;
            CreatedByUserId = (Guid)(SecuritySystem.CurrentUserId ?? Guid.Empty);
            CreatedOn = DateTime.Now;

        }

        public virtual void UpdateOrganization()
        {
            // --- Update Organization
            if (Organization == null)
                SetOrganization(DomainService.Instance.ActiveOrganizationId);
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

            if (Organization == null)
                UpdateOrganization();
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();

            if (String.IsNullOrEmpty(OrganizationName) 
                || Organization == null)
                UpdateOrganization();


            if (String.IsNullOrEmpty(FriendlyId) || FriendlyId == "0")
                FriendlyId = GetSequenceId();
        }

        private string _OrganizationName;
        /// <summary>
        /// Used for generating big data reports and analytics
        /// </summary>
#if RELEASE
        [Browsable(false)]
#endif
        [Size(128)]
        [ImmediatePostData]
        public string OrganizationName
        {
            get
            {
                return _OrganizationName;
            }
            set
            {
                SetPropertyValue(nameof(OrganizationName), ref _OrganizationName, value);
                if (this is RMXOrganizationBase)
                    LocationName = value;
            }
        }

        #region XPO Overrides



        #endregion

        private RMXLocationTypeEnum _LocationType;
        public RMXLocationTypeEnum LocationType
        {
            get { return _LocationType; }
            set { _LocationType = value; }
        }

        private RMXOrganizationBase _Organization;
        //[VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        [ImmediatePostData]
        //[RuleRequiredField]
        //[Browsable(false)]
        public RMXOrganizationBase Organization {
            get => _Organization;
            //#if !DEBUG
            //            private set
            //#else
            set => SetPropertyValue(nameof(Organization), ref _Organization, value);
        }

        //[RuleFromBoolProperty("ContactUniqueEmail", DefaultContexts.Save, "Contact with this Email already exists")]
        //protected bool IsEmailUnique
        //{
        //    get
        //    {
        //        string toLowerEmail = Object.ReferenceEquals(Email, null) ? String.Empty : Email.ToLower();
        //        XPCollection<Contact> contacts =
        //              new XPCollection<Contact>(PersistentCriteriaEvaluationBehavior.InTransaction,
        //              Session, new BinaryOperator(new FunctionOperator(FunctionOperatorType.Lower,
        //              new OperandProperty("Email")),
        //           new OperandValue(toLowerEmail), BinaryOperatorType.Equal));
        //        foreach (Contact contact in contacts)
        //        {
        //            if (contact != this)
        //            {
        //                return false;
        //            }
        //        }
        //        return true;
        //    }
        //}

        ///// <summary>
        ///// For faster request to User's Administrative access
        ///// </summary>
        //[Browsable(false)]
        //public bool IsCurrentUserAnAdministrator
        //{
        //    get { return BusinessEngine.Instance.IsCurrentUserAnAdministrator; }
        //}

        public void SetOrganization(Guid organizationOid)
        {
            if (organizationOid == null)
            {
                DomainService.Instance.LogValue("RMXLocationBoundBaseObject.SetOrganization(organization)", organizationOid);
                return;
            }
            var newOrganization = Session.GetObjectByKey<RMXOrganizationBase>(organizationOid);
            Organization = newOrganization;
            if (newOrganization != null)
                if (OrganizationName != newOrganization.OrganizationName)
                    OrganizationName = newOrganization.OrganizationName;
        }
        private string _Description;
        [Size(4096)]
        public string Description {
            get => _Description;
            set => SetPropertyValue(nameof(Description), ref _Description, value);
        }

        [ImmediatePostData]
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        [Size(255)]
        [RuleUniqueValue]
        [RuleRequiredField("RuleRequiredField for RMXLocationCategory.Name", DefaultContexts.Save, "A Name must be specified", ResultType = ValidationResultType.Error)]
        [XafDisplayName("Name")]
        public string Name {
            get => name;
            set => SetPropertyValue(nameof(Name), ref name, value);
        }

        protected override void OnDeleting()
        {
            if (IsSystemObject)
                throw new UserFriendlyException("You cannot delete a SYSTEM object. Please contact your Administrator for assistance.");

            base.OnDeleting();
        }


        #region ITreeNode Properties

        IBindingList ITreeNode.Children
        {
            get { return Children as IBindingList; }
        }
        ITreeNode ITreeNode.Parent
        {
            get { return Parent as ITreeNode; }
        }

        #endregion

        #region Gantt View Properties

        [ImmediatePostData]
        [NonPersistent]
        public System.Drawing.Color Color
        {
            get { return System.Drawing.Color.FromArgb(ColorValue); }
            set { ColorValue = value.ToArgb(); }
        }

        [Browsable(false)]
        [Persistent("Color")]
        public int ColorValue
        {
            get { return GetPropertyValue<int>(nameof(ColorValue)); }
            set { SetPropertyValue<int>(nameof(ColorValue), value); }
        }

        [Browsable(false),
        NonPersistent]
        public string Caption
        {
            get { return Name; }
            set { Name = value; }
        }

        [Browsable(false)]
        public object Id
        {
            get { return Oid; }
        }

        [Browsable(false)]
        public int OleColor
        {
            get { return System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(ColorValue)); }
        }

        #endregion

        #region System Properties


        #endregion

        #region IQuickAuditBaseObject Members

        private bool _IsActive;
        private DateTime? _CreatedOn;
        private DateTime? _ModifiedOn;
        private string _CreatedBy;
        private string _ModifiedBy;
        private string _Tag;

        //[VisibleInDetailView(false), VisibleInListView(false)]
        //public bool IsActive
        //{
        //    get { return _IsActive; }
        //    set { SetPropertyValue("IsActive", ref _IsActive, value); }
        //}
        [VisibleInDetailView(false),
        VisibleInListView(false)]
        public string CreatedBy {
            get => _CreatedBy;
            set => SetPropertyValue(nameof(CreatedBy), ref _CreatedBy, value);
        }
        [VisibleInDetailView(false),
        VisibleInListView(false)]
        [ModelDefault("DisplayFormat", ConstantResources.DefaultDateAndTimeOfDayEditMask)]
        [ModelDefault("EditMask", ConstantResources.DefaultDateAndTimeOfDayEditMask)]
        public DateTime? CreatedOn {
            get => _CreatedOn;
            set => SetPropertyValue<DateTime?>(nameof(CreatedOn), ref _CreatedOn, value);
        }
        Guid createdByUserId;
        public Guid CreatedByUserId {
            get => createdByUserId;
            set => SetPropertyValue(nameof(CreatedByUserId), ref createdByUserId, value);
        }
        [VisibleInDetailView(false),
        VisibleInListView(false), VisibleInLookupListView(false)]
        Guid modifiedByUserId;
        public Guid ModifiedByUserId {
            get => modifiedByUserId;
            set => SetPropertyValue(nameof(ModifiedByUserId), ref modifiedByUserId, value);
        }
        [VisibleInDetailView(false),
        VisibleInListView(false)]
        public string ModifiedBy {
            get => _ModifiedBy;
            set => SetPropertyValue(nameof(ModifiedBy), ref _ModifiedBy, value);
        }
        [VisibleInDetailView(false),
        VisibleInListView(false)]
        public DateTime? ModifiedOn {
            get => _ModifiedOn;
            set => SetPropertyValue<DateTime?>(nameof(ModifiedOn), ref _ModifiedOn, value);
        }

        [Size(SizeAttribute.Unlimited)]
        //[VisibleInDetailView(false),
        //VisibleInListView(false)]
        [Browsable(false)]
        public string Tag {
            get { return _Tag; }
            set { SetPropertyValue(nameof(Tag), ref _Tag, value); }
        }

        #endregion

        #region IRMXSequencedObject Members
        [RuleUniqueValue]
        [RuleRequiredField("RuleRequiredField for RMXLocationCategory.RecordId", DefaultContexts.Save, "A RecordId must be specified", ResultType = ValidationResultType.Error)]
        [DevExpress.ExpressApp.DC.XafDisplayName("RecordId")]
        public virtual string FriendlyId {
            get => GetPropertyValue<string>(nameof(FriendlyId));
            set => SetPropertyValue<string>(nameof(FriendlyId), value);
        }

        [RuleUniqueValue]
        [RuleRequiredField]
        public virtual long? SequenceId {
            get => GetPropertyValue<long?>(nameof(SequenceId));
            set => SetPropertyValue<long?>("SequenceId", value);
        }

        public virtual string GetSequenceId()
        {
            var prefix = "LOC";
            if (SequenceId != null)
                return prefix + String.Format("{0:D6}", SequenceId);

            SequenceId = DevExpress.Persistent.BaseImpl
                        .DistributedIdGeneratorHelper
                        .Generate(this.Session.DataLayer, this.GetType().FullName, string.Empty) + 1;
            return prefix + String.Format("{0:D6}", SequenceId);
        }

        #endregion

    }
}
