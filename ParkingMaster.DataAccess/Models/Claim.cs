using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingMaster.DataAccess.Models
{
    public class Claim : IEquatable<Claim>
    {
        [Key]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Value { get; set; }
        public ICollection<Client> Clients { get; set; }

        public Claim(string t, string v)
        {
            Title = t;
            Value = v;
            Id = Guid.NewGuid();
            Clients = new List<Client>();

        }

        public Claim()
        {
            Title = "DEFAULT";
            Value = "DEFAULT";
            Id = Guid.NewGuid();
            Clients = new List<Client>();
        }

        public Boolean Equals(Claim obj)
        {
            if(obj == null)
            {
                return false;
            }

            Claim otherClaim = obj as Claim;

            if(otherClaim != null)
            {
                if (this.Title.Equals(otherClaim.Title))
                {
                    return this.Value.Equals(otherClaim.Value);
                };
            }

            return false;
        }
    }
}

