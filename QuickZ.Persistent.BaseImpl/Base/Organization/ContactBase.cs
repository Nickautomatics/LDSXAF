using System;
using System.Linq;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Utils;


namespace EDS.RentalManager.Express.Module.BusinessObjects
{

    public partial class ContactBase
    {
        #region Constructors

        public ContactBase(Session session)
            : base(session)
        { }

        #endregion

        #region Events

        public override void AfterConstruction()
        {
            base.AfterConstruction();          
            IsActive = true;
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();

            //if (String.IsNullOrEmpty(FullName))
            //    FullName = $"{FirstName} {LastName}" ;
        }

        #endregion
        
    }

 
}
