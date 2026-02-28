using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using ProudChickenWPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProudChickenWPF.Data
{
    internal class BrugerRepository
    {
        public bool CheckBruger(Bruger bruger)
        {
            
            using SqlConnection sqlCon = new SqlConnection(DatabaseRepository.DatabaseConnectionString);
            sqlCon.Open();
            string Query = "SELECT id FROM dbo.Bruger where BrugerNavn = @BrugerNavn AND AdgangsKode = @AdgangsKode";
            using SqlCommand cmd = new SqlCommand(Query, sqlCon);
            cmd.Parameters.AddWithValue("@BrugerNavn", bruger.BrugerNavn);
            cmd.Parameters.AddWithValue("@AdgangsKode", bruger.AdgangsKode);
            int Id = Convert.ToInt32(cmd.ExecuteScalar());

            if (Id > 0)
            {
                return true;
            }
            sqlCon.Close();
            return false;
        }
    }
}
