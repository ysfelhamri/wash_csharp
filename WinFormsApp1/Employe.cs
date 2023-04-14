using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLavage
{
    public class Employe
    {
        public string id;
        public string nom;
        public string prenom;
        public Employe(string id,string nom,string prenom) {
            this.id = id;
            this.nom = nom;
            this.prenom = prenom;
        }
        override public string ToString()
        {
            return id + " - " + nom + " " + prenom;
        }
    }
}
