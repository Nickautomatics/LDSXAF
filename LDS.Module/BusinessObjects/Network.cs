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

    public class Network : XPObject, INetwork
    { 
        public Network(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        string networkLeader;
        string name;

        [RuleRequiredField]
        public string Name
        {
            get => name;
            set => SetPropertyValue(nameof(Name), ref name, value);
        }

        
        public string NetworkLeader
        {
            get => networkLeader;
            set => SetPropertyValue(nameof(NetworkLeader), ref networkLeader, value);
        }

        [Association("Network-Students")]
        public XPCollection<Student> Students
        {
            get
            {
                return GetCollection<Student>(nameof(Students));
            }
        }

    }
}