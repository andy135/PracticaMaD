//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Es.Udc.DotNet.PracticaMaD.Model
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    public partial class Category
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Category()
        {
            this.Event = new HashSet<Event>();
        }
    
        public long categoryId { get; set; }
        public string categoryName { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Event> Event { get; set; }

        public override int GetHashCode()
        {
            unchecked
            {
                int multiplier = 31;
                int hash = GetType().GetHashCode();

                hash = hash * multiplier + categoryId.GetHashCode();
                hash = hash * multiplier + (categoryName == null ? 0 : categoryName.GetHashCode());

                return hash;
            }

        }

        public override bool Equals(object obj)
        {
            Category target = (Category)obj;

            return true
               && (this.categoryId == target.categoryId)
               && (this.categoryName == target.categoryName);

        }

        public override String ToString()
        {
            StringBuilder strUserProfile = new StringBuilder();

            strUserProfile.Append("[ ");
            strUserProfile.Append(" categoryId = " + categoryId + " | ");
            strUserProfile.Append(" categoryName = " + categoryName + " | ");
            strUserProfile.Append("] ");

            return strUserProfile.ToString();
        }

    }
}
