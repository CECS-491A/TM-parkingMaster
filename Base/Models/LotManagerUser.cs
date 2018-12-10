using System.ComponentModel.DataAnnotations;

namespace Base
{
    //properties for Lot Manager User
    [Table("LotManager")]
    public class LotManager
    {
        //TODO: add security user as Properties of Manager
        [Key]
        public int? LotManagerId { get; set; }
        [ForeignKey("UserAccount")]
        public string email { get; set; }
        

        //TODO: add more Navigation Properties
        public virtual UserAccount UserAccount { get; set; }
      

        //TODO:  Constructors

    }
}
