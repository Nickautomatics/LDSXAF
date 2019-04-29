using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Security;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using System;
using System.Linq;
using QuickZ.Persistent.BaseImpl.Security;
using QuickZ.Persistent.Base.Core;

namespace EDS.RentalManager.Express.Module.BusinessObjects.Common
{
    public enum MailServerPriority
    {
        Company = 1,
        Location = 2
    }

    public enum AccountType
    {
        SuperAdministrator = 1, // A Super Administrator from EMX
        User = 0
    }
    public partial class RMXOrganizationBase
    {
        //public RMXOrganization(Session session, string rootOrganizationName, string rootOrganizationCode) : this(session)
        //{
        //    _OrganizationName = rootOrganizationName;
        //    _SystemID = rootOrganizationCode;
        //}

        #region Methods
        public virtual string GetSequenceId()
        {
            SequenceId = DevExpress.Persistent.BaseImpl
                        .DistributedIdGeneratorHelper
                        .Generate(this.Session.DataLayer, this.GetType().FullName, string.Empty) + 1;
            return String.Format("{0:D6}", SequenceId);
        }
        #endregion

        #region XPO Overrides

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            // --- Generate Sequence Object
            if (String.IsNullOrEmpty(FriendlyId) || FriendlyId == "0")
                FriendlyId = GetSequenceId();

            //// --- Because this is an Organization, it's Organization property must be set to itself
            //SetOrganization(this);
            LocationType = RMXLocationTypeEnum.Company;
            // --- Set default to Active
            IsActive = true;

        }
        protected override void OnSaving()
        {
            base.OnSaving();

            //if (Organization == null)
            //    SetOrganization(this);

            //if (SequenceId == 0)
            //    GetSequenceId();
        }
        protected override void OnLoaded()
        {
            base.OnLoaded();

            if (LocationType != RMXLocationTypeEnum.Company)
                LocationType = RMXLocationTypeEnum.Company;
        }



        public override void UpdateOrganization()
        {
            // --- Because this is an Organization, it's Organization property must be set to itself
            SetOrganization(this.Oid);
        }

        #endregion


        #region Variables and Constants
        private DateTime? _DefaultPickupTime;
        private DateTime? _DefaultDropOffTime;

        public RMXOrganizationBase(Session session, string name)
            : base(session, name)
        {

        }

        //public const string DateAndTimeOfDayEditMask = "MM/dd/yyyy hh:mm tt";


        #endregion

        #region Methods

        protected override void OnDeleting()
        {
            if (IsSystemObject)
                throw new UserFriendlyException("You cannot delete a SYSTEM object. Please contact your Administrator for assistance.");

            base.OnDeleting();
        }

        protected override void OnSaved()
        {
            base.OnSaved();

            if (!String.IsNullOrEmpty(IncomingPassword))
                CEM.Encrypt(IncomingPassword);
            if (!String.IsNullOrEmpty(OutgoingPassword))
                CEM.Encrypt(OutgoingPassword);
            if (DomainService.Instance.ActiveOrganizationId == Oid)
                DomainService.Instance.CompanyWebsiteURL = CompanyWebsiteURL;

        }

        #endregion

    }
}
