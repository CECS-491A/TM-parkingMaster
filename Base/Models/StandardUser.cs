using System.ComponentModel.DataAnnotations;

namespace Base
{
    // Standard user properties
    [Table("StandardUser")]
    public class StandardUser
    {
        //TODO: add more Properties
        [ForeignKey("UserAccount")]
        public string email { get; set; }

        //TODO: add maybe more Navigation Properties
        public virtual UserAccount UserAccount { get; set; }

        //TODO: add Constructors
        public StandardUser() { }

      
    }
}
