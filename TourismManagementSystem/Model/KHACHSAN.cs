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
    
    public partial class KHACHSAN
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KHACHSAN()
        {
            this.LICHTRINHs = new HashSet<LICHTRINH>();
        }
    
        public string MAKS { get; set; }
        public string TENKS { get; set; }
        public string DIACHI { get; set; }
        public string SDT { get; set; }
        public Nullable<int> SOSAO { get; set; }
        public Nullable<int> SUCCHUA { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LICHTRINH> LICHTRINHs { get; set; }
    }
}