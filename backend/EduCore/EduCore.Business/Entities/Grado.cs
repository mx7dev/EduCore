using EduCore.Business.Enums;

namespace EduCore.Business.Entities
{
    public class Grado
    {
        public int Id { get; private set; }
        public int Numero { get; private set; }
        public NivelEducativo Nivel { get; private set; }

        public Grado(int numero, NivelEducativo nivel)
        {
            if (numero < 1)
                throw new ArgumentException("El número de grado no es válido");

            if (nivel == NivelEducativo.Primaria && numero > 6)
                throw new ArgumentException("Primaria solo tiene hasta 6to grado");

            if (nivel == NivelEducativo.Secundaria && numero > 5)
                throw new ArgumentException("Secundaria solo tiene hasta 5to grado");

            Numero = numero;
            Nivel = nivel;
        }
    }
}
