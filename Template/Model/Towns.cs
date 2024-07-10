using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    //Smeni Town
    public class Town
    {
        //Smeni Poletata s tezi svurzani s firmata ili nesto idk
        public int Id { get; set; }
        public string Name { get; set; }
        [ForeignKey("Country")]
        public int CountryId { get; set; }
        public Country Country { get; set; }
    }
}