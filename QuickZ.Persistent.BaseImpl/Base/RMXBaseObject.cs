using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using QuickZ.Persistent.Base.Core;
using QuickZ.Persistent.Base;
using EDS.RentalManager.Express.Module.BusinessObjects.Common;
using DevExpress.ExpressApp.ConditionalAppearance;

namespace EDS.RentalManager.Express.Module.BusinessObjects
{
    public interface IFreeMilesTarget
    {
        double FreeMiles { get; set; }
    }
    public interface IActivatedObject
    {
        bool IsActive { get; set; }
        void Activate();
        void Deactivate();
    }
    public interface ILocationSharedObject
    {
        
    }
    public interface ITagAsSystemObject
    {

    }
    public interface IPurgeRecords
    {
        
    }
    public interface ICreateEstimate
    {

    }
    public interface IRemovedObjectCRUDAndCloneAction
    {

    }

    public interface ISecurityFiltering
    {
        
    }


    public interface IAlternateColorTarget
    {

    }

    public interface IRMXFindPanelTarget
    {

    }
    public interface IViewInfoBar
    {
        string MainTitle { get; set; }
        string Subject { get; set; }
    }
    public interface IRMXBaseObject
    {
        Guid Oid { get; }
        //RMXOrganization Organization { get; set; }
        //RMXLocation Location { get; set; }
    }
    public interface IHasDisplayText
    {
        string GetDisplayText();
    }

    [NonPersistent]
    [DevExpress.ExpressApp.DC.XafDefaultProperty(nameof(Oid))]
    [Appearance("IsUserSuperAdminEnableObject", AppearanceItemType = "ViewItem",
    TargetItems = "*", Criteria = "CurrentUserIsSuperAdmin() AND IsSystemObject = true", Context = "ListView", BackColor = "LightGray",
    FontColor = "Blue", Priority = 0, Enabled = true)]

    [Appearance("IsUserSuperAdminDisableObject", AppearanceItemType = "ViewItem",
    TargetItems = "*", Criteria = "!CurrentUserIsSuperAdmin() AND IsSystemObject = true", Context = "ListView", BackColor = "LightGray",
    FontColor = "Blue", Priority = 0, Enabled = false)]
    public abstract class RMXBaseObject : XPCustomObject, IRMXBaseObject, IAlternateColorTarget, IRMXFindPanelTarget, IHasDisplayText, ISystemObject, IActivatedObject, IQRCodeTarget
    {
        #region Properies
        //public static bool IsXpoProfiling = false;
#if MediumTrust
		    private Guid oid = Guid.Empty;
		    [Browsable(false), Key(true), NonCloneable]
		    public Guid Oid {
			    get { return oid; }
			    set { oid = value; }
		    }
#else
        [Persistent("Oid"),
        Key(true),
        Browsable(false),
        MemberDesignTimeVisibility(false),
        NonCloneable]
        private Guid oid = Guid.Empty;
        [PersistentAlias("oid"),
        Browsable(false)]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public Guid Oid
        {
            get { return oid; }
        }
#endif

        bool isStandardizedObject;
        [Browsable(false)]
        [NonCloneable]
        [ImagesForBoolValues("ImageForTrue_16x16", "ImageForFalse_16x16")]
        [CaptionsForBoolValues("TRUE", "FALSE")]
        public bool IsStandardizedObject
        {
            get { return isStandardizedObject; }
            set { SetPropertyValue("IsStandardizedObject", ref isStandardizedObject, value); }
        }
        private bool _IsSystemObject;
#if RELEASE
            [Browsable(false)]
#endif
        [NonCloneable()]
        [ImagesForBoolValues("ImageForTrue_16x16", "ImageForFalse_16x16")]
        [CaptionsForBoolValues("TRUE", "FALSE")]
        public bool IsSystemObject
        {
            get
            {
                return _IsSystemObject;
            }
            set
            {
                SetPropertyValue("IsSystemObject", ref _IsSystemObject, value);
            }
        }
        #endregion
        private bool isDefaultPropertyAttributeInit = false;
        private XPMemberInfo defaultPropertyMemberInfo;

        public RMXBaseObject(Session session)
            : base(session)
        {
        }
        public RMXBaseObject(Session session, Guid oidOverride)
            : this(session)
        {
            SetUniqueID(oidOverride);
        }

        #region XPO Overrides
        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (BaseObject.OidInitializationMode == OidInitializationMode.AfterConstruction)
                SetUniqueID(XpoDefault.NewGuid());

            if (DomainService.Instance.ConnectionString.Contains("PrimeThePump"))
                IsStandardizedObject = true;

            if ((DomainService.Instance.IsSuperAdmin) && (DomainService.Instance.ConnectionString.Contains("PrimeThePump")))
                IsSystemObject = true;
            else if (!IsSystemObject && !(DomainService.Instance.IsSuperAdmin) && !(DomainService.Instance.ConnectionString.Contains("PrimeThePump")))
                IsSystemObject = false;
        }

        //protected override void OnSaving()
        //{
        //    base.OnSaving();
        //    
        //}
        protected override void OnDeleting()
        {
            //if ((IsSystemObject && IsCurrentUserInRole("Administrators") && !DomainService.Instance.IsSuperAdmin) || (DomainService.Instance.ConnectionString.Contains("PrimeThePump") && !DomainService.Instance.IsSuperAdmin))
            //{
            //   throw new UserFriendlyException("You cannot delete a SYSTEM object.");
            //}
            base.OnDeleting();
        }
        #endregion

        #region Methods
        public virtual void SetUniqueID(Guid guid)
        {
            oid = guid;
        }
        public override string ToString()
        {
            return GetDisplayText();
        }

        public virtual string GetDisplayText()
        {
            if (!BaseObject.IsXpoProfiling)
            {
                if (!isDefaultPropertyAttributeInit)
                {
                    string defaultPropertyName = string.Empty;
                    XafDefaultPropertyAttribute xafDefaultPropertyAttribute = XafTypesInfo.Instance.FindTypeInfo(GetType()).FindAttribute<XafDefaultPropertyAttribute>();
                    if (xafDefaultPropertyAttribute != null)
                        defaultPropertyName = xafDefaultPropertyAttribute.Name;
                    else
                    {
                        DefaultPropertyAttribute defaultPropertyAttribute = XafTypesInfo.Instance.FindTypeInfo(GetType()).FindAttribute<DefaultPropertyAttribute>();
                        if (defaultPropertyAttribute != null)
                            defaultPropertyName = defaultPropertyAttribute.Name;
                    }
                    if (!string.IsNullOrEmpty(defaultPropertyName))
                        defaultPropertyMemberInfo = ClassInfo.FindMember(defaultPropertyName);

                    isDefaultPropertyAttributeInit = true;
                }
                if (defaultPropertyMemberInfo != null)
                {
                    object obj = defaultPropertyMemberInfo.GetValue(this);
                    if (obj != null)
                        return obj.ToString();

                }
            }
            return base.ToString();
        }
        #endregion

        #region IActivatedObject Members

        private bool _IsActive;
        [VisibleInDetailView(false),VisibleInListView(false)]
        [ImagesForBoolValues("ImageForTrue_16x16", "ImageForFalse_16x16")]
        [CaptionsForBoolValues("TRUE", "FALSE")]
        public bool IsActive {
            get => _IsActive;
            set => SetPropertyValue("IsActive", ref _IsActive, value);
        }

        void IActivatedObject.Activate()
        {
            IsActive = true;
        }

        void IActivatedObject.Deactivate()
        {
            IsActive = false;
        }
        #endregion

    }
}
