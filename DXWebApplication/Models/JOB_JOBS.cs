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
    
    public partial class JOB_JOBS
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public JOB_JOBS()
        {
            this.ACC_EMP_Employee = new HashSet<ACC_EMP_Employee>();
        }
    
        public int JOB_ID { get; set; }
        public string JOB_Name { get; set; }
        public string JOB_Name2 { get; set; }
        public string JOB_Shortcut { get; set; }
        public int JOB_Number { get; set; }
        public int JOB_Gender { get; set; }
        public bool JOB_IsDeleted { get; set; }
        public Nullable<System.DateTime> JOB_EntryDate { get; set; }
        public Nullable<System.DateTime> JOB_UpdateDate { get; set; }
        public Nullable<System.DateTime> JOB_DeleteDate { get; set; }
        public byte[] JOB_File { get; set; }
        public string JOB_FileName { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ACC_EMP_Employee> ACC_EMP_Employee { get; set; }
    }
}
