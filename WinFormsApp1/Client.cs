using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1
{
    public class Client
    {
        public string id;
        public string nom;
        public string prenom;
        public string num_voi;
        public Client(string id,string nom,string prenom,string num_voi)
        {
            this.id = id;
            this.nom = nom;
            this.prenom = prenom;
            this.num_voi = num_voi;
        }
        override public string ToString()
        {
            return id+" - "+nom+" "+prenom+" - "+num_voi;
        }
    }
}
