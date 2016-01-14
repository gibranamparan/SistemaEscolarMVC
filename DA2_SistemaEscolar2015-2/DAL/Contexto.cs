using DA2_SistemaEscolar2015.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Web;

namespace DA2_SistemaEscolar2015.DAL
{
    public class Contexto : DbContext
    {
        public Contexto(): base("ConexionSistemaEscolar")
        {

        }

        //Defnicion de tablas a partir de las entidades
        public DbSet<Alumno> alumnos { get; set; }
        public DbSet<Carrera> carreras { get; set; }
        public DbSet<Grupo> grupos { get; set; }
    }
}