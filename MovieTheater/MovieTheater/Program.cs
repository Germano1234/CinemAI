using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MovieTheater.View;
namespace MovieTheater
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new FrmBase());
        }
        public static String connString = "Server=.\\SQLEXPRESS;Database=MovieTheater;UID=sa;PWD=123;TrustServerCertificate=True;";

    }
}