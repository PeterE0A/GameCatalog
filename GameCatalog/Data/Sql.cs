using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace GameCatalog.Data
{
    public class Sql
    {
        //string myDb1ConnectionString = _configuration.GetConnectionString("myDb1");
        public static string? connectionString;// = "Data Source= .;Initial Catalog = MemeDB; User ID = sa; Password=Passw0rd;";

        public static List<Game> Read()
        {
            List<Game> list = new List<Game>();
            SqlConnection con = new SqlConnection (connectionString);
            SqlCommand cmd = new SqlCommand("SELECT * FROM gameTable", con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Game game = new Game() { Id = dr.GetInt32(0), GameName = dr.GetString(1), Genre = dr.GetString(2), Price = dr.GetDecimal(3) };
                list.Add(game);
            }
            con.Close();

            return list;
        }

        public static void Create(Game game)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new("INSERT INTO gameTable (Name, Genre, Price) VALUES (@gameName, @gameGenre, @gamePrice)", conn);
                cmd.Parameters.Add("@gameName", SqlDbType.NVarChar).Value = game.GameName;
                cmd.Parameters.Add("@gameGenre", SqlDbType.NVarChar).Value = game.Genre;
                cmd.Parameters.Add("@gamePrice", SqlDbType.Decimal).Value = game.Price;
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                game.GameName = "";
                game.Genre = "";
                game.Price = 0;

            }
        }

        public static void Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new("DELETE FROM gameTable WHERE Id = @id", conn);
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }


        public static void Update(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new("UPDATE FROM gameTable WHERE Id = @id", conn);
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }


    }
}
