//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TourismManagementSystem.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class LOAITUYEN
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LOAITUYEN()
        {
            this.TUYENs = new HashSet<TUYEN>();
        }
    
        public string MALOAI { get; set; }
        public string TENLOAI { get; set; }
        public Nullable<int> TGMUATRUOCKHOIHANH { get; set; }
        public Nullable<int> TGHOANVEMIENPHI { get; set; }
        public Nullable<int> LEPHIHOANVETRE { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TUYEN> TUYENs { get; set; }
    }
}