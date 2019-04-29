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
using LDS.Module.Interfaces;

namespace LDS.Module.BusinessObjects
{
    [DefaultClassOptions]
    [XafDefaultProperty(nameof(FullName))]
    public class AssistantTeacher : BaseObject, ITeacher
    {
        public AssistantTeacher(Session session)
            : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }


        string password;
        string username;
        DateTime birthday;
        string contactOfCellLeader;
        string cellLeader;
        string nickname;
        string lastName;
        string firstName;


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Username
        {
            get => username;
            set => SetPropertyValue(nameof(Username), ref username, value);
        }


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [PasswordPropertyText(true)]
        public string Password
        {
            get => password;
            set => SetPropertyValue(nameof(Password), ref password, value);
        }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string FirstName
        {
            get => firstName;
            set => SetPropertyValue(nameof(FirstName), ref firstName, value);
        }


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string LastName
        {
            get => lastName;
            set => SetPropertyValue(nameof(LastName), ref lastName, value);
        }

        [NonPersistent]
        public string FullName
        {
            get => $"{FirstName} {LastName}";
        }


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Nickname
        {
            get => nickname;
            set => SetPropertyValue(nameof(Nickname), ref nickname, value);
        }


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string CellLeader
        {
            get => cellLeader;
            set => SetPropertyValue(nameof(CellLeader), ref cellLeader, value);
        }



        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string ContactOfCellLeader
        {
            get => contactOfCellLeader;
            set => SetPropertyValue(nameof(ContactOfCellLeader), ref contactOfCellLeader, value);
        }

        
        public DateTime Birthday
        {
            get => birthday;
            set => SetPropertyValue(nameof(Birthday), ref birthday, value);
        }

        [Association("AssistantTeacher-Students")]
        public XPCollection<Student> Students
        {
            get
            {
                return GetCollection<Student>(nameof(Students));
            }
        }
    }
}