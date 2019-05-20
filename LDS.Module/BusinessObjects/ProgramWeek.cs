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
using LDS.Module.PublicEnums;

namespace LDS.Module.BusinessObjects
{
    [DefaultClassOptions]

    public class ProgramWeek : XPObject
    { 
        public ProgramWeek(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        Program program;
        bool activeWeek;
        PublicEnums.Week weeks;

        public PublicEnums.Week Weeks
        {
            get => weeks;
            set => SetPropertyValue(nameof(Weeks), ref weeks, value);
        }

        public bool ActiveWeek
        {
            get => activeWeek;
            set => SetPropertyValue(nameof(ActiveWeek), ref activeWeek, value);
        }

        public Program Program
        {
            get => program;
            set => SetPropertyValue(nameof(Program), ref program, value);
        }


    }
}