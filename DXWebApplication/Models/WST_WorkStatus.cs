//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DXWebApplication.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class WST_WorkStatus
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public WST_WorkStatus()
        {
            this.ACC_EMP_Employee = new HashSet<ACC_EMP_Employee>();
        }
    
        public int WST_ID { get; set; }
        public string WST_Name { get; set; }
        public string WST_Name2 { get; set; }
        public int WST_Number { get; set; }
        public Nullable<int> WST_KindOfWorkStatus { get; set; }
        public string WST_Shortcut { get; set; }
        public Nullable<System.DateTime> WST_EntryDate { get; set; }
        public Nullable<System.DateTime> WST_UpdateDate { get; set; }
        public Nullable<System.DateTime> WST_DeleteDate { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ACC_EMP_Employee> ACC_EMP_Employee { get; set; }
    }
}
