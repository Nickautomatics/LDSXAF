using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.Security.Strategy;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using LDS.Module.BusinessObjects;
using LDS.Module.PublicEnums;
using QRCoder;

namespace LDS.Module.Controllers
{
    public partial class StudentViewController : ObjectViewController<DetailView, Student>
    {
        string CurrentUserRole;
        SimpleAction testbutton;
        NewObjectViewController newObject;

        public StudentViewController()
        {
            testbutton = new SimpleAction(this, "GenerateQr", "GenerateQr", GenerateQRButton_Execute);
        }

        protected override void OnActivated()
        {
            base.OnActivated();

            CurrentUserRole = ((XPCollection<PermissionPolicyRole>)((PermissionPolicyUser)SecuritySystem.CurrentUser).Roles).FirstOrDefault().Name;
            Student CurrentUser = View.CurrentObject as Student;

            FilterByProgram(CurrentUserRole, CurrentUser);
            AssignBatch(CurrentUserRole, CurrentUser);


            if(CurrentUser.QRImage != null)
            {
                testbutton.Enabled.SetItemValue("GenerateQr", false);
            } else
            {
                testbutton.Enabled.SetItemValue("GenerateQr", true);
            }

            AppearanceController test = Frame?.GetController<AppearanceController>();
            test.CustomApplyAppearance += Test_CustomApplyAppearance;



            if(View.Id == "Student_DetailView_Generate")
            {
                newObject = Frame.GetController<NewObjectViewController>();
                newObject.NewObjectAction.Enabled.SetItemValue("disableNew", false);
            }



        }


        private void GenerateQRButton_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var currentObject = View.CurrentObject as Student;
            string ID = $"{DateTime.Now.Year}-{currentObject.Oid.ToString().PadLeft(4, '0')}";
            currentObject.StudentIDNo = ID;
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(ID, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);
            currentObject.QRImage = qrCodeImage;
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

        public void FilterByProgram(string CurrentUserRole, Student CurrentUser)
        {

            if (CurrentUser.Program == Program.None && (Program)Enum.Parse(typeof(Program), CurrentUserRole) != Program.Administrators)
            {
                CurrentUser.Program = (Program)Enum.Parse(typeof(Program), CurrentUserRole);
            }
        }

        public void AssignBatch(string CurrentUserRole, Student CurrentUser)
        {
            XPCollection<Batch> test = (XPCollection<Batch>)ObjectSpace.GetObjects<Batch>();
            CurrentUser.Batch = (Batch)test.Where(u => u.IsActiveBatch == true).LastOrDefault();
        }

        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
        }

        protected override void OnDeactivated()
        {
            base.OnDeactivated();
            //newObject.NewObjectAction.Enabled.SetItemValue("disableNew", true);
        }




    }
}
