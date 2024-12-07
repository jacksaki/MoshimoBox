using Livet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoshimoBox.Models
{
    public class FileInfo: NotificationObject
    {
        public FileInfo(string path)
        {
            var fi = new System.IO.FileInfo(path);
            if (!fi.Exists)
            {
                return;
            }
            this.Created = fi.CreationTime;
            this.LastAccess = fi.LastAccessTime;
            this.LastUpdated = fi.LastWriteTime;
            this.IsReadOnly = fi.IsReadOnly;
            this.SHA256 = System.IO.File.ReadAllBytes(path).ToSha256();
            this.Length = fi.Length;
        }

        private FileSizeUnit _Unit;

        public FileSizeUnit Unit
        {
            get
            {
                return _Unit;
            }
            set
            { 
                if (_Unit == value)
                {
                    return;
                }
                _Unit = value;
                RaisePropertyChanged();
            }
        }

        public decimal Size
        {
            get
            {
                if(this.Unit == null)
                {
                    return this.Length;
                }
                else
                {
                    return this.Unit.GetSize(this.Length);
                }
            }
        }
        private long Length { get; }
        public bool IsReadOnly { get; }
        public string Name { get; }
        public DateTime Created { get; }
        public DateTime LastUpdated { get; }
        public DateTime LastAccess { get; }
        public string SHA256 { get; }
    }
}
