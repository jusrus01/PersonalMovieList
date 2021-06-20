using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace PersonalMovieListApi.Attributes
{
    public class MaxLengthAttribute : ValidationAttribute
    {
        private readonly int len;
        public MaxLengthAttribute(int len)
        {
            this.len = len;            
        }

        public override bool IsValid(object value)
        {
            if(value is ICollection == false)
            {
                return false;
            }
            
            return ((ICollection)value).Count <= len;
        }
    }
}