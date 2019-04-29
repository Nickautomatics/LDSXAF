﻿using System;
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
    public partial class BatchViewController : ObjectViewController<DetailView, Batch>
    {
        string CurrentUserRole;
        public BatchViewController()
        {
        }
        protected override void OnActivated()
        {
            base.OnActivated();
        
            CurrentUserRole = ((XPCollection<PermissionPolicyRole>)((PermissionPolicyUser)SecuritySystem.CurrentUser).Roles).FirstOrDefault().Name;
            Batch CurrentBatch = View.CurrentObject as Batch;
            if (CurrentBatch.Program == Program.None && (Program)Enum.Parse(typeof(Program), CurrentUserRole) != Program.Administrators)
            {
                CurrentBatch.Program = (Program)Enum.Parse(typeof(Program), CurrentUserRole);
            }

            AppearanceController test = Frame?.GetController<AppearanceController>();
            test.CustomApplyAppearance += Test_CustomApplyAppearance;
        }

        private void Test_CustomApplyAppearance(object sender, ApplyAppearanceEventArgs e)
        {
            if(View != null)
            {

            ViewItem item = View.FindItem("Program");
            if (item is IAppearanceEnabled)
            {
                ((IAppearanceEnabled)item).Enabled = (Program)Enum.Parse(typeof(Program), CurrentUserRole) == Program.Administrators;
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
