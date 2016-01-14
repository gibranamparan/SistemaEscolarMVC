using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DA2_SistemaEscolar2015.Models
{
    public class Grupo
    {
        //declaracion de llaves foraneas
        //estandar: <nombreEntidad>ID

        public int grupoID { get; set; }//Llave
        
        [Display(Name="Nombre de Grupo")]
        public String nombre { get; set; }

        //Llave foranea con Carrera
        public int carreraID { get; set; }
        public Carrera carrera { get; set; }

        //A un grupo le corresponde un conjunto de alumnos
        public virtual ICollection<Alumno> alumnos { get; set; }
    }
}