namespace HeavyArchives.Domain.Entities.M2MTables
{
    public class BandPerson
    {
        public int Id{ get; set; }
        public int BandId{ get; set; }
        public int PersonId{ get; set; }
        public Band Band{ get; set; }
        public Person Person { get; set; }
        public int StatusID { get; set; }
        public BandMemberStatus Status{ get; set; }//jel bi ovdje trebao onda bit Status - ako je onda ne moze u mapperu usporedit klasu i string
        public string RoleInBand{ get; set; } //koji kojem je kapacitetu zaposlen (vocals, drums, guitar...)
    }
}
