using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using QuickZ.Persistent.Base.Documents;
using QuickZ.Persistent.Base.Mails;
using QuickZ.Persistent.Base.Core;
using QuickZ.Persistent.BaseImpl;
using System.IO;


namespace QuickZ.Persistent.BaseImpl.Mailings
{
	//[NonPersistent]
	[MapInheritance(MapInheritanceType.ParentTable)]
    [ImageName("BOMailMergeRegularTemplateBase")]
    [DevExpress.ExpressApp.DC.XafDefaultProperty(nameof(Subject))]
    public abstract class MailMergeRegularTemplateBase : MailMergeTemplateBase, IEmail, IMailMergeEmailTarget, ITemplateWithAttachments, IObjectWithMailMergeTemplate
    {
		public MailMergeRegularTemplateBase(DevExpress.Xpo.Session session)
			: base(session)
		{

		}

		public override void AfterConstruction()
		{
			base.AfterConstruction();
			OwnerType = BusinessDataOwnership.System;
		}

        bool isIncludeVehicleAttachments;
        [VisibleInListView(false)]
        public bool IsIncludeVehicleAttachments {
            get {
                return isIncludeVehicleAttachments;
            }
            set {
                SetPropertyValue(nameof(IsIncludeVehicleAttachments), ref isIncludeVehicleAttachments, value);
            }
        }

        bool isIncludeVehicleClassAttachments;
        [VisibleInListView(false)]
        public bool IsIncludeVehicleClassAttachments {
            get {
                return isIncludeVehicleClassAttachments;
            }
            set {
                SetPropertyValue(nameof(IsIncludeVehicleClassAttachments), ref isIncludeVehicleClassAttachments, value);
            }
        }

        bool isIncludeRentalFeesAttachments;
        [VisibleInListView(false)]
        public bool IsIncludeRentalFeesAttachments {
            get {
                return isIncludeRentalFeesAttachments;
            }
            set {
                SetPropertyValue(nameof(IsIncludeRentalFeesAttachments), ref isIncludeRentalFeesAttachments, value);
            }
        }
        //bool isIncludeRentalFeesAttachements;
        //[VisibleInListView(false)]
        //public bool IsIncludeRentalFeesAttachements {
        //    get {
        //        return isIncludeRentalFeesAttachements;
        //    }
        //    set {
        //        SetPropertyValue("IsIncludeRentalFeesAttachements", ref isIncludeRentalFeesAttachements, value);
        //    }
        //}

        bool isIncludeRentalEquipmentAttachments;
        [VisibleInListView(false)]
        public bool IsIncludeRentalEquipmentAttachments {
            get {
                return isIncludeRentalEquipmentAttachments;
            }
            set {
                SetPropertyValue(nameof(IsIncludeRentalEquipmentAttachments), ref isIncludeRentalEquipmentAttachments, value);
            }
        }

        private string _Subject;
		[NonPersistent]
		public string Subject
		{
			get { return _Subject; }
			set { _Subject = value; }
		}

		private string _Body;
		[NonPersistent]
		public string Body
		{
			get { return _Body; }
			set { _Body = value; }
		}

        private string _BodyHtml;
        [Browsable(false)]
        [NonPersistent]
        public string BodyHtml
        {
            get { return _BodyHtml; }
            set { _BodyHtml = value; }
        }

        private string _From;
		//[NonPersistent]
		public string From
		{
			get
			{
				return _From;
			}
			set
			{
				_From = value;
			}
		}
		private string _To;
		//[NonPersistent]
		public string To
		{
			get
			{
				return _To;
			}
			set
			{
				_To = value;
			}
		}
		private string _CC;
		//[NonPersistent]
		public string CC
		{
			get
			{
				return _CC;
			}
			set
			{
				_CC = value;
			}
		}
		private string _Bcc;
		//[NonPersistent]
		public string BCC
		{
			get
			{
				return _Bcc;
			}
			set
			{
				_Bcc = value;
			}
		}
		[Browsable(false)]
		IList ITemplateWithAttachments.Attachments
		{
			get { return Attachments  as IList; }
		}

		[ImmediatePostData]
		[Association("MailMergeRegularTemplates-AdditionalDocuments")]
		public XPCollection<BusinessDocumentData> Attachments
		{
			get
			{
                return GetCollection<BusinessDocumentData>("Attachments");
			}
		}


	}
}
