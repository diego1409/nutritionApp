//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace nutritionApp
{
    using System;
    using System.Collections.Generic;
    
    public partial class imc
    {
        public int idIMC { get; set; }
        public int idUsuario { get; set; }
        public byte estatura { get; set; }
        public float peso { get; set; }
        public float resultado { get; set; }
        public string observaciones { get; set; }
    
        public virtual usuario usuario { get; set; }
    }
}
