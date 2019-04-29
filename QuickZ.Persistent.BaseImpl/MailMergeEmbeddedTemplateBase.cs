using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using QuickZ.Persistent.Base.Mails;
using QuickZ.Persistent.Base.Core;
using QuickZ.Persistent.BaseImpl;

namespace QuickZ.Persistent.BaseImpl.Mailings
{
    [NonPersistent]
    [ImageName("BOMailMergeEmbeddedTemplate")]
    [MapInheritance(MapInheritanceType.ParentTable)]
    [DevExpress.ExpressApp.DC.XafDefaultProperty(nameof(TemplateName))]
    public abstract class MailMergeEmbeddedTemplateBase : MailMergeTemplateBase, IMailMergeTemplate, IObjectWithMailMergeTemplate
    {
        public MailMergeEmbeddedTemplateBase(DevExpress.Xpo.Session session)
            : base(session)
        {
            
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            OwnerType = BusinessDataOwnership.System;   
        }



    }
}
