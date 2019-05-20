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
using LDS.Module.PublicEnums;
using DevExpress.ExpressApp.ConditionalAppearance;
using System.Drawing;
using DevExpress.Xpo.Metadata;
using DevExpress.ExpressApp.Xpo;

namespace LDS.Module.BusinessObjects
{
#region AppearanceRule
    [Appearance("AbsentWeek1", AppearanceItemType = "ViewItem", TargetItems = "Week1", Criteria = "Week1 = 'Absent'", Context = "ListView", BackColor = "Red", FontColor = "White", Priority = 2)]
    [Appearance("AbsentWeek2", AppearanceItemType = "ViewItem", TargetItems = "Week2", Criteria = "Week2 = 'Absent'", Context = "ListView", BackColor = "Red", FontColor = "White", Priority = 2)]
    [Appearance("AbsentWeek3", AppearanceItemType = "ViewItem", TargetItems = "Week3", Criteria = "Week3 = 'Absent'", Context = "ListView", BackColor = "Red", FontColor = "White", Priority = 2)]
    [Appearance("AbsentWeek4", AppearanceItemType = "ViewItem", TargetItems = "Week4", Criteria = "Week4 = 'Absent'", Context = "ListView", BackColor = "Red", FontColor = "White", Priority = 2)]
    [Appearance("AbsentWeek5", AppearanceItemType = "ViewItem", TargetItems = "Week5", Criteria = "Week5 = 'Absent'", Context = "ListView", BackColor = "Red", FontColor = "White", Priority = 2)]
    [Appearance("AbsentWeek6", AppearanceItemType = "ViewItem", TargetItems = "Week6", Criteria = "Week6 = 'Absent'", Context = "ListView", BackColor = "Red", FontColor = "White", Priority = 2)]
    [Appearance("AbsentWeek7", AppearanceItemType = "ViewItem", TargetItems = "Week7", Criteria = "Week7 = 'Absent'", Context = "ListView", BackColor = "Red", FontColor = "White", Priority = 2)]
    [Appearance("AbsentWeek8", AppearanceItemType = "ViewItem", TargetItems = "Week8", Criteria = "Week8 = 'Absent'", Context = "ListView", BackColor = "Red", FontColor = "White", Priority = 2)]
    [Appearance("AbsentWeek9", AppearanceItemType = "ViewItem", TargetItems = "Week9", Criteria = "Week9 = 'Absent'", Context = "ListView", BackColor = "Red", FontColor = "White", Priority = 2)]
    [Appearance("AbsentWeek10", AppearanceItemType = "ViewItem", TargetItems = "Week10", Criteria = "Week10 = 'Absent'", Context = "ListView", BackColor = "Red", FontColor = "White", Priority = 2)]

    [Appearance("PresentWeek1", AppearanceItemType = "ViewItem", TargetItems = "Week1", Criteria = "Week1 = 'Present'", Context = "ListView", BackColor = "Green", FontColor = "White", Priority = 2)]
    [Appearance("PresentWeek2", AppearanceItemType = "ViewItem", TargetItems = "Week2", Criteria = "Week2 = 'Present'", Context = "ListView", BackColor = "Green", FontColor = "White", Priority = 2)]
    [Appearance("PresentWeek3", AppearanceItemType = "ViewItem", TargetItems = "Week3", Criteria = "Week3 = 'Present'", Context = "ListView", BackColor = "Green", FontColor = "White", Priority = 2)]
    [Appearance("PresentWeek4", AppearanceItemType = "ViewItem", TargetItems = "Week4", Criteria = "Week4 = 'Present'", Context = "ListView", BackColor = "Green", FontColor = "White", Priority = 2)]
    [Appearance("PresentWeek5", AppearanceItemType = "ViewItem", TargetItems = "Week5", Criteria = "Week5 = 'Present'", Context = "ListView", BackColor = "Green", FontColor = "White", Priority = 2)]
    [Appearance("PresentWeek6", AppearanceItemType = "ViewItem", TargetItems = "Week6", Criteria = "Week6 = 'Present'", Context = "ListView", BackColor = "Green", FontColor = "White", Priority = 2)]
    [Appearance("PresentWeek7", AppearanceItemType = "ViewItem", TargetItems = "Week7", Criteria = "Week7 = 'Present'", Context = "ListView", BackColor = "Green", FontColor = "White", Priority = 2)]
    [Appearance("PresentWeek8", AppearanceItemType = "ViewItem", TargetItems = "Week8", Criteria = "Week8 = 'Present'", Context = "ListView", BackColor = "Green", FontColor = "White", Priority = 2)]
    [Appearance("PresentWeek9", AppearanceItemType = "ViewItem", TargetItems = "Week9", Criteria = "Week9 = 'Present'", Context = "ListView", BackColor = "Green", FontColor = "White", Priority = 2)]
    [Appearance("PresentWeek10", AppearanceItemType = "ViewItem", TargetItems = "Week10", Criteria = "Week10 = 'Present'", Context = "ListView", BackColor = "Green", FontColor = "White", Priority = 2)]

    [Appearance("ExcusedWeek1", AppearanceItemType = "ViewItem", TargetItems = "Week1", Criteria = "Week1 = 'Excused'", Context = "ListView", BackColor = "Pink", FontColor = "White", Priority = 2)]
    [Appearance("ExcusedWeek2", AppearanceItemType = "ViewItem", TargetItems = "Week2", Criteria = "Week2 = 'Excused'", Context = "ListView", BackColor = "Pink", FontColor = "White", Priority = 2)]
    [Appearance("ExcusedWeek3", AppearanceItemType = "ViewItem", TargetItems = "Week3", Criteria = "Week3 = 'Excused'", Context = "ListView", BackColor = "Pink", FontColor = "White", Priority = 2)]
    [Appearance("ExcusedWeek4", AppearanceItemType = "ViewItem", TargetItems = "Week4", Criteria = "Week4 = 'Excused'", Context = "ListView", BackColor = "Pink", FontColor = "White", Priority = 2)]
    [Appearance("ExcusedWeek5", AppearanceItemType = "ViewItem", TargetItems = "Week5", Criteria = "Week5 = 'Excused'", Context = "ListView", BackColor = "Pink", FontColor = "White", Priority = 2)]
    [Appearance("ExcusedWeek6", AppearanceItemType = "ViewItem", TargetItems = "Week6", Criteria = "Week6 = 'Excused'", Context = "ListView", BackColor = "Pink", FontColor = "White", Priority = 2)]
    [Appearance("ExcusedWeek7", AppearanceItemType = "ViewItem", TargetItems = "Week7", Criteria = "Week7 = 'Excused'", Context = "ListView", BackColor = "Pink", FontColor = "White", Priority = 2)]
    [Appearance("ExcusedWeek8", AppearanceItemType = "ViewItem", TargetItems = "Week8", Criteria = "Week8 = 'Excused'", Context = "ListView", BackColor = "Pink", FontColor = "White", Priority = 2)]
    [Appearance("ExcusedWeek9", AppearanceItemType = "ViewItem", TargetItems = "Week9", Criteria = "Week9 = 'Excused'", Context = "ListView", BackColor = "Pink", FontColor = "White", Priority = 2)]
    [Appearance("ExcusedWeek10", AppearanceItemType = "ViewItem", TargetItems = "Week10", Criteria = "Week10 = 'Excused'", Context = "ListView", BackColor = "Pink", FontColor = "White", Priority = 2)]

    [Appearance("NoQR", AppearanceItemType = "ViewItem", TargetItems = "*", Criteria = "QRImage = null", Context = "ListView", BackColor = "Orange", FontColor = "White", Priority = 1)]


    #endregion

    [DefaultClassOptions]
    public class Student : XPLiteObject, IStudent, IAttendance
    {
        public Student(Session session)
            : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        protected override void OnSaving()
        {
            base.OnSaving();
        }

        AssistantTeacher assistantTeacher;
        AttendanceMode week1;
        AttendanceMode week2;
        AttendanceMode week3;
        AttendanceMode week4;
        AttendanceMode week5;
        AttendanceMode week6;
        AttendanceMode week7;
        AttendanceMode week8;
        AttendanceMode week9;
        AttendanceMode week10;

        string studentIDNo;
        Batch batch;
        string contactOfCellLeader;
        string cellLeader;
        string nickname;
        Network network;
        DateTime birthday;
        Program program;
        string lastName;
        string firstName;

        int oid;

        [Key(AutoGenerate = true)]
        [Browsable(false)]
        public int Oid
        {
            get { return oid; }
            set { oid = value; }
        }

        [Browsable(false)]
        public string StudentIDNo
        {
            get => studentIDNo;
            set => SetPropertyValue(nameof(StudentIDNo), ref studentIDNo, value);
        }

        [RuleRequiredField]
        public string FirstName
        {
            get => firstName;
            set => SetPropertyValue(nameof(FirstName), ref firstName, value);
        }

        [RuleRequiredField]
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



        [Size(SizeAttribute.Unlimited), ValueConverter(typeof(ImageValueConverter))]
        public Image Picture
        {
            get { return GetPropertyValue<Image>("Picture"); }
            set { SetPropertyValue<Image>("Picture", value); }
        }

        [Size(SizeAttribute.Unlimited), ValueConverter(typeof(ImageValueConverter))]
        public Image QRImage
        {
            get { return GetPropertyValue<Image>("QRImage"); }
            set { SetPropertyValue<Image>("QRImage", value); }
        }

        [Association("Network-Students")]
        public Network Network
        {
            get => network;
            set => SetPropertyValue(nameof(Network), ref network, value);
        }

        public Program Program
        {
            get => program;
            set => SetPropertyValue(nameof(Program), ref program, value);
        }

        public DateTime Birthday
        {
            get => birthday;
            set => SetPropertyValue(nameof(Birthday), ref birthday, value);
        }

        public string Nickname
        {
            get => nickname;
            set => SetPropertyValue(nameof(Nickname), ref nickname, value);
        }


        public string CellLeader
        {
            get => cellLeader;
            set => SetPropertyValue(nameof(CellLeader), ref cellLeader, value);
        }


        public string ContactOfCellLeader
        {
            get => contactOfCellLeader;
            set => SetPropertyValue(nameof(ContactOfCellLeader), ref contactOfCellLeader, value);
        }

        [VisibleInDetailView(false)]
        public AttendanceMode Week1
        {
            get => week1;
            set => SetPropertyValue(nameof(Week1), ref week1, value);
        }

        [VisibleInDetailView(false)]
        public AttendanceMode Week2
        {
            get => week2;
            set => SetPropertyValue(nameof(Week2), ref week2, value);
        }

        [VisibleInDetailView(false)]
        public AttendanceMode Week3
        {
            get => week3;
            set => SetPropertyValue(nameof(Week3), ref week3, value);
        }

        [VisibleInDetailView(false)]
        public AttendanceMode Week4
        {
            get => week4;
            set => SetPropertyValue(nameof(Week4), ref week4, value);
        }

        [VisibleInDetailView(false)]
        public AttendanceMode Week5
        {
            get => week5;
            set => SetPropertyValue(nameof(Week5), ref week5, value);
        }

        [VisibleInDetailView(false)]
        public AttendanceMode Week6
        {
            get => week6;
            set => SetPropertyValue(nameof(Week6), ref week6, value);
        }

        [VisibleInDetailView(false)]
        public AttendanceMode Week7
        {
            get => week7;
            set => SetPropertyValue(nameof(Week7), ref week7, value);
        }

        [VisibleInDetailView(false)]
        public AttendanceMode Week8
        {
            get => week8;
            set => SetPropertyValue(nameof(Week8), ref week8, value);
        }

        [VisibleInDetailView(false)]
        public AttendanceMode Week9
        {
            get => week9;
            set => SetPropertyValue(nameof(Week9), ref week9, value);
        }

        [VisibleInDetailView(false)]
        public AttendanceMode Week10
        {
            get => week10;
            set => SetPropertyValue(nameof(Week10), ref week10, value);
        }

        [Association("Batch-Students")]
        public Batch Batch
        {
            get => batch;
            set => SetPropertyValue(nameof(Batch), ref batch, value);
        }

        
        [Association("AssistantTeacher-Students")]
        [DataSourceCriteria("Program = '@This.Program'")]
        public AssistantTeacher AssistantTeacher
        {
            get => assistantTeacher;
            set => SetPropertyValue(nameof(AssistantTeacher), ref assistantTeacher, value);
        }
    }
}