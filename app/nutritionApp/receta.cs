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
    
    public partial class receta
    {
        public receta()
        {
            this.comidas = new HashSet<comida>();
            this.ingrediente_receta = new HashSet<ingrediente_receta>();
        }
    
        public int idReceta { get; set; }
        public byte[] foto { get; set; }
        public string dificultad { get; set; }
        public int tiempo { get; set; }
        public float carbos { get; set; }
        public float proteinas { get; set; }
        public float grasas { get; set; }
        public float azucares { get; set; }
        public Nullable<int> calorias { get; set; }
        public string pasos { get; set; }
    
        public virtual ICollection<comida> comidas { get; set; }
        public virtual ICollection<ingrediente_receta> ingrediente_receta { get; set; }
    }
}
