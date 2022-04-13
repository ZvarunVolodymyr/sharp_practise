using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using CertificateClass;
using conteiner;
using db_imitator;
using helping;
using validation;

namespace Main
{
    public class MainMenu
    {
        public static void Main(string[] args)
        {
            session.start_session();

            while (true)
            {
                session.menu_manager.run();                
            }
        }
    }
}