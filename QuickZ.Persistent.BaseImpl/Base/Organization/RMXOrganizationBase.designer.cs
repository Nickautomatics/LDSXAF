using System;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System.Drawing;
using DevExpress.Xpo.Metadata;
using System.ComponentModel;
using DevExpress.Persistent.Validation;
using QuickZ.BusinessEmail;
using QuickZ.Persistent.Base.General;
using EDS.ExpressApp.Common;

namespace EDS.RentalManager.Express.Module.BusinessObjects.Common {
    public interface ISmartyStreetsAuth {
        bool IsAllowSmartyStreets { get; set; }
        string SmartyStreetsAuthorizationId { get; set; }
        string SmartyStreetsAuthorizationToken { get; set; }
    }

    public interface IOrganization {
        Guid Oid { get; }
        byte[] OrganizationLogo { get; set; }
        string OrganizationName { get; set; }
        string OrganizationAddress { get; set; }
        string OrganizationWebsiteURL { get; set; }
        string OrganizationEmail { get; set; }
        bool IsActive { get; set; }
        void SetUniqueID(Guid guid);
        bool IsSystemObject { get; set; }
    }

    public enum OrganizationTypeEnum {
        Individual = 0,
        Organization = 1,
        Unknown = 9999
    }

    [NavigationItem(false),//[NavigationItem(true, GroupName = "Teacher"),"Organization"),
    CreatableItem(false)]
    [Persistent("Company")]
    [DevExpress.ExpressApp.DC.XafDisplayName("Company")]
    [ImageName("NavCompanyProfile")]
    [DevExpress.ExpressApp.DC.XafDefaultProperty("OrganizationName")]
    public abstract partial class RMXOrganizationBase : RMXLocationCategory, IMailServerSettings, IOrganization, ISmartyStreetsAuth, QuickZ.Persistent.Base.IRefreshWebSiteObject
    {
        #region Constructors

        public RMXOrganizationBase(Session session)
            : base(session) {
        }
        public RMXOrganizationBase(Session session, Guid oidOverride)
            : base(session, oidOverride) {

        }

        #endregion

        #region Persistent Properties
        private string _OrganizationName;
        [EditorAlias(CustomEditorAliases.CustomSkinEditor)]
        [ImmediatePostData]
        public String Skin {
            get => GetPropertyValue<String>(nameof(Skin));
            set => SetPropertyValue(nameof(Skin), value);
        }

        private bool _IsShowSkinEditor;
        [ImagesForBoolValues("ImageForTrue_16x16", "ImageForFalse_16x16")]
        [CaptionsForBoolValues("TRUE", "FALSE")]
        public bool IsShowSkinEditor {
            get => _IsShowSkinEditor;
            set => SetPropertyValue(nameof(IsShowSkinEditor), ref _IsShowSkinEditor, value);
        }

        //[ImageEditor]
        //[Size(SizeAttribute.Unlimited), ValueConverter(typeof(ImageValueConverter)),Delayed(true)]
        //[ImmediatePostData]
        //public Image OrganizationLogo {
        //    get => GetPropertyValue<Image>(nameof(OrganizationLogo));
        //    set => SetPropertyValue<Image>(nameof(OrganizationLogo), value);
        //}
        [DevExpress.ExpressApp.DC.XafDisplayName("Glyph")]
        [Delayed(true), VisibleInListViewAttribute(false)]
        [Size(SizeAttribute.Unlimited), ImmediatePostData]
        [ImageEditor(ListViewImageEditorMode = ImageEditorMode.PictureEdit, DetailViewImageEditorMode = ImageEditorMode.PictureEdit, ListViewImageEditorCustomHeight = 40)]
        public byte[] OrganizationLogo
        {
            get { return GetDelayedPropertyValue<byte[]>(nameof(OrganizationLogo)); }
            set { SetDelayedPropertyValue<byte[]>(nameof(OrganizationLogo), value); }
        }
        private string _OrganizationAddress;
        public string OrganizationAddress {
            get => _OrganizationAddress;
            set => SetPropertyValue(nameof(OrganizationAddress), ref _OrganizationAddress, value);
        }

        private string _OrganizationWebsiteURL;
        public string OrganizationWebsiteURL {
            get { return _OrganizationWebsiteURL; }
            set { SetPropertyValue(nameof(OrganizationWebsiteURL), ref _OrganizationWebsiteURL, value); }
        }

        private string _OrganizationEmail;
        public string OrganizationEmail {
            get { return _OrganizationEmail; }
            set { SetPropertyValue(nameof(OrganizationEmail), ref _OrganizationEmail, value); }
        }
        //XPCollection<RMXLocationCategory> _Location = null;
        ////[Association("Organization-Locations")]
        //public XPCollection<RMXLocationCategory> Locations
        //{
        //    get
        //    {
        //        if(_Location == null)
        //               _Location= new XPCollection<RMXLocationCategory>(Session, DevExpress.Data.Filtering.CriteriaOperator.Parse("Organization.Oid = ?", this.Oid)); // GetCollection<RMXLocationCategory>("Locations");
        //        return _Location;
        //    }
        //}

        private bool _IsPublishNewObjectsByDefault;
        /// <summary>
        /// When enabled, AccessStatus is always set to Published. Otherwise, it's Draft.
        /// </summary>
        [ImagesForBoolValues("ImageForTrue_16x16", "ImageForFalse_16x16")]
        [CaptionsForBoolValues("TRUE", "FALSE")]
        public bool IsPublishNewObjectsByDefault {
            get {
                return _IsPublishNewObjectsByDefault;
            }
            set {
                SetPropertyValue(nameof(IsPublishNewObjectsByDefault), ref _IsPublishNewObjectsByDefault, value);
            }
        }

        bool isLocationsPrivate;
        [ImagesForBoolValues("ImageForTrue_16x16", "ImageForFalse_16x16")]
        [CaptionsForBoolValues("TRUE", "FALSE")]
        public bool IsLocationsPrivate {
            get => isLocationsPrivate;
            set => SetPropertyValue(nameof(IsLocationsPrivate), ref isLocationsPrivate, value);
        }

        #endregion

        #region Persistent Properties
        
        
        // private string _CompanyEmail;
        //private string _CompanyWebsiteURL;
        //private string _CompanyAddress;
        //private string _CompanyName;

        private bool _CalcCheckInFromActive;
        [ImagesForBoolValues("ImageForTrue_16x16", "ImageForFalse_16x16")]
        [CaptionsForBoolValues("TRUE", "FALSE")]
        public bool CalcCheckInFromActive {
            get => _CalcCheckInFromActive;
            set => SetPropertyValue(nameof(CalcCheckInFromActive), ref _CalcCheckInFromActive, value);
        }

        [ImageEditor]
        [Size(SizeAttribute.Unlimited), ValueConverter(typeof(ImageValueConverter))]
        [Delayed(true)]
        [ImmediatePostData]
        public Image CompanyLogo {
            get => GetPropertyValue<Image>(nameof(CompanyLogo));
            set => SetPropertyValue<Image>(nameof(CompanyLogo), value);
        }
        //[DevExpress.ExpressApp.DC.XafDisplayName("Glyph")]
        //[Delayed(), VisibleInListViewAttribute(false)]
        //[Size(SizeAttribute.Unlimited), ImmediatePostData]
        //[ImageEditor(ListViewImageEditorMode = ImageEditorMode.PictureEdit, DetailViewImageEditorMode = ImageEditorMode.PictureEdit, ListViewImageEditorCustomHeight = 40)]
        //public byte[] OrganizationLogo
        //{
        //    get { return GetDelayedPropertyValue<byte[]>(nameof(OrganizationLogo)); }
        //    set { SetDelayedPropertyValue<byte[]>(nameof(OrganizationLogo), value); }
        //}
        //[DevExpress.ExpressApp.DC.XafDisplayName("Glyph")]
        //[Delayed(), VisibleInListViewAttribute(false)]
        //[Size(SizeAttribute.Unlimited), ImmediatePostData]
        //[ImageEditor(ListViewImageEditorMode = ImageEditorMode.PictureEdit, DetailViewImageEditorMode = ImageEditorMode.PictureEdit, ListViewImageEditorCustomHeight = 40)]
        //public byte[] OrganizationLogo
        //{
        //    get { return GetDelayedPropertyValue<byte[]>(nameof(OrganizationLogo)); }
        //    set { SetDelayedPropertyValue<byte[]>(nameof(OrganizationLogo), value); }
        //}
        [ImageEditor]
        [Size(SizeAttribute.Unlimited), ValueConverter(typeof(ImageValueConverter))]
        [Delayed(true)]
        [ImmediatePostData]
        public Image LogoColor {
            get => GetPropertyValue<Image>(nameof(LogoColor));
            set => SetPropertyValue<Image>(nameof(LogoColor), value);
        }

        [NonPersistent]
        [RuleRequiredField("RuleRequiredField for RMXOragnizationBase.CompanyName", DefaultContexts.Save, "A CompanyName must be specified", ResultType = ValidationResultType.Error)]
        public string CompanyName {
            get => OrganizationName;
            //#if !DEBUG
            //            private set
            //#else
            set => OrganizationName = value;
        }

        private string _CompanyNameAcronym;
        [Size(16)]
        public string CompanyNameAcronym {
            get => _CompanyNameAcronym;
            set => SetPropertyValue(nameof(CompanyNameAcronym), ref _CompanyNameAcronym, value);
        }

        [NonPersistent]
        public string CompanyAddress {
            get => OrganizationAddress;
            set => OrganizationAddress = value;
        }

        [NonPersistent]
        public string CompanyWebsiteURL {
            get => OrganizationWebsiteURL;
            set => OrganizationWebsiteURL = value;
        }
        
        public string CompanyEmail => EmailAddress;

        private string _CompanyPhone;
        [Size(32)]
        public string CompanyPhone {
            get => _CompanyPhone;
            set => SetPropertyValue(nameof(CompanyPhone), ref _CompanyPhone, value);
        }

        [VisibleInListView(true)]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", ConstantResources.DefaultDateAndTimeOfDayEditMask)]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", ConstantResources.DefaultDateAndTimeOfDayEditMask)]
        [EditorAlias(ExpressApp.Common.CustomEditorAliases.CustomDateTimeEditor)]
        [ToolTip("Specify a date and time of day in the following format: " + ConstantResources.DefaultDateAndTimeOfDayEditMask, null, DevExpress.Utils.ToolTipIconType.Information)]
        public DateTime? DefaultDropOffTime {
            get => _DefaultDropOffTime;
            set => SetPropertyValue(nameof(DefaultDropOffTime), ref _DefaultDropOffTime, value);
        }

        [VisibleInListView(true)]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", ConstantResources.DefaultDateAndTimeOfDayEditMask)]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", ConstantResources.DefaultDateAndTimeOfDayEditMask)]
        [EditorAlias(ExpressApp.Common.CustomEditorAliases.CustomDateTimeEditor)]
        [ToolTip("Specify a date and time of day in the following format: " + ConstantResources.DefaultDateAndTimeOfDayEditMask, null, DevExpress.Utils.ToolTipIconType.Information)]
        public DateTime? DefaultPickupTime {
            get => _DefaultPickupTime;
            set => SetPropertyValue(nameof(DefaultPickupTime), ref _DefaultPickupTime, value);
        }

        #endregion

        #region Properties
        
        private Boolean isCoreDataCreated;
        [ImagesForBoolValues("ImageForTrue_16x16", "ImageForFalse_16x16")]
        [CaptionsForBoolValues("TRUE", "FALSE")]
        public Boolean IsCoreDataCreated {
            get => isCoreDataCreated;
            set => SetPropertyValue(nameof(IsCoreDataCreated), ref isCoreDataCreated, value);
        }

        private Boolean isLoginDataCreated;
        [ImagesForBoolValues("ImageForTrue_16x16", "ImageForFalse_16x16")]
        [CaptionsForBoolValues("TRUE", "FALSE")]
        public Boolean IsSampleUsersCreated {
            get => isLoginDataCreated;
            set => SetPropertyValue(nameof(IsSampleUsersCreated), ref isLoginDataCreated, value);
        }

        private Boolean isLookupDataCreated;
        [ImagesForBoolValues("ImageForTrue_16x16", "ImageForFalse_16x16")]
        [CaptionsForBoolValues("TRUE", "FALSE")]
        public Boolean IsLookupDataCreated {
            get => isLookupDataCreated;
            set => SetPropertyValue(nameof(IsLookupDataCreated), ref isLookupDataCreated, value);
        }

        private Boolean isSampleDataCreated;
        [ImagesForBoolValues("ImageForTrue_16x16", "ImageForFalse_16x16")]
        [CaptionsForBoolValues("TRUE", "FALSE")]
        public Boolean IsSampleDataCreated {
            get => isSampleDataCreated;
            set => SetPropertyValue(nameof(IsSampleDataCreated), ref isSampleDataCreated, value);
        }

        private Boolean isStartUpUserCreated;
        [ImagesForBoolValues("ImageForTrue_16x16", "ImageForFalse_16x16")]
        [CaptionsForBoolValues("TRUE", "FALSE")]
        public Boolean IsStartUpUserCreated {
            get => isStartUpUserCreated;
            set => SetPropertyValue(nameof(IsStartUpUserCreated), ref isStartUpUserCreated, value);
        }
        private DateTime? coreDataCreatedDate;
        [ImagesForBoolValues("ImageForTrue_16x16", "ImageForFalse_16x16")]
        [CaptionsForBoolValues("TRUE", "FALSE")]
        public DateTime? CoreDataCreatedDate {
            get => coreDataCreatedDate;
            set => SetPropertyValue(nameof(CoreDataCreatedDate), ref coreDataCreatedDate, value);
        }

        private DateTime? loginDataCreatedDate;
        public DateTime? SampleUsersCreatedDate {
            get => loginDataCreatedDate;
            set => SetPropertyValue(nameof(SampleUsersCreatedDate), ref loginDataCreatedDate, value);
        }
        private DateTime? lookupDataCreatedDate;
        public DateTime? LookupDataCreatedDate {
            get => lookupDataCreatedDate;
            set => SetPropertyValue(nameof(LookupDataCreatedDate), ref lookupDataCreatedDate, value);
        }
        private DateTime? sampleDataCreatedDate;
        public DateTime? SampleDataCreatedDate {
            get => sampleDataCreatedDate;
            set => SetPropertyValue(nameof(SampleDataCreatedDate), ref sampleDataCreatedDate, value);
        }

        private DateTime? startUpUserCreatedDate;
        public DateTime? StartUpUserCreatedDate {
            get => startUpUserCreatedDate;
            set => SetPropertyValue(nameof(StartUpUserCreatedDate), ref startUpUserCreatedDate, value);
        }

        #endregion

        #region IMailServerSettings Properties

        private MailServerPriority _MailServerPriority;
        public MailServerPriority MailServerPriority {
            get => _MailServerPriority;
            set => SetPropertyValue(nameof(MailServerPriority), ref _MailServerPriority, value);
        }

        [RuleRegularExpression(DefaultContexts.Save, ConstantResources.EmailRegularExpression, CustomMessageTemplate = "Invalid email format!")]
        public string EmailAddress {
            get {
                if (String.IsNullOrEmpty(OutgoingUserName) && !String.IsNullOrEmpty(IncomingUserName))
                    _OutgoingUserName = IncomingUserName;
                //else
                //    OutgoingUserName = "no email";
                return OutgoingUserName; ;
            }
            set => OutgoingUserName = value;
        }

        private string _IncomingUserName;
        public string IncomingUserName {
            get => _IncomingUserName;
            set => SetPropertyValue(nameof(IncomingUserName), ref _IncomingUserName, value);
        }

        private string _IncomingPassword;
        [Size(32)]
        [PasswordPropertyText(true)]
        public string IncomingPassword {
            get => _IncomingPassword;
            set => SetPropertyValue(nameof(IncomingPassword), ref _IncomingPassword, value);
        }

        private string _IncomingServerName;
        public string IncomingServerName {
            get => _IncomingServerName;
            set => SetPropertyValue(nameof(IncomingServerName), ref _IncomingServerName, value);
        }

        private int _IncomingServerPort;
        public int IncomingServerPort {
            get => _IncomingServerPort;
            set => SetPropertyValue(nameof(IncomingServerPort), ref _IncomingServerPort, value);
        }

        private QuickZ.Persistent.Base.Mails.MailServerType _IncomingServerType;
        public QuickZ.Persistent.Base.Mails.MailServerType IncomingServerType {
            get => _IncomingServerType;
            set => SetPropertyValue(nameof(IncomingServerType), ref _IncomingServerType, value);
        }

        private string _OutgoingUserName;
        public string OutgoingUserName {
            get => _OutgoingUserName;
            set => SetPropertyValue(nameof(OutgoingUserName), ref _OutgoingUserName, value);
        }

        private string _OutgoingPassword;
        [Size(32)]
        [PasswordPropertyText(true)]
        public string OutgoingPassword {
            get => _OutgoingPassword;
            set => SetPropertyValue(nameof(OutgoingPassword), ref _OutgoingPassword, value);
        }

        private string _OutgoingServerName;
        public string OutgoingServerName {
            get => _OutgoingServerName;
            set => SetPropertyValue(nameof(OutgoingServerName), ref _OutgoingServerName, value);
        }

        private int _OutgoingServerPort;
        public int OutgoingServerPort {
            get => _OutgoingServerPort;
            set => SetPropertyValue(nameof(OutgoingServerPort), ref _OutgoingServerPort, value);
        }

        string eLInstallLocation;
        public string ELInstallLocation {
            get => eLInstallLocation;
            set => SetPropertyValue(nameof(ELInstallLocation), ref eLInstallLocation, value);
        }

        #endregion

        #region ISmartyStreetsAuth Properties
        bool isAllowSmartyStreets;
        [ImagesForBoolValues("ImageForTrue_16x16", "ImageForFalse_16x16")]
        [CaptionsForBoolValues("TRUE", "FALSE")]
        public bool IsAllowSmartyStreets {
            get => isAllowSmartyStreets;
            set => SetPropertyValue(nameof(IsAllowSmartyStreets), ref isAllowSmartyStreets, value);
        }
        string smartyStreetsAuthorizationId;
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string SmartyStreetsAuthorizationId {
            get => smartyStreetsAuthorizationId;
            set => SetPropertyValue(nameof(SmartyStreetsAuthorizationId), ref smartyStreetsAuthorizationId, value);
        }
        string smartyStreetsAuthorizationToken;
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string SmartyStreetsAuthorizationToken {
            get => smartyStreetsAuthorizationToken;
            set => SetPropertyValue(nameof(SmartyStreetsAuthorizationToken), ref smartyStreetsAuthorizationToken, value);
        }

        string smartyStreetsWebsiteKey;
        [Size(50)]
        public string SmartyStreetsWebsiteKey {
            get => smartyStreetsWebsiteKey;
            set => SetPropertyValue(nameof(SmartyStreetsWebsiteKey), ref smartyStreetsWebsiteKey, value);
        }
        #endregion

        public static XPCollection<RMXOrganizationBase> GetOrganizations(Session session) {
            return new XPCollection<RMXOrganizationBase>(session, null, new SortProperty(nameof(OrganizationName), DevExpress.Xpo.DB.SortingDirection.Ascending));
        }

    }
}
