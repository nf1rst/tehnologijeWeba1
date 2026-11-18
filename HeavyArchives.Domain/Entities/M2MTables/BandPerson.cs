namespace HeavyArchives.Domain.Entities.M2MTables
{
    public class BandPerson
    {
        public int Id{ get; set; }
        public int BandId{ get; set; }
        public int PersonId{ get; set; }
        public Band Band{ get; set; } = new Band();
        public Person Person { get; set; } = new Person();
        public int StatusID { get; set; }
        public BandMemberStatus Status { get; set; } = new BandMemberStatus();//jel bi ovdje trebao onda bit Status - ako je onda ne moze u mapperu usporedit klasu i string
        public string RoleInBand { get; set; } = String.Empty;//kojiuloga u  bendu(vocals, drums, guitar...)
    }
}
