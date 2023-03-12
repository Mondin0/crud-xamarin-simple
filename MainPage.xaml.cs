using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using AppPruebaSQL.Data;
using AppPruebaSQL.Modelos;
using AppPruebaSQL;

namespace AppPruebaSQL
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            llenarDatos();
        }

        private async void btnRegistrar_Clicked(object sender, EventArgs e)
        {
            int edadValidada;
            if (validarDatos())
            {
                try
                {
                    edadValidada = int.Parse(txtEdad.Text);
                }
                catch (Exception)
                {
                    await DisplayAlert("error","la edad no es un numero","ok");
                    return;
                }
                Alumno alumno = new Alumno {
                    nombre = txtNombre.Text,
                    apellidoPaterno= txtApellidoMaterno.Text,
                    apellidoMaterno=txtApellidoPaterno.Text,
                    edad= edadValidada,
                    email=txtEmail.Text
                };
                await App.SQliteDB.SaveAlumnoAsync(alumno);
                limpiarControles();
                await DisplayAlert("Registro", "Se registro de manera exitosa","Ok");
                llenarDatos();
            }
            else
            {
                await DisplayAlert("Advertencia", "Error en la carga","Ok");
            }
        }
        private async void llenarDatos()
        {
            var alumnoList = await App.SQliteDB.GetAlumnosAsync();
            if (alumnoList != null)
            {
                lstAlumnos.ItemsSource = alumnoList;
            }
        }
        private bool validarDatos()
        {
            bool respuesta;

            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtApellidoPaterno.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtApellidoMaterno.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtEdad.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtEmail.Text))
            {
                respuesta = false;
            }
            else
            {
                respuesta = true;
            }
            return respuesta;
        }

        private async void btnActualizar_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtIdAlumno.Text))
            {
                Alumno alumno = new Alumno
                {
                    idAlumno = Convert.ToInt32(txtIdAlumno.Text),
                    nombre = txtNombre.Text,
                    apellidoPaterno = txtApellidoPaterno.Text,
                    apellidoMaterno = txtApellidoPaterno.Text,
                    edad = Convert.ToInt32(txtEdad.Text),
                    email=txtEmail.Text
                };
                await App.SQliteDB.SaveAlumnoAsync(alumno);
                await DisplayAlert("Actualizacion", "Se Actualizo el usuario de manera exitosa", "Ok");
                limpiarControles();
                txtIdAlumno.IsVisible = false;
                btnActualizar.IsVisible = false;
                btnRegistrar.IsVisible = true;
                llenarDatos();
            }
        }

        private async void lstAlumnos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var obj = (Alumno)e.SelectedItem;
            btnRegistrar.IsVisible = false;
            txtIdAlumno.IsVisible = true;
            btnActualizar.IsVisible = true;
            btnEliminar.IsVisible = true;
            if (!string.IsNullOrEmpty(obj.idAlumno.ToString()))
            {
                var alumno = await App.SQliteDB.GetAlumnoByid(obj.idAlumno);
                if (alumno != null)
                {
                    txtIdAlumno.Text = alumno.idAlumno.ToString();
                    txtNombre.Text = alumno.nombre;
                    txtApellidoPaterno.Text = alumno.apellidoPaterno;
                    txtApellidoMaterno.Text = alumno.apellidoMaterno;
                    txtEdad.Text = alumno.edad.ToString();
                    txtEmail.Text = alumno.email;
                }
            }
        }

        private async void btnEliminar_Clicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("eliminar", "Desea eliminar al usuario?", "si", "no");
            var alumno = await App.SQliteDB.GetAlumnoByid(Convert.ToInt32(txtIdAlumno.Text));
            if (alumno!= null && answer)
            {
                try
                {
                    await App.SQliteDB.DeleteAlumnoAsync(alumno);
                }
                catch (Exception)
                {
                    await DisplayAlert("Error", "Se produjo un error a la hora de eliminar el alumno", "Ok");
                    return;
                }
                
                await DisplayAlert("Eliminacion", "Se Elimino el usuario de manera exitosa", "Ok");
                llenarDatos();
                txtIdAlumno.IsVisible = false;
                btnActualizar.IsVisible = false;
                btnEliminar.IsVisible = false;
                btnRegistrar.IsVisible = true;

            }
        }
        private void limpiarControles()
        {
            txtIdAlumno.Text = "";
            txtNombre.Text = "";
            txtApellidoPaterno.Text = "";
            txtApellidoMaterno.Text = "";
            txtEdad.Text = "";
            txtEmail.Text = "";
        }
    }
}
