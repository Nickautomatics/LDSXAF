using System;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using EDS.RentalManager.Express.Module.BusinessObjects.Common;
using QuickZ.Persistent.Base.Documents;
using QuickZ.Persistent.Base.Core;
using System.Collections;
using DevExpress.ExpressApp.Utils;
using QuickZ.Persistent.BaseImpl.Mailings;

namespace QuickZ.Persistent.BaseImpl
{
    [Persistent("PrintDocumentTemplate")]
    [DevExpress.ExpressApp.DC.XafDefaultProperty(nameof(GroupName))]
    public abstract class PrintDocumentGroupTemplateBase : RMXQuickAuditBaseObject, IDocumentPrintGroup, ILocationPrivacyObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        
        public PrintDocumentGroupTemplateBase(Session session)
            : base(session)
        {
        }

        RMXLocationCategory ILocationPrivacyObject.Location => (this.HomeLocation);
        private string _GroupName;
        [Size(128)]
        public string GroupName
        {
            get
            {
                return _GroupName;
            }
            set
            {
                SetPropertyValue(nameof(GroupName), ref _GroupName, value);
            }
        }
        private Type _DataType;
        [TypeConverter(typeof(LocalizedClassInfoTypeConverter))]
        [ValueConverter(typeof(TypeToStringConverter))]
        public Type TargetObjectType
        {
            get { return _DataType; }
            set { SetPropertyValue(nameof(TargetObjectType), ref _DataType, value); }
        }

    }
}