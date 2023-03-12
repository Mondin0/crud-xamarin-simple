using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using AppPruebaSQL.Modelos;
using System.Threading.Tasks;

namespace AppPruebaSQL.Data
{
    public class SQliteHelper
    {
        SQLiteAsyncConnection db;

        public SQliteHelper(string dbPath)
        {
            db = new SQLiteAsyncConnection(dbPath);
            db.CreateTableAsync<Alumno>().Wait();
        }
        public Task <int> SaveAlumnoAsync(Alumno alumno)
        {
            try
            {
                if (alumno.idAlumno != 0)
                {
                    return db.UpdateAsync(alumno);
                }
                else
                {
                    return db.InsertAsync(alumno);
                }
            }
            catch (Exception e)
            {
                e.ToString();
                throw;
            }
            
        }
        /// <summary>
        /// Recuperar todos los alumnos
        /// </summary>
        /// <returns></returns>
        public Task<List<Alumno>> GetAlumnosAsync()
        {
            return db.Table<Alumno>().ToListAsync();
        }
        /// <summary>
        /// REcuperar alumno por id
        /// </summary>
        /// <param name="alumnoId">id del alumno a recuperar</param>
        /// <returns></returns>
        public Task<Alumno> GetAlumnoByid(int alumnoId)
        {
            return db.Table<Alumno>().Where(alumn => alumn.idAlumno == alumnoId).FirstOrDefaultAsync();
        }
        /// <summary>
        /// Eliminar un alumno segun el id
        /// </summary>
        /// <param name="alumno"></param>
        /// <returns></returns>
        public Task<int> DeleteAlumnoAsync(Alumno alumno)
        {
            return db.DeleteAsync(alumno);
        }
        
    }
}
