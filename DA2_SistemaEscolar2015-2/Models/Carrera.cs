using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DA2_SistemaEscolar2015.Models
{
    public class Carrera
    {
        public int carreraID { get; set; }
        public String nombre { get; set; }

        //A una carrera le corresponde muchos grupos
        public ICollection<Grupo> grupos {get;set;}

    }
}