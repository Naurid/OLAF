namespace DisneyBattle.WebAPI.Models
{
    namespace Stock.Models
    {
        public class PersonnageModel
        {
            public int Id { get; set; }
            public string Nom { get; set; }
            public int AlignementId { get; set; }
            public int Niveau { get; set; }
            public int Experience { get; set; }
            public int PointsVie { get; set; }
            public int PointsAttaque { get; set; }
            public int PointsDefense { get; set; }
            public int LieuId { get; set; }
        }
    }

}
