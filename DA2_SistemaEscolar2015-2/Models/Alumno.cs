using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DA2_SistemaEscolar2015.Models
{
    public class Alumno
    {
        //Data Annotation
        [Key]//Indicando que noMatricula es llave
        public int noMatricula { get; set; }

        public String nombre { set; get; }
            
        public String apellidoP { get; set; }
        public String apellidoM { get; set; }
        public DateTime fechaNac { get; set; }

        public int grupoID { get; set; }//Llave foranea
        public virtual Grupo grupo { get; set; }
    }

    /*Representa la definicion de un modelo para vista (ViewModel),
     * por lo que no contiene información de relaciones con otras entidades*/
    public class VMAlumno
    {
        public int noMatricula { get; set; }
        public String nombre { set; get; }

        public String apellidoP { get; set; }
        public String apellidoM { get; set; }
        public DateTime fechaNac { get; set; }

        public int grupoID { get; set; }
        public String nombreGrupo { get; set; }

        public VMAlumno(Alumno alumno)
        {
            this.noMatricula = alumno.noMatricula;
            this.nombre = alumno.nombre;
            this.apellidoM = alumno.apellidoM;
            this.apellidoP = alumno.apellidoP;
            this.fechaNac = alumno.fechaNac;
            this.grupoID = alumno.grupoID;

            if(alumno.grupo != null)
                this.nombreGrupo = alumno.grupo.nombre;

        }
    }



    
    
    
    
    
    
    
    
    
    /* public class VMAlumno
    {
        //Data Annotation
        public int noMatricula { get; set; }

        public String nombreCompleto { get; set; }
        public DateTime fechaNac { get; set; }

        public string nombreGrupo { get; set; }

        public VMAlumno(Alumno alumno) {
            this.noMatricula = alumno.noMatricula;
            this.nombreCompleto = alumno.nombre + " " + alumno.apellidoP + " " + alumno.apellidoM;
            this.fechaNac = alumno.fechaNac;

            if (alumno.grupo == null)
                this.nombreGrupo = "";
            else
                this.nombreGrupo = alumno.grupo.nombre;
        }
    }*/
}