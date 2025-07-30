using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanThoTravel.Domain.Entities.Member
{
    public class MemberEntity : BaseEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Address { get; set; }

    }
}
