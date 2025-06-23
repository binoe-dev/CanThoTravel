using CleanArc.Domain.Member;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Repository.Member
{
    public interface IMemberRepository
    {
        Task<List<MemberDomain>> Get();
    }
}
