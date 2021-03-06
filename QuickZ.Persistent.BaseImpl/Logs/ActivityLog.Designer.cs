//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using EDS.RentalManager.Express.Module.BusinessObjects.Common;

namespace EDS.RentalManager.Express.Module.BusinessObjects
{
    public enum AcitivtyType
    {
        Processing,
        Email,
        Print,
        Document
    }

    [ImageName("BOActivityLog")]
    [NonPersistent]
    [DevExpress.ExpressApp.DC.XafDefaultProperty(nameof(ActivityDescription))]
    public abstract partial class ActivityLog : RMXQuickAuditBaseObject
    {
        //protected override void OnSaving()
        //{
        //    base.OnSaving();

        //    //if (Session.IsNewObject(this))
        //    //{
        //    //    CreatedOn = DateTime.Now;
        //    //    CreatedBy = Session.GetObjectByKey<Employee>(SecuritySystem.CurrentUserId);
        //    //}

        //}

        #region Persistent Properties

        private string _ActivityDescription;
        [Size(512)]
        public string ActivityDescription
        {
            get { return _ActivityDescription; }
            set { SetPropertyValue("ActivityDescription", ref _ActivityDescription, value); }
        }

  
        #endregion
    }

}
