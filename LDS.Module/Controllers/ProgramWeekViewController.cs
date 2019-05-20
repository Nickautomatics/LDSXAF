using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.ConditionalAppearance;
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
    public partial class ProgramWeekViewController : ObjectViewController<DetailView, ProgramWeek>
    {
        string CurrentUserRole;

        public ProgramWeekViewController()
        {
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            CurrentUserRole = ((XPCollection<PermissionPolicyRole>)((PermissionPolicyUser)SecuritySystem.CurrentUser).Roles).FirstOrDefault().Name;

            ProgramWeek CurrentUser = View.CurrentObject as ProgramWeek;
            FilterByProgram(CurrentUserRole, CurrentUser);

            AppearanceController test = Frame?.GetController<AppearanceController>();
            test.CustomApplyAppearance += Test_CustomApplyAppearance;
        }

        public void FilterByProgram(string CurrentUserRole, ProgramWeek CurrentUser)
        {

            if (CurrentUser.Program == Program.None && (Program)Enum.Parse(typeof(Program), CurrentUserRole) != Program.Administrators)
            {
                CurrentUser.Program = (Program)Enum.Parse(typeof(Program), CurrentUserRole);
            }
        }

        private void Test_CustomApplyAppearance(object sender, ApplyAppearanceEventArgs e)
        {
            if (View != null)
            {

                ViewItem item = View.FindItem("Program");
                if (item is IAppearanceEnabled)
                {
                    ((IAppearanceEnabled)item).Enabled = (Program)Enum.Parse(typeof(Program), CurrentUserRole) == Program.Administrators;
                }

                ViewItem item2 = View.FindItem("Batch");
                if (item2 is IAppearanceEnabled)
                {
                    ((IAppearanceEnabled)item2).Enabled = (Program)Enum.Parse(typeof(Program), CurrentUserRole) == Program.Administrators;
                }
            }

        }

        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
    }
}
