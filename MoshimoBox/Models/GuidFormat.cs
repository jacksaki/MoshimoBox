using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoshimoBox.Models
{
    public class GuidFormat(string formatText, string description)
    {
        public string FormatText => formatText;
        public string Description => description;
        public string DisplayText => $"{formatText}: {Description}";
        public static GuidFormat[] GetAll()
        {
            return new GuidFormat[]
            {
                new GuidFormat("N","32桁 ⇒ xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx"),
                new GuidFormat("D" ,"ハイフンで区切られた32桁 ⇒ xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx"),
                new GuidFormat("B" ,"中かっこで囲まれ、ハイフンで区切られた32桁 ⇒ {xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx"),
                new GuidFormat("P" ,"丸かっこで囲まれ、ハイフンで区切られた32桁 ⇒ (xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx)"),
            };
        }
    }
}
