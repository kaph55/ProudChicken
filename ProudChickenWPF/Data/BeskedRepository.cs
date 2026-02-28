using Microsoft.Data.SqlClient;
using ProudChickenWPF.Model;

namespace ProudChickenWPF.Data;
public class BeskedRepository : IBeskedRepository
{
    public int AddBesked(Besked besked)
    {
        using SqlConnection sqlCon = new SqlConnection(DatabaseRepository.DatabaseConnectionString);
        sqlCon.Open();

        string query = @"INSERT INTO dbo.Besked (Indhold, SendSms, SendEmail, BegivenhedsDato)
            VALUES (@Indhold, @SendSms, @SendEmail, @BegivenhedsDato); SELECT CAST(SCOPE_IDENTITY() AS int)";

        using SqlCommand cmd = new SqlCommand(query, sqlCon);
        cmd.Parameters.AddWithValue("@Indhold", besked.Indhold);
        cmd.Parameters.AddWithValue("@SendSms", besked.SmsSend);
        cmd.Parameters.AddWithValue("@SendEmail", besked.EmailSend);
        cmd.Parameters.AddWithValue("@BegivenhedsDato", besked.BegivenhedsDato);

        int beskedId = Convert.ToInt32(cmd.ExecuteScalar());        
       
        sqlCon.Close();

        return beskedId;
    }
    public void AddBeskedTilKunde(int beskedId, int kundeId, bool smsSendt, bool emailSendt, DateTime sendtTime)
    {
        using SqlConnection sqlCon = new SqlConnection(DatabaseRepository.DatabaseConnectionString);
        sqlCon.Open();

        string query = @"INSERT INTO dbo.BeskedHistorik (BeskedId, KundeId, SendtSms, SendtEmail, SendtDato)
                         VALUES (@BeskedId, @KundeId, @SmsSendt, @EmailSendt, @DateTime)";
        using SqlCommand cmd = new SqlCommand(@query, sqlCon);
        cmd.Parameters.AddWithValue("@BeskedId", beskedId);
        cmd.Parameters.AddWithValue("@KundeId", kundeId);
        cmd.Parameters.AddWithValue("@SmsSendt", smsSendt);
        cmd.Parameters.AddWithValue("@EmailSendt", emailSendt);
        cmd.Parameters.AddWithValue("@DateTime", sendtTime);

        cmd.ExecuteNonQuery();

        sqlCon.Close();
    }
    
    public List<BeskedHistorik> SearchBeskeder(BeskedHistorikFilter filter)
    {
        List<BeskedHistorik> beskedHistorikListe = new List<BeskedHistorik>();         
        using SqlConnection sqlCon = new SqlConnection(DatabaseRepository.DatabaseConnectionString);
        sqlCon.Open();

        string query = @"SELECT 
                            b.Id AS BeskedId, 
                            b.Indhold, 
                            bh.SendtSms, 
                            bh.SendtEmail, 
                            b.BegivenhedsDato, 
                            k.KundeNavn,
                            k.VejNavn,
                            k.ByNavn
                            FROM Besked b
                            INNER JOIN BeskedHistorik bh on b.Id = bh.BeskedId
                            INNER JOIN kunde k ON k.Id = bh.KundeId
                            WHERE  
                            b.BegivenhedsDato >= @StartAt
                            AND b.BegivenhedsDato <= @EndAt
                            AND (k.PostNummer = @PostNummer or @PostNummer = '')
                            AND (k.KundeNavn like @KundeNavn or @KundeNavn = '')                             
                            AND (k.ByNavn like @ByNavn or @ByNavn = '')
                            ORDER BY b.BegivenhedsDato desc";

        using SqlCommand cmd = new SqlCommand(query, sqlCon);
        cmd.Parameters.AddWithValue("@StartAt", filter.StartAt);
        cmd.Parameters.AddWithValue("@EndAt", filter.EndAt);
        cmd.Parameters.AddWithValue("@PostNummer", filter.PostNummer);
        cmd.Parameters.AddWithValue("@KundeNavn", "%" + filter.KundeNavn + "%");        
        cmd.Parameters.AddWithValue("@ByNavn", "%" + filter.ByNavn + "%");
        using SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            BeskedHistorik beskedHistorik = new BeskedHistorik
            {

                BeskedId = Convert.ToInt32(reader["BeskedId"]),
                Indhold = reader["Indhold"].ToString(),
                SendtSms = Convert.ToBoolean(reader["SendtSms"]),
                SendtEmail = Convert.ToBoolean(reader["SendtEmail"]),
                BegivenhedsDato = Convert.ToDateTime(reader["BegivenhedsDato"]),
                KundeNavn = reader["KundeNavn"].ToString(),
                VejNavn = reader["VejNavn"].ToString(),
                ByNavn = reader["ByNavn"].ToString() 
            };

            beskedHistorikListe.Add(beskedHistorik);

        }
        sqlCon.Close();
        return beskedHistorikListe;
    }
    

}
