using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using System.ComponentModel;
using DevExpress.Persistent.Validation;
using System.IO;
using DevExpress.ExpressApp.Utils;
using DevExpress.ExpressApp;
using EDS.RentalManager.Express.Module.BusinessObjects.Common;
using DevExpress.ExpressApp.DC;
using QuickZ.Persistent.Base.Core;
using System.Windows.Forms;

namespace QuickZ.Persistent.BaseImpl
{
    [DevExpress.ExpressApp.DC.XafDefaultProperty(nameof(FileName))]
    [DeferredDeletion(false), OptimisticLocking(false)]
    [ImageName("BOBusinessFileData")]
    [Persistent("BusinessFile")]
    //[FileAttachment("TargetFile")]
    public class BusinessFileData : RMXQuickAuditBaseObject, IFileData, IEmptyCheckable, ILocationPrivacyObject
    {
        RMXLocationCategory ILocationPrivacyObject.Location => (this.HomeLocation);

        public BusinessFileData(Session session) : base(session)
        {
        }

      
        private string fileName;
        [Persistent("Size")]
        private int size;

        public void Clear()
        {
            this.Content = null;
            this.FileName = string.Empty;

            // --- from FileSystemStoreObject
            //Dennis: When clearing the file name property we need to save the name of the old file to remove it from the store in the future. You can also setup a separate service for that.
            if (string.IsNullOrEmpty(tempFileName))
                tempFileName = RealFileName;
            StoredFileSize = 0;
        }

        public virtual void LoadFromStream(string fileName, Stream source)
        {
            Guard.ArgumentNotNull(source, "stream");
            this.FileName = fileName;
            byte[] buffer = new byte[source.Length];
            source.Read(buffer, 0, buffer.Length);
            this.Content = buffer;

            // --- from FileSystemStoreObject
            TempSourceStream = source;
            //Dennis: When assigning a new file we need to save the name of the old file to remove it from the store in the future.
            if (string.IsNullOrEmpty(tempFileName))
                tempFileName = RealFileName;
        }
        ////Dennis: Fires when uploading a file.
        //void IFileData.LoadFromStream(string fileName, Stream source)
        //{
        //    //Dennis: When assigning a new file we need to save the name of the old file to remove it from the store in the future.
        //    if (fileName != FileName) // updated, old code was: if (string.IsNullOrEmpty(tempFileName))
        //        tempFileName = RealFileName;

        //    FileName = fileName;
        //    TempSourceStream = source;
        //}
        public virtual void SaveToStream(Stream destination)
        {
            if (this.Content != null)
            {
                destination.Write(this.Content, 0, this.Size);

                // --- from FileSystemStoreObject
                try
                {
                    if (!string.IsNullOrEmpty(RealFileName))
                    {
                        //if (destination == null)
                        //    OpenFileWithDefaultProgram(RealFileName);
                        //else
                        CopyFileToStream(RealFileName, destination);
                    }
                    else if (TempSourceStream != null)
                        CopyStream(TempSourceStream, destination);
                }
                catch (DirectoryNotFoundException exc)
                {
                    MessageBox.Show(exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //DomainService.Instance.PopupMessage("Warning", exc);
                    //throw new UserFriendlyException(exc);
                }
                catch (FileNotFoundException exc)
                {
                    MessageBox.Show(exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //DomainService.Instance.PopupMessage("Warning", exc);
                    //throw new UserFriendlyException(exc);
                }

            }
            destination.Flush();
        }

        public override string ToString()
        {
            return this.FileName;
        }
        
        public string ExtName
        {
            get { return Path.GetExtension(FileName); }
        }
        

        [MemberDesignTimeVisibility(false), Persistent, Delayed(true), ValueConverter(typeof(CompressionConverter)), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public byte[] Content
        {
            get
            {
                return base.GetDelayedPropertyValue<byte[]>("Content");
            }
            set
            {
                int size = this.size;
                if (value != null)
                {
                    this.size = value.Length;
                }
                else
                {
                    this.size = 0;
                }
                base.SetDelayedPropertyValue<byte[]>("Content", value);
                this.OnChanged("Size", size, this.size);
            }
        }

        [Size(260), Custom("AllowEdit", "False")]
        public string FileName
        {
            get
            {
                return this.fileName;
            }
            set
            {
                base.SetPropertyValue<string>("FileName", ref this.fileName, value);
            }
        }

        //--------------------------------------------------------------------------------------------------
        //This additional properties is required for email processing to have a original copy of attach document
        private string _OriginalFileName;
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [Browsable(false)]
        public string OriginalFileName
        {
            get
            {
                return _OriginalFileName;
            }
            set
            {
                SetPropertyValue("OriginalFileName", ref _OriginalFileName, value);
            }
        }

        private int _OriginalSize;
        [Browsable(false)]
        public int OriginalSize
        {
            get
            {
                return _OriginalSize;
            }
            set
            {
                SetPropertyValue("OriginalSize", ref _OriginalSize, value);
            }
        }

        private byte[] _OriginalContent;
        [Browsable(false)]
        public byte[] OriginalContent
        {
            get
            {
                return _OriginalContent;
            }
            set
            {
                SetPropertyValue("OriginalContent", ref _OriginalContent, value);
            }
        }

        private DateTime _OriginalDate;
        [Browsable(false)]
        public DateTime OriginalDate
        {
            get
            {
                return _OriginalDate;
            }
            set
            {
                SetPropertyValue("OriginalDate", ref _OriginalDate, value);
            }
        }
        //--------------------------------------------------------------------------------------------------------

        [NonPersistent, MemberDesignTimeVisibility(false)]
        public bool IsEmpty
        {
            get
            {
                return string.IsNullOrEmpty(this.FileName);
            }
        }

        public int Size
        {
            get
            {
                return this.size;
            }
        }

        //private int _StoredFileSize;
        [NonPersistent]
        public int StoredFileSize
        {
            get { return GetPropertyValue<int>("StoredFileSize"); }
            private set { SetPropertyValue<int>("StoredFileSize", value); }
        }

        #region From FileSystemStoreObject class

        private string tempFileName = string.Empty;
        private static object syncRoot = new object();

        private Stream tempSourceStream;
        [Browsable(false)]
        public Stream TempSourceStream
        {
            get { return tempSourceStream; }
            set
            {
                //Michael: The original Stream might be closed after a while (on the web too - T160753)
                if (value == null)
                {
                    tempSourceStream = null;
                }
                else
                {
                    if (value.Length > (long)int.MaxValue) throw new UserFriendlyException("File is too long");
                    tempSourceStream = new MemoryStream((int)value.Length);
                    CopyStream(value, tempSourceStream);
                    tempSourceStream.Position = 0;
                }
            }
        }

        public string RealFileName
        {
            get
            {
                if (!string.IsNullOrEmpty(FileName) && Oid != Guid.Empty)
                    return Path.Combine(FileSystemStoreLocation, string.Format("{0}-{1}", Oid, FileName));
                return null;
            }
        }
        protected virtual void SaveFileToStore()
        {
            
            if (!string.IsNullOrEmpty(RealFileName))             
            {
             
                try
                {
                    //first separate directory and filename
                    //second step check if the path is already exist
                    //third step If not create condition

                    string dir = Path.GetDirectoryName(RealFileName);


                    if (!Directory.Exists(dir))
                        Directory.CreateDirectory(dir);

                    using (Stream destination = File.OpenWrite(RealFileName))
                       
                    {
                        CopyStream(TempSourceStream, destination);
                        StoredFileSize = (int)destination.Length;
                    }
                }
                catch (DirectoryNotFoundException exc)
                {
                    MessageBox.Show(exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // DomainService.Instance.PopupMessage("Warning", exc);
                    //throw new UserFriendlyException(exc);
                }
            }
        }
        private void RemoveOldFileFromStore()
        {
            //Dennis: We need to remove the old file from the store when saving the current object.
            if (!string.IsNullOrEmpty(tempFileName) && tempFileName != RealFileName)
            {//B222892
                try
                {
                    File.Delete(tempFileName);
                    tempFileName = string.Empty;
                }
                catch (DirectoryNotFoundException exc)
                {
                    MessageBox.Show(exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //DomainService.Instance.PopupMessage("Warning", exc);
                    //throw new UserFriendlyException(exc);
                }
            }
        }

        protected override void OnSaving()
        {
            base.OnSaving();
            Guard.ArgumentNotNullOrEmpty(FileSystemStoreLocation, "FileSystemStoreLocation");
            lock (syncRoot)
            {
                if (!Directory.Exists(FileSystemStoreLocation))
                    Directory.CreateDirectory(FileSystemStoreLocation);
            }
            SaveFileToStore();
            RemoveOldFileFromStore();
        }
        protected override void OnDeleting()
        {
            //Dennis: We need to remove the old file from the store.
            Clear();
            base.OnDeleting();
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();

            // TODO: Might need to transfer this code to a Controller for better performance
            if (TempSourceStream == null && Content != null)
            {
                TempSourceStream = new MemoryStream(Content);
                SaveFileToStore();
            }
        }

        protected override void Invalidate(bool disposing)
        {
            if (disposing && TempSourceStream != null)
            {
                TempSourceStream.Close();
                TempSourceStream = null;
            }
            base.Invalidate(disposing);
        }
        #endregion

        public static int ReadBytesSize = 0x1000;
        public static string FileSystemStoreLocation = String.Format("{0}Files", PathHelper.GetApplicationFolder());

        public static void CopyFileToStream(string sourceFileName, Stream destination)
        {
            if (string.IsNullOrEmpty(sourceFileName) || destination == null) return;
            using (Stream source = File.OpenRead(sourceFileName))
                CopyStream(source, destination);
        }
        public static void OpenFileWithDefaultProgram(string sourceFileName)
        {
            Guard.ArgumentNotNullOrEmpty(sourceFileName, "sourceFileName");
            System.Diagnostics.Process.Start(sourceFileName);
        }
        public static void CopyStream(Stream source, Stream destination)
        {
            if (source == null || destination == null) return;
            byte[] buffer = new byte[ReadBytesSize];
            int read = 0;
            while ((read = source.Read(buffer, 0, buffer.Length)) > 0)
                destination.Write(buffer, 0, read);
        }

       
    }
}
