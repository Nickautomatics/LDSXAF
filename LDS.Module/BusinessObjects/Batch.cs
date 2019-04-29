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
using DevExpress.ExpressApp.ConditionalAppearance;

namespace LDS.Module.BusinessObjects
{
    [DefaultClassOptions]
    [XafDefaultProperty(nameof(BatchNo))]

    public class Batch : BaseObject
    { 
        public Batch(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        Program program;
        bool isActiveBatch;
        int batchNo;

        [DefaultValue(false)]
        public bool IsActiveBatch
        {
            get => isActiveBatch;
            set => SetPropertyValue(nameof(IsActiveBatch), ref isActiveBatch, value);
        }

        [RuleRequiredField]
        public int BatchNo
        {
            get => batchNo;
            set => SetPropertyValue(nameof(BatchNo), ref batchNo, value);
        }

        public Program Program
        {
            get => program;
            set => SetPropertyValue(nameof(Program), ref program, value);
        }

        [Association("Batch-Students")]
        public XPCollection<Student> Students
        {
            get
            {
                return GetCollection<Student>(nameof(Students));
            }
        }

    }
}