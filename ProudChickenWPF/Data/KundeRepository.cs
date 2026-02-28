using Microsoft.Data.SqlClient;
using ProudChickenWPF.Model;



namespace ProudChickenWPF.Data
{
    class KundeRepository : IKundeRepository
    {
        public List<Kunde> GetAlleKunder(string postNummer)
        {
            List<Kunde> kunder = new List<Kunde>();

            using SqlConnection sqlCon = new SqlConnection(DatabaseRepository.DatabaseConnectionString);

            sqlCon.Open();
            string query = "SELECT * FROM dbo.Kunde WHERE PostNummer = @PostNummer or @PostNummer = '' order by id desc";

            using SqlCommand cmd = new SqlCommand(query, sqlCon);
            cmd.Parameters.AddWithValue("@PostNummer", postNummer);
            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Kunde kunde = new Kunde
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    KundeNavn = reader["KundeNavn"].ToString(),                    
                    VejNavn = reader["VejNavn"].ToString(),
                    PostNummer = reader["PostNummer"].ToString(),
                    ByNavn = reader["ByNavn"].ToString(),
                    TelefonNummer = reader["TelefonNummer"].ToString(),
                    Email = reader["Email"].ToString(),
                    PreferEmail = Convert.ToBoolean(reader["PreferEmail"]),
                    PreferSms = Convert.ToBoolean(reader["PreferSms"]),
                    Varm = Convert.ToBoolean(reader["Varm"]),
                    Kold = Convert.ToBoolean(reader["Kold"]),
                    Frozen = Convert.ToBoolean(reader["Frozen"])

                };
                kunder.Add(kunde);


            }

            sqlCon.Close();
            return kunder;

        }

        public void AddKunde(Kunde kunde)
        {
            using SqlConnection sqlCon = new SqlConnection(DatabaseRepository.DatabaseConnectionString);
            sqlCon.Open();
            string query = "INSERT INTO dbo.Kunde(KundeNavn, VejNavn, PostNummer, ByNavn, TelefonNummer, Email, PreferEmail, PreferSms, Kold, Frozen, Varm)" +
                             "VALUES  (@KundeNavn, @VejNavn, @PostNummer, @ByNavn, @TelefonNummer, @Email, @PreferEmail, @PreferSms, @Kold, @Frozen, @Varm)";
            using SqlCommand cmd = new SqlCommand(@query, sqlCon);
            cmd.Parameters.AddWithValue("@KundeNavn", kunde.KundeNavn);
            cmd.Parameters.AddWithValue("@VejNavn", kunde.VejNavn);
            cmd.Parameters.AddWithValue("@PostNummer", kunde.PostNummer);
            cmd.Parameters.AddWithValue("@ByNavn", kunde.ByNavn);
            cmd.Parameters.AddWithValue("@TelefonNummer", kunde.TelefonNummer);
            cmd.Parameters.AddWithValue("@Email", kunde.Email);
            cmd.Parameters.AddWithValue("@PreferEmail", kunde.PreferEmail);
            cmd.Parameters.AddWithValue("@PreferSms", kunde.PreferSms);
            cmd.Parameters.AddWithValue("@Kold", kunde.Kold);
            cmd.Parameters.AddWithValue("@Frozen", kunde.Frozen);
            cmd.Parameters.AddWithValue("@Varm", kunde.Varm);
            cmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public void OpdateKunde(Kunde kunde)
        {
            using SqlConnection sqlCon = new SqlConnection(DatabaseRepository.DatabaseConnectionString);
            sqlCon.Open();

            string query = @"UPDATE dbo.Kunde 
                            SET 
                            KundeNavn = @KundeNavn,                             
                            VejNavn = @VejNavn,
                            PostNummer = @PostNummer,
                            ByNavn = @ByNavn,
                            TelefonNummer = @TelefonNummer,
                            Email = @Email,
                            PreferEmail = @PreferEmail,
                            PreferSms = @PreferSms,
                            Kold = @Kold,
                            Frozen = @Frozen,
                            Varm = @Varm
                            WHERE Id = @Id";

            using SqlCommand cmd = new SqlCommand(query, sqlCon);
            cmd.Parameters.AddWithValue("@Id", kunde.Id);
            cmd.Parameters.AddWithValue("@KundeNavn", kunde.KundeNavn);             
            cmd.Parameters.AddWithValue("@VejNavn", kunde.VejNavn);
            cmd.Parameters.AddWithValue("@PostNummer", kunde.PostNummer);
            cmd.Parameters.AddWithValue("@ByNavn", kunde.ByNavn);
            cmd.Parameters.AddWithValue("@TelefonNummer", kunde.TelefonNummer);
            cmd.Parameters.AddWithValue("@Email", kunde.Email);
            cmd.Parameters.AddWithValue("@PreferEmail", kunde.PreferEmail);
            cmd.Parameters.AddWithValue("@PreferSms", kunde.PreferSms);
            cmd.Parameters.AddWithValue("@Kold", kunde.Kold);
            cmd.Parameters.AddWithValue("@Frozen", kunde.Frozen);
            cmd.Parameters.AddWithValue("@Varm", kunde.Varm);

            cmd.ExecuteNonQuery();
            sqlCon.Close();
        }
        public void SletKunde(int kundeId)
        {
            using SqlConnection sqlCon = new SqlConnection(DatabaseRepository.DatabaseConnectionString);
            sqlCon.Open();

            string query = "DELETE FROM dbo.Kunde WHERE Id = @Id";

            using SqlCommand cmd = new SqlCommand(query, sqlCon);
            cmd.Parameters.AddWithValue("@Id", kundeId);

            cmd.ExecuteNonQuery();
            sqlCon.Close();
        }
    }
}
