using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using account;
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
            session.db.load_dump(new Type[]{typeof(user), typeof(certificate_class)});
            while (true)
            {
                session.menu_manager.run();                
            }
        }
    }
}