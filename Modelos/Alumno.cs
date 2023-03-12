using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace AppPruebaSQL.Modelos
{
    public class Alumno
    {
        [PrimaryKey, AutoIncrement]
        public int idAlumno { get; set; }
        [MaxLength(50)]
        public string nombre { get; set; }
        [MaxLength(50)]
        public string apellidoPaterno{ get; set; }
        [MaxLength(50)]
        public string apellidoMaterno { get; set; }
        public int edad { get; set; }

        [MaxLength(100)]
        public string email { get; set; }
    }
}
