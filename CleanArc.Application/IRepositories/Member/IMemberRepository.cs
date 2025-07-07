using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArc.Domain.Entities.Member;

namespace CleanArc.Application.Repository
{
    public interface IMemberRepository
    {
        Task<List<MemberEntity>> Get();
        Task<List<MemberEntity>> GetAll();
    }
}
