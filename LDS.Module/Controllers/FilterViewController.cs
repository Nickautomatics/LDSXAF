using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using LDS.Module.BusinessObjects;
using LDS.Module.PublicEnums;

namespace LDS.Module.Controllers
{
    public partial class FilterViewController : ViewController<ListView>
    {
        public FilterViewController()
        {
        }

        protected override void OnActivated()
        {
            base.OnActivated();
            string CurrentUserRole = ((XPCollection<PermissionPolicyRole>)((PermissionPolicyUser)SecuritySystem.CurrentUser).Roles).FirstOrDefault().Name;


            if (View is ListView listview)
            {
                switch (View.Id)
                {
                    case "Student_ListView":
                    case "Student_ListView_Attendance":
                    case "Batch_ListView":
                        if (CurrentUserRole != "Administrators")
                            listview.CollectionSource.Criteria["FilterStudent"] = CriteriaOperator.Parse($"[Program] = '{(Program)Enum.Parse(typeof(Program), CurrentUserRole)}'");
                        break;
                    default:
                        break;
                }


            }


        }

        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
        }

        protected override void OnDeactivated()
        {
            base.OnDeactivated();
        }

    }
}
