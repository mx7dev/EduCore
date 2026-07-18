using System;
using System.Collections.Generic;
using System.Text;

namespace EduCore.Business.Entities
{
    public class Curso
    {
        public int Id { get; private set; }
        public string Codigo { get; private set; }
        public string Nombre { get; private set; }
        public string? Descripcion { get; private set; }
        public bool Activo { get; private set; }

        public Curso(string codigo, string nombre, string? descripcion)
        {
            if (string.IsNullOrWhiteSpace(codigo))
                throw new ArgumentException("El código es obligatorio");

            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre es obligatorio");

            Codigo = codigo;
            Nombre = nombre;
            Descripcion = descripcion;
            Activo = true;
        }

        public void Desactivar()
        {
            Activo = false;
        }

        public void ActualizarNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre no puede estar vacío");

            Nombre = nombre;
        }
    }
}
