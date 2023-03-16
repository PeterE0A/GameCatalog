using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace GameCatalog.Data
{
    public class Game
    {
        public int Id { get; set; }
        public string GameName { get; set; }
        public string Genre { get; set; }
        public decimal Price { get; set; }

    }
}
