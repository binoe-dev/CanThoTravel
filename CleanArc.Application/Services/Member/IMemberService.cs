using CleanArc.Domain.Entities;
using CleanArc.Domain.Entities.Member;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Service.Member
{
    public interface IMemberService
    {
        Task<List<MemberEntity>> Get();
    }
}
