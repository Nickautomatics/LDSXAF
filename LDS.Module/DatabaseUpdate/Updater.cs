using System;
using System.Linq;
using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Updating;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Security.Strategy;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using LDS.Module.BusinessObjects;
using LDS.Module.PublicEnums;

namespace LDS.Module.DatabaseUpdate {
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppUpdatingModuleUpdatertopic.aspx
    public class Updater : ModuleUpdater {
        public Updater(IObjectSpace objectSpace, Version currentDBVersion) :
            base(objectSpace, currentDBVersion) {
        }
        public override void UpdateDatabaseAfterUpdateSchema() {
            base.UpdateDatabaseAfterUpdateSchema();
            //string name = "MyName";
            //DomainObject1 theObject = ObjectSpace.FindObject<DomainObject1>(CriteriaOperator.Parse("Name=?", name));
            //if(theObject == null) {
            //    theObject = ObjectSpace.CreateObject<DomainObject1>();
            //    theObject.Name = name;
            //}

            //PermissionPolicyUser sampleUser = ObjectSpace.FindObject<PermissionPolicyUser>(new BinaryOperator("UserName", "User"));
            //if(sampleUser == null) {
            //    sampleUser = ObjectSpace.CreateObject<PermissionPolicyUser>();
            //    sampleUser.UserName = "User";
            //    sampleUser.SetPassword("");
            //}
            //PermissionPolicyRole defaultRole = CreateDefaultRole();
            //sampleUser.Roles.Add(defaultRole);

            PermissionPolicyRole lifeClassRole = CreateLifeClassRole();
            PermissionPolicyUser lifeClassUser = ObjectSpace.FindObject<PermissionPolicyUser>(new BinaryOperator("UserName", "LifeClass"));
            if (lifeClassUser == null)
            {
                lifeClassUser = ObjectSpace.CreateObject<PermissionPolicyUser>();
                lifeClassUser.UserName = "LifeClass";
                lifeClassUser.SetPassword("");
            }
            lifeClassUser.Roles.Add(lifeClassRole);

            PermissionPolicyRole sol1Role = CreateSOL1Role();
            PermissionPolicyUser sol1User = ObjectSpace.FindObject<PermissionPolicyUser>(new BinaryOperator("UserName", "SOL1"));
            if (sol1User == null)
            {
                sol1User = ObjectSpace.CreateObject<PermissionPolicyUser>();
                sol1User.UserName = "SOL1";
                sol1User.SetPassword("");
            }
            sol1User.Roles.Add(sol1Role);


            PermissionPolicyRole sol2Role = CreateSOL2Role();
            PermissionPolicyUser sol2User = ObjectSpace.FindObject<PermissionPolicyUser>(new BinaryOperator("UserName", "SOL2"));
            if (sol2User == null)
            {
                sol2User = ObjectSpace.CreateObject<PermissionPolicyUser>();
                sol2User.UserName = "SOL2";
                sol2User.SetPassword("");
            }
            sol2User.Roles.Add(sol2Role);

            PermissionPolicyRole sol3Role = CreateSOL3Role();
            PermissionPolicyUser sol3User = ObjectSpace.FindObject<PermissionPolicyUser>(new BinaryOperator("UserName", "SOL3"));
            if (sol3User == null)
            {
                sol3User = ObjectSpace.CreateObject<PermissionPolicyUser>();
                sol3User.UserName = "SOL3";
                sol3User.SetPassword("");
            }
            sol3User.Roles.Add(sol3Role);



            PermissionPolicyUser userAdmin = ObjectSpace.FindObject<PermissionPolicyUser>(new BinaryOperator("UserName", "Admin"));
            if(userAdmin == null) {
                userAdmin = ObjectSpace.CreateObject<PermissionPolicyUser>();
                userAdmin.UserName = "Admin";
                // Set a password if the standard authentication type is used
                userAdmin.SetPassword("");
            }
			// If a role with the Administrators name doesn't exist in the database, create this role
            PermissionPolicyRole adminRole = ObjectSpace.FindObject<PermissionPolicyRole>(new BinaryOperator("Name", "Administrators"));
            if(adminRole == null) {
                adminRole = ObjectSpace.CreateObject<PermissionPolicyRole>();
                adminRole.Name = "Administrators";
            }
            adminRole.IsAdministrative = true;
			userAdmin.Roles.Add(adminRole);


            ObjectSpace.CommitChanges(); //This line persists created object(s).
        }
        public override void UpdateDatabaseBeforeUpdateSchema() {
            base.UpdateDatabaseBeforeUpdateSchema();
            //if(CurrentDBVersion < new Version("1.1.0.0") && CurrentDBVersion > new Version("0.0.0.0")) {
            //    RenameColumn("DomainObject1Table", "OldColumnName", "NewColumnName");
            //}
        }
        private PermissionPolicyRole CreateDefaultRole() {
            PermissionPolicyRole defaultRole = ObjectSpace.FindObject<PermissionPolicyRole>(new BinaryOperator("Name", "Default"));
            if(defaultRole == null) {
                defaultRole = ObjectSpace.CreateObject<PermissionPolicyRole>();
                defaultRole.Name = "Default";

				defaultRole.AddObjectPermission<PermissionPolicyUser>(SecurityOperations.Read, "[Oid] = CurrentUserId()", SecurityPermissionState.Allow);
                defaultRole.AddNavigationPermission(@"Application/NavigationItems/Items/Default/Items/MyDetails", SecurityPermissionState.Allow);
				defaultRole.AddMemberPermission<PermissionPolicyUser>(SecurityOperations.Write, "ChangePasswordOnFirstLogon", "[Oid] = CurrentUserId()", SecurityPermissionState.Allow);
				defaultRole.AddMemberPermission<PermissionPolicyUser>(SecurityOperations.Write, "StoredPassword", "[Oid] = CurrentUserId()", SecurityPermissionState.Allow);
                defaultRole.AddTypePermissionsRecursively<PermissionPolicyRole>(SecurityOperations.Read, SecurityPermissionState.Deny);
                defaultRole.AddTypePermissionsRecursively<ModelDifference>(SecurityOperations.ReadWriteAccess, SecurityPermissionState.Allow);
                defaultRole.AddTypePermissionsRecursively<ModelDifferenceAspect>(SecurityOperations.ReadWriteAccess, SecurityPermissionState.Allow);
				defaultRole.AddTypePermissionsRecursively<ModelDifference>(SecurityOperations.Create, SecurityPermissionState.Allow);
                defaultRole.AddTypePermissionsRecursively<ModelDifferenceAspect>(SecurityOperations.Create, SecurityPermissionState.Allow);                
            }
            return defaultRole;
        }

        private PermissionPolicyRole CreateLifeClassRole()
        {
            PermissionPolicyRole lifeClassRole = ObjectSpace.FindObject<PermissionPolicyRole>(new BinaryOperator("Name", "LifeClass"));
            if (lifeClassRole == null)
            {
                lifeClassRole = ObjectSpace.CreateObject<PermissionPolicyRole>();
                lifeClassRole.Name = Program.LifeClass.ToString();
                lifeClassRole.AddObjectPermission<PermissionPolicyUser>(SecurityOperations.Read, "[Oid] = CurrentUserId()", SecurityPermissionState.Allow);
                //lifeClassRole.AddNavigationPermission(@"Application/NavigationItems/Items/Default/Items/MyDetails", SecurityPermissionState.Allow);
                lifeClassRole.AddMemberPermission<PermissionPolicyUser>(SecurityOperations.Write, "ChangePasswordOnFirstLogon", "[Oid] = CurrentUserId()", SecurityPermissionState.Allow);
                lifeClassRole.AddMemberPermission<PermissionPolicyUser>(SecurityOperations.Write, "StoredPassword", "[Oid] = CurrentUserId()", SecurityPermissionState.Allow);
                lifeClassRole.AddTypePermissionsRecursively<PermissionPolicyRole>(SecurityOperations.Read, SecurityPermissionState.Deny);
                lifeClassRole.AddTypePermissionsRecursively<ModelDifference>(SecurityOperations.ReadWriteAccess, SecurityPermissionState.Allow);
                lifeClassRole.AddTypePermissionsRecursively<ModelDifferenceAspect>(SecurityOperations.ReadWriteAccess, SecurityPermissionState.Allow);
                lifeClassRole.AddTypePermissionsRecursively<ModelDifference>(SecurityOperations.Create, SecurityPermissionState.Allow);
                lifeClassRole.AddTypePermissionsRecursively<ModelDifferenceAspect>(SecurityOperations.Create, SecurityPermissionState.Allow);
                lifeClassRole.AddTypePermission<Student>(SecurityOperations.CRUDAccess, SecurityPermissionState.Allow);
                lifeClassRole.AddTypePermission<Batch>(SecurityOperations.CRUDAccess, SecurityPermissionState.Allow);
                lifeClassRole.AddTypePermission<AssistantTeacher>(SecurityOperations.CRUDAccess, SecurityPermissionState.Allow);
                lifeClassRole.AddTypePermission<ProgramWeek>(SecurityOperations.CRUDAccess, SecurityPermissionState.Allow);
                lifeClassRole.AddNavigationPermission("Application/NavigationItems/Items/Default/Items/Batch_ListView", SecurityPermissionState.Allow);
                lifeClassRole.AddNavigationPermission("Application/NavigationItems/Items/Default/Items/Student_ListView", SecurityPermissionState.Allow);
                lifeClassRole.AddNavigationPermission("Application/NavigationItems/Items/Default/Items/Student_ListView_Attendance", SecurityPermissionState.Allow);
                lifeClassRole.AddNavigationPermission("Application/NavigationItems/Items/Default/Items/Student_ListView_Generate", SecurityPermissionState.Allow);
                lifeClassRole.AddNavigationPermission("Application/NavigationItems/Items/Default/Items/AssistantTeacher_ListView", SecurityPermissionState.Allow);
                lifeClassRole.AddNavigationPermission("Application/NavigationItems/Items/Default/Items/ProgramWeek_ListView", SecurityPermissionState.Allow);
            }
            return lifeClassRole;
        }

        private PermissionPolicyRole CreateSOL1Role()
        {
            PermissionPolicyRole sol1Role = ObjectSpace.FindObject<PermissionPolicyRole>(new BinaryOperator("Name", "SOL1"));
            if (sol1Role == null)
            {
                sol1Role = ObjectSpace.CreateObject<PermissionPolicyRole>();
                sol1Role.Name = Program.SOL1.ToString();
                sol1Role.AddObjectPermission<PermissionPolicyUser>(SecurityOperations.Read, "[Oid] = CurrentUserId()", SecurityPermissionState.Allow);
                //sol1Role.AddNavigationPermission(@"Application/NavigationItems/Items/Default/Items/MyDetails", SecurityPermissionState.Allow);
                sol1Role.AddMemberPermission<PermissionPolicyUser>(SecurityOperations.Write, "ChangePasswordOnFirstLogon", "[Oid] = CurrentUserId()", SecurityPermissionState.Allow);
                sol1Role.AddMemberPermission<PermissionPolicyUser>(SecurityOperations.Write, "StoredPassword", "[Oid] = CurrentUserId()", SecurityPermissionState.Allow);
                sol1Role.AddTypePermissionsRecursively<PermissionPolicyRole>(SecurityOperations.Read, SecurityPermissionState.Deny);
                sol1Role.AddTypePermissionsRecursively<ModelDifference>(SecurityOperations.ReadWriteAccess, SecurityPermissionState.Allow);
                sol1Role.AddTypePermissionsRecursively<ModelDifferenceAspect>(SecurityOperations.ReadWriteAccess, SecurityPermissionState.Allow);
                sol1Role.AddTypePermissionsRecursively<ModelDifference>(SecurityOperations.Create, SecurityPermissionState.Allow);
                sol1Role.AddTypePermissionsRecursively<ModelDifferenceAspect>(SecurityOperations.Create, SecurityPermissionState.Allow);
                sol1Role.AddTypePermission<Student>(SecurityOperations.CRUDAccess, SecurityPermissionState.Allow);
                sol1Role.AddTypePermission<Batch>(SecurityOperations.CRUDAccess, SecurityPermissionState.Allow);
                sol1Role.AddTypePermission<AssistantTeacher>(SecurityOperations.CRUDAccess, SecurityPermissionState.Allow);
                sol1Role.AddTypePermission<ProgramWeek>(SecurityOperations.CRUDAccess, SecurityPermissionState.Allow);
                sol1Role.AddNavigationPermission("Application/NavigationItems/Items/Default/Items/Batch_ListView", SecurityPermissionState.Allow);
                sol1Role.AddNavigationPermission("Application/NavigationItems/Items/Default/Items/Student_ListView", SecurityPermissionState.Allow);
                sol1Role.AddNavigationPermission("Application/NavigationItems/Items/Default/Items/Student_ListView_Attendance", SecurityPermissionState.Allow);
                sol1Role.AddNavigationPermission("Application/NavigationItems/Items/Default/Items/Student_ListView_Generate", SecurityPermissionState.Allow);
                sol1Role.AddNavigationPermission("Application/NavigationItems/Items/Default/Items/AssistantTeacher_ListView", SecurityPermissionState.Allow);
                sol1Role.AddNavigationPermission("Application/NavigationItems/Items/Default/Items/ProgramWeek_ListView", SecurityPermissionState.Allow);
            }
            return sol1Role;
        }


        private PermissionPolicyRole CreateSOL2Role()
        {
            PermissionPolicyRole sol2Role = ObjectSpace.FindObject<PermissionPolicyRole>(new BinaryOperator("Name", "SOL2"));
            if (sol2Role == null)
            {
                sol2Role = ObjectSpace.CreateObject<PermissionPolicyRole>();
                sol2Role.Name = Program.SOL2.ToString();
                sol2Role.AddObjectPermission<PermissionPolicyUser>(SecurityOperations.Read, "[Oid] = CurrentUserId()", SecurityPermissionState.Allow);
                //sol2Role.AddNavigationPermission(@"Application/NavigationItems/Items/Default/Items/MyDetails", SecurityPermissionState.Allow);
                sol2Role.AddMemberPermission<PermissionPolicyUser>(SecurityOperations.Write, "ChangePasswordOnFirstLogon", "[Oid] = CurrentUserId()", SecurityPermissionState.Allow);
                sol2Role.AddMemberPermission<PermissionPolicyUser>(SecurityOperations.Write, "StoredPassword", "[Oid] = CurrentUserId()", SecurityPermissionState.Allow);
                sol2Role.AddTypePermissionsRecursively<PermissionPolicyRole>(SecurityOperations.Read, SecurityPermissionState.Deny);
                sol2Role.AddTypePermissionsRecursively<ModelDifference>(SecurityOperations.ReadWriteAccess, SecurityPermissionState.Allow);
                sol2Role.AddTypePermissionsRecursively<ModelDifferenceAspect>(SecurityOperations.ReadWriteAccess, SecurityPermissionState.Allow);
                sol2Role.AddTypePermissionsRecursively<ModelDifference>(SecurityOperations.Create, SecurityPermissionState.Allow);
                sol2Role.AddTypePermissionsRecursively<ModelDifferenceAspect>(SecurityOperations.Create, SecurityPermissionState.Allow);
                sol2Role.AddTypePermission<Student>(SecurityOperations.CRUDAccess, SecurityPermissionState.Allow);
                sol2Role.AddTypePermission<Batch>(SecurityOperations.CRUDAccess, SecurityPermissionState.Allow);
                sol2Role.AddTypePermission<AssistantTeacher>(SecurityOperations.CRUDAccess, SecurityPermissionState.Allow);
                sol2Role.AddTypePermission<ProgramWeek>(SecurityOperations.CRUDAccess, SecurityPermissionState.Allow);
                sol2Role.AddNavigationPermission("Application/NavigationItems/Items/Default/Items/Batch_ListView", SecurityPermissionState.Allow);
                sol2Role.AddNavigationPermission("Application/NavigationItems/Items/Default/Items/Student_ListView", SecurityPermissionState.Allow);
                sol2Role.AddNavigationPermission("Application/NavigationItems/Items/Default/Items/Student_ListView_Attendance", SecurityPermissionState.Allow);
                sol2Role.AddNavigationPermission("Application/NavigationItems/Items/Default/Items/Student_ListView_Generate", SecurityPermissionState.Allow);
                sol2Role.AddNavigationPermission("Application/NavigationItems/Items/Default/Items/AssistantTeacher_ListView", SecurityPermissionState.Allow);
                sol2Role.AddNavigationPermission("Application/NavigationItems/Items/Default/Items/ProgramWeek_ListView", SecurityPermissionState.Allow);
            }
            return sol2Role;
        }



        private PermissionPolicyRole CreateSOL3Role()
        {
            PermissionPolicyRole sol3Role = ObjectSpace.FindObject<PermissionPolicyRole>(new BinaryOperator("Name", "SOL3"));
            if (sol3Role == null)
            {
                sol3Role = ObjectSpace.CreateObject<PermissionPolicyRole>();
                sol3Role.Name = Program.SOL3.ToString();
                sol3Role.AddObjectPermission<PermissionPolicyUser>(SecurityOperations.Read, "[Oid] = CurrentUserId()", SecurityPermissionState.Allow);
                //sol3Role.AddNavigationPermission(@"Application/NavigationItems/Items/Default/Items/MyDetails", SecurityPermissionState.Allow);
                sol3Role.AddMemberPermission<PermissionPolicyUser>(SecurityOperations.Write, "ChangePasswordOnFirstLogon", "[Oid] = CurrentUserId()", SecurityPermissionState.Allow);
                sol3Role.AddMemberPermission<PermissionPolicyUser>(SecurityOperations.Write, "StoredPassword", "[Oid] = CurrentUserId()", SecurityPermissionState.Allow);
                sol3Role.AddTypePermissionsRecursively<PermissionPolicyRole>(SecurityOperations.Read, SecurityPermissionState.Deny);
                sol3Role.AddTypePermissionsRecursively<ModelDifference>(SecurityOperations.ReadWriteAccess, SecurityPermissionState.Allow);
                sol3Role.AddTypePermissionsRecursively<ModelDifferenceAspect>(SecurityOperations.ReadWriteAccess, SecurityPermissionState.Allow);
                sol3Role.AddTypePermissionsRecursively<ModelDifference>(SecurityOperations.Create, SecurityPermissionState.Allow);
                sol3Role.AddTypePermissionsRecursively<ModelDifferenceAspect>(SecurityOperations.Create, SecurityPermissionState.Allow);
                sol3Role.AddTypePermission<Student>(SecurityOperations.CRUDAccess, SecurityPermissionState.Allow);
                sol3Role.AddTypePermission<Batch>(SecurityOperations.CRUDAccess, SecurityPermissionState.Allow);
                sol3Role.AddTypePermission<AssistantTeacher>(SecurityOperations.CRUDAccess, SecurityPermissionState.Allow);
                sol3Role.AddTypePermission<ProgramWeek>(SecurityOperations.CRUDAccess, SecurityPermissionState.Allow);
                sol3Role.AddNavigationPermission("Application/NavigationItems/Items/Default/Items/Batch_ListView", SecurityPermissionState.Allow);
                sol3Role.AddNavigationPermission("Application/NavigationItems/Items/Default/Items/Student_ListView", SecurityPermissionState.Allow);
                sol3Role.AddNavigationPermission("Application/NavigationItems/Items/Default/Items/Student_ListView_Attendance", SecurityPermissionState.Allow);
                sol3Role.AddNavigationPermission("Application/NavigationItems/Items/Default/Items/Student_ListView_Generate", SecurityPermissionState.Allow);
                sol3Role.AddNavigationPermission("Application/NavigationItems/Items/Default/Items/AssistantTeacher_ListView", SecurityPermissionState.Allow);
                sol3Role.AddNavigationPermission("Application/NavigationItems/Items/Default/Items/ProgramWeek_ListView", SecurityPermissionState.Allow);

            }
            return sol3Role;
        }

    }
}
