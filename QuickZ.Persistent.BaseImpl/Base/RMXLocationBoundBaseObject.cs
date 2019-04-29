using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using EDS.RentalManager.Express.Module.BusinessObjects;
using QuickZ.Persistent.Base.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDS.RentalManager.Express.Module.BusinessObjects.Common
{
    public interface IRMXOrganizationBoundObject
    {
        RMXOrganizationBase Organization { get; }
        string OrganizationName { get; }
        void SetOrganization(Guid organization);
        //bool IsCurrentUserAnAdministrator { get; }
    }
    public interface IRMXLocationBoundObject
    {
        RMXLocationCategory HomeLocation { get; }
        string LocationName { get; }
        void SetLocation(Guid location);
        void SetLocationName(string locationName);

        //bool IsCurrentUserAnAdministrator { get; }
    }

    public interface IRMXHasMultipleLocationsObject
    {
        XPCollection<RMXLocationCategory> FilteredLocations { get; }
        XPCollection<RMXLocationCategory> Locations { get; }
    }

    public interface IRMXHasMultipleOrganizationsObject
    {
        //XPCollection<BusinessEngine> FilteredOrganizations { get; }
        XPCollection<RMXOrganizationBase> Organizations { get; }
    }


    [Appearance("RMXBaseObjectDisableOrganizationAndLocation", AppearanceItemType = "ViewItem", Enabled = false,
         TargetItems = "Organization,Location,OrganizationName,LocationName", Criteria = "NOT CurrentUserIsAdministrator()", Context = "DetailView",
             FontColor = "Blue")]
    [NonPersistent]
    [DevExpress.ExpressApp.DC.XafDefaultProperty(nameof(LocationName))]
    public abstract class RMXLocationBoundBaseObject : RMXBaseObject, IRMXOrganizationBoundObject, IRMXLocationBoundObject
    {
        public RMXLocationBoundBaseObject(Session session)
            : base(session)
        {

        }

        public RMXLocationBoundBaseObject(Session session, Guid oidOverride)
            : base(session, oidOverride)
        {

        }

        public virtual void UpdateOrganizationAndLocation()
        {
            // --- Update Organization
            if (Organization == null || String.IsNullOrEmpty(Organization?.OrganizationName))
                SetOrganization(DomainService.Instance.ActiveOrganizationId);

            // --- Update Location
            if (HomeLocation == null || String.IsNullOrEmpty(HomeLocation?.LocationName))
                SetLocation(DomainService.Instance.ActiveLocationId);
        }

#if RELEASE
        [Browsable(false)]
#endif
        [DevExpress.Persistent.Validation.RuleRequiredField]
        [ImmediatePostData]
        [VisibleInDetailView(false),
        VisibleInListView(false), VisibleInLookupListView(false)]
        public abstract RMXOrganizationBase Organization { get; set; }

#if RELEASE
        [Browsable(false)]
#endif
        [DevExpress.Persistent.Validation.RuleRequiredField]
        [VisibleInDetailView(false),
        VisibleInListView(false), VisibleInLookupListView(false)]
        [Persistent("Location")]
        public abstract RMXLocationCategory HomeLocation { get; set; }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            UpdateOrganizationAndLocation();
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            UpdateOrganizationAndLocation();
        }

        //   private bool _IsCurrentUserAnAdministrator;
        private string _OrganizationName;
        /// <summary>
        /// Used for generating big data reports and analytics
        /// </summary>
        //#if !DEBUG
        //       //[Browsable(false)]
        //#endif
        [VisibleInDetailView(false),
        VisibleInListView(false), VisibleInLookupListView(false)]
        public string OrganizationName
        {
            get
            {
                return _OrganizationName;
            }
            //#if !DEBUG
            //            private set
            //#else
            //           
            //#endif
            set
            {
                SetPropertyValue("OrganizationName", ref _OrganizationName, value);
            }
        }

        private string _LocationName;
        /// <summary>
        /// Used for generating big data reports and analytics
        /// </summary>
        //#if !DEBUG
        //       // [Browsable(false)]
        //#endif
        // [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        [Appearance("DisableLocationName", Enabled = false)]
        public string LocationName
        {
            get
            {
                return _LocationName;
            }
            //#if !DEBUG
            /// ----Jomar: commented this part due to it causes the LocationName not being able to display it's name instead it will display it's Group Count
            ///private set 
            set
            {
                SetPropertyValue("LocationName", ref _LocationName, value);
            }
        }

        public void SetOrganization(Guid organizationOid)
        {
            if (organizationOid == null)
            {
                DomainService.Instance.LogValue("RMXLocationBoundBaseObject.SetOrganization(organization)", organizationOid);
                return;
            }
            var newOrganization = Session.GetObjectByKey<RMXOrganizationBase>(organizationOid);
            if (newOrganization != null)
            {
                Organization = newOrganization;

                if (OrganizationName != newOrganization.OrganizationName)
                    OrganizationName = newOrganization.OrganizationName;
            }
        }
        public void SetLocation(Guid locationOid)
        {
            if (locationOid == null)
            {
                DomainService.Instance.LogValue("RMXLocationBoundBaseObject.SetOrganization(location)", locationOid);
                return;
            }

            var newHomeLocation = Session.GetObjectByKey<RMXLocationCategory>(locationOid);
            if (newHomeLocation != null)
            {
                HomeLocation = newHomeLocation;

                if (LocationName != newHomeLocation.Name)
                    LocationName = newHomeLocation.Name;

                // --- Make sure that Organization is set to New Location's Organization
                if (newHomeLocation.Organization.Oid != Organization?.Oid)
                    SetOrganization(newHomeLocation.Organization.Oid);
            }
        }

        public void SetLocationName(string locationName)
        {
            LocationName = locationName;
        }
    }
}
