using CanThoTravel.Domain.Entities;
using CanThoTravel.Domain.Entities.Member;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanThoTravel.Application.Service.Member
{
    public interface IMemberService
    {
        Task<List<MemberEntity>> Get();
    }
}
