using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RapidORM.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class IsImage : System.Attribute
    {
        public bool Value { get; set; }

        public IsImage() { }

        public IsImage(bool isImage)
        {
            this.Value = isImage;
        }
    }
}
