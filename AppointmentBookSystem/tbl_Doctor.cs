//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AppointmentBookSystem
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_Doctor
    {
        internal object tbl_specialization;

        public int Doctor_Id { get; set; }
        public string Doctor_Name { get; set; }
        public string Doctor_Username { get; set; }
        public string Doctor_Password { get; set; }
        public string Doctor_Gender { get; set; }
        public string Doctor_Phone { get; set; }
        public string Doctor_Address { get; set; }
        public Nullable<bool> Doctor_IsAvailable { get; set; }
        public Nullable<int> Doctor_Specialization_Id { get; set; }
    }
}