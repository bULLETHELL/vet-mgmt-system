//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace vet_mgmt_system
{
    using System;
    using System.Collections.Generic;
    
    public partial class PatientsMedicalProcedure
    {
        public int PatientID { get; set; }
        public int ProcedureID { get; set; }
        public System.DateTime Date { get; set; }
    
        public virtual Date Date1 { get; set; }
        public virtual MedicalProcedure MedicalProcedure { get; set; }
        public virtual Patient Patient { get; set; }
    }
}