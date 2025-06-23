using CleanArc.Application.Repository.Member;
using CleanArc.Domain.Member;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Repository.Member
{
    public class MemberRepository : IMemberRepository
    {
        public static List<MemberDomain> lstMembers = new List<MemberDomain>()
        {
            new MemberDomain { Id = 1, Name = "John Doe", Type = "Regular", Address = "123 Main St" },
            new MemberDomain { Id = 2, Address = "456 Elm St", Name = "Jane Smith", Type = "Premium" },
            new MemberDomain { Id = 3, Address = "789 Oak St", Name = "Alice Johnson", Type = "Regular" },
            new MemberDomain { Id = 4, Address = "321 Pine St", Name = "Bob Brown", Type = "Premium" },
            new MemberDomain { Id = 5, Address = "654 Maple St", Name = "Charlie White", Type = "Regular" }
        };
        public async Task<List<MemberDomain>> Get()
        {
            return lstMembers;
        }
    }
}
