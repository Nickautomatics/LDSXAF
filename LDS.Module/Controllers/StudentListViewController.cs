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
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class StudentListViewController : ViewController
    {
        NewObjectViewController newObject;

        public StudentListViewController()
        {
            TargetObjectType = typeof(Student);
            TargetViewType = ViewType.Any;
        }

        protected override void OnActivated()
        {
            base.OnActivated();

            if (View.Id == "Student_ListView_Attendance")
            {
                ((ListEditor)((ListView)View).Editor).AllowEdit = true;
            }

            if (View.Id == "Student_DetailView_Generate" || View.Id == "Student_ListView_Generate")
            {
                newObject = Frame.GetController<NewObjectViewController>();
                newObject.NewObjectAction.Enabled.SetItemValue("disableNew", false);
            }


        }

        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
        }
        protected override void OnDeactivated()
        {
            base.OnDeactivated();
            if (View.Id == "Student_DetailView_Generate" || View.Id == "Student_ListView_Generate")
            {
                newObject.NewObjectAction.Enabled.SetItemValue("disableNew", true);
            }
        }
    }
}
