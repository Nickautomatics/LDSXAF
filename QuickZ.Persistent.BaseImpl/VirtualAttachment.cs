using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.ExpressApp;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using QuickZ.Persistent.Base.Core;
using QuickZ.Persistent.Base.Documents;
using EDS.RentalManager.Express.Module.BusinessObjects.Common;
using DevExpress.Persistent.Base;

namespace QuickZ.Persistent.BaseImpl
{
    [NonPersistent]
    //[DevExpress.ExpressApp.DC.DomainComponent]
    [DevExpress.ExpressApp.DC.XafDefaultProperty(nameof(Category))]
    public abstract class VirtualAttachment : RMXQuickAuditBaseObject
    {
        public VirtualAttachment(Session session)
            : base(session)
        { }
        private string _Category;
        public string Category
        {
            get { return _Category; }
            set { SetPropertyValue("Category", ref _Category, value); }
        }

        //[Size(SizeAttribute.Unlimited), ValueConverter(typeof(DevExpress.Xpo.Metadata.ImageValueConverter)), Delayed(true)]
        //public System.Drawing.Image Glyph {
        //    get { return GetPropertyValue<System.Drawing.Image>(nameof(Glyph)); }
        //    set { SetPropertyValue<System.Drawing.Image>(nameof(Glyph), value); }
        //}
        [DevExpress.ExpressApp.DC.XafDisplayName("Glyph")]
        [Delayed(true), VisibleInListViewAttribute(false)]
        [Size(SizeAttribute.Unlimited), ImmediatePostData]
        [ImageEditor(ListViewImageEditorMode = ImageEditorMode.PictureEdit, DetailViewImageEditorMode = ImageEditorMode.PictureEdit, ListViewImageEditorCustomHeight = 40)]
        public byte[] Glyph
        {
            get { return GetDelayedPropertyValue<byte[]>(nameof(Glyph)); }
            set { SetDelayedPropertyValue<byte[]>(nameof(Glyph), value); }
        }
    }
}
