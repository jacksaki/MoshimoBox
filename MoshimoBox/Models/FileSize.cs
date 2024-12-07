using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoshimoBox.Models
{
    public class FileSizeUnit
    {
        public static FileSizeUnit[] GetAll()
        {
            return new FileSizeUnit[]
            {
                new FileSizeUnit("B", "バイト", 1L),
                new FileSizeUnit("KB", "キロバイト", 1024L),
                new FileSizeUnit("MB", "メガバイト", 1024L * 1024L),
                new FileSizeUnit("GB", "ギガバイト", 1024L * 1024L * 1024L),
                new FileSizeUnit("TB", "テラバイト", 1024L * 1024L * 1024L * 1024L),
                new FileSizeUnit("PB", "ペタバイト", 1024L * 1024L * 1024L * 1024L * 1024L),
            };
        }

        public decimal GetSize(long length)
        {
            return (decimal)(length / this.Per);
        }

        private FileSizeUnit(string name, string nameJp, long per)
        {
            this.Name = name;
            this.NameJp = nameJp;
            this.Per = per;
        }

        public string Name { get; }
        public string NameJp { get; }
        public long Per { get; }
    }
}
