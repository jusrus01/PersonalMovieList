using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace PersonalMovieListApi.Attributes
{
    public class LengthAttribute : ValidationAttribute
    {
        private readonly int len;
        public LengthAttribute(int len)
        {
            this.len = len;            
        }

        public override bool IsValid(object value)
        {
            if(value is ICollection == false)
            {
                return false;
            }
            
            return ((ICollection)value).Count == len;
        }
    }
}