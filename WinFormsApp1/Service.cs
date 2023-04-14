using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1
{
    public class Service
    {
        public string id;
        public string nom;
        public string prix;
        public Service(string id,string nom,string prix)
        {
            this.id = id;
            this.nom = nom;
            this.prix = prix;
        }
    }
}
