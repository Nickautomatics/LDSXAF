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
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.ReportsV2;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;

using DevExpress.ExpressApp;
using DevExpress.XtraReports.UI;
using QuickZ.Persistent.BaseImpl;
using QuickZ.Persistent.Base.Documents;
using DevExpress.Persistent.BaseImpl;
using EDS.RentalManager.Express.Module.BusinessObjects;

namespace QuickZ.Persistent.BaseImpl
{
    [DevExpress.ExpressApp.DC.DomainComponent]
    [NavigationItem(false), CreatableItem(true)]
    [DevExpress.ExpressApp.DC.XafDefaultProperty(nameof(DocumentName))]
    //[ImageName("BO_List")]
    [ImageName("BOBusinessDocumentReport")]
    [DevExpress.ExpressApp.DC.XafDisplayName("Business Report")]
    [MapInheritance(MapInheritanceType.ParentTable)]
    public partial class BusinessDocumentReport : BusinessDocumentData, IReportDataV2Writable, IInplaceReportV2, IBusinessDocument, IBusinessDocumentReport, ITagAsSystemObject
    {
		// Fields...
		private string _Category;
		//[VisibleInDetailView(false)]
		public string Category
		{
			get
			{
				return _Category;
			}
			set
			{
				SetPropertyValue(nameof(Category), ref _Category, value);
			}
		}

        private string displayName = "";
		private bool isInplaceReport = false;
#if MediumTrust
		private string dataTypeName = string.Empty;
#else
		[Persistent("ObjectTypeName"), Size(512)]
		private string dataTypeName = string.Empty;
#endif
		private string parametersObjectTypeName = string.Empty;
		private Type predefinedReportType;
		[Delayed(true), Persistent(nameof(Content)), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public byte[] Content
		{
			get
			{
				return GetDelayedPropertyValue<byte[]>(nameof(Content));
			}
			set
			{
				if (((IReportDataV2)this).IsPredefined)
				{
					throw new NotImplementedException();
				}
				SetDelayedPropertyValue<byte[]>(nameof(Content), value);
			}
		}
		protected override void OnSaving()
		{
			if (String.IsNullOrEmpty(displayName) || (displayName.Trim() == ""))
			{
				throw new UserFriendlyException(ReportsModuleV2.GetEmptyDisplayNameErrorMessage());
			}

            DocumentName = DisplayName;

			base.OnSaving();
		}
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public BusinessDocumentReport(Session session, Type dataType)
			: base(session)
		{
			Guard.ArgumentNotNull(dataType, "dataType");
			this.dataTypeName = dataType.FullName;
		}
		[Persistent("Name")]
		public string DisplayName
		{
			get { return displayName; }
			set { SetPropertyValue(nameof(DisplayName), ref displayName, value); }
		}

#if MediumTrust
		[Browsable(false)]
		[Persistent("ObjectTypeName")]
		public string DataTypeName {
			get { return dataTypeName; }
			set { SetPropertyValue("ObjectTypeName", ref dataTypeName, value); }
		}
#else
		[Browsable(false)]
		[PersistentAlias("dataTypeName")]
		public string DataTypeName
		{
			get { return dataTypeName; }
		}
#endif
		[SettingsBindable(true)]
		[VisibleInListView(false)]
		[TypeConverter(typeof(ReportParametersObjectTypeConverter))]
		[Localizable(true)]
		public Type ParametersObjectType
		{
			get
			{
				if (!string.IsNullOrEmpty(ParametersObjectTypeName))
				{
					ITypeInfo typeInfo = XafTypesInfo.Instance.FindTypeInfo(ParametersObjectTypeName);
					if (typeInfo != null)
					{
						return typeInfo.Type;
					}
				}
				return null;
			}
			set
			{
				((IReportDataV2Writable)this).SetParametersObjectType(value);
			}
		}
		[Size(512)]
		[Browsable(false)]
		public string ParametersObjectTypeName
		{
			get { return parametersObjectTypeName; }
			set { SetPropertyValue(ReportsModuleV2.ParametersObjectTypeNameMemberName, ref parametersObjectTypeName, value); }
		}
		[NonPersistent, System.ComponentModel.DisplayName("Data Type")]
		public string DataTypeCaption
		{
			get { return CaptionHelper.GetClassCaption(dataTypeName); }
		}
		Type IReportDataV2.DataType
		{
			get
			{
				if (!string.IsNullOrEmpty(DataTypeName))
				{
					ITypeInfo typeInfo = XafTypesInfo.Instance.FindTypeInfo(DataTypeName);
					if (typeInfo != null)
					{
						return typeInfo.Type;
					}
				}
				return null;
			}
		}
		[VisibleInListView(false)]
		public bool IsInplaceReport
		{
			get { return isInplaceReport; }
			set { SetPropertyValue(ReportsModuleV2.IsInplaceReportMemberName, ref isInplaceReport, value); }
		}
		[Browsable(false)]
		[ValueConverter(typeof(TypeToStringConverter))]
		[Size(512)]
		public Type PredefinedReportType
		{
			get { return predefinedReportType; }
			set { SetPropertyValue(ReportsModuleV2.PredefinedReportTypeMemberName, ref predefinedReportType, value); }
		}
		void IReportDataV2Writable.SetContent(byte[] content)
		{
			Content = content;
		}
		void IReportDataV2Writable.SetPredefinedReportType(Type reportType)
		{
			if (reportType != null)
			{
				Guard.TypeArgumentIs(typeof(XtraReport), reportType, "reportType");
			}
			PredefinedReportType = reportType;
		}
		void IReportDataV2Writable.SetParametersObjectType(Type parametersObjectType)
		{
			if (parametersObjectType != null)
			{
				Guard.TypeArgumentIs(typeof(ReportParametersObjectBase), parametersObjectType, "parametersObjectType");
			}
			ParametersObjectTypeName = parametersObjectType != null ? parametersObjectType.FullName : string.Empty;
		}
		void IReportDataV2Writable.SetDataType(Type newDataType)
		{
			dataTypeName = newDataType != null ? newDataType.FullName : string.Empty;
		}
		void IReportDataV2Writable.SetDisplayName(string displayName)
		{
			DisplayName = displayName;
		}
		[VisibleInListView(false)]
		[VisibleInDetailView(false)]
		[NonPersistent]
		public bool IsPredefined
		{
			get { return PredefinedReportType != null; }
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
                SetPropertyValue(nameof(FileNameAsAttachment), ref _FileNameAsAttachment, value);
            }
        }
    }
}