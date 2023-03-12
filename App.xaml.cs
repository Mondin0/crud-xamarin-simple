using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AppPruebaSQL.Data;
using System.IO;

namespace AppPruebaSQL
{
    public partial class App : Application
    {
        static SQliteHelper db;
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        public static SQliteHelper SQliteDB
        {
            get
            {
                if ( db == null)
                {
                    try
                    {
                        db = new SQliteHelper(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Escuela.db3"));
                    }
                    catch (Exception e)
                    {
                        e.ToString();
                    }
                }
                return db;
            }
        }
        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
