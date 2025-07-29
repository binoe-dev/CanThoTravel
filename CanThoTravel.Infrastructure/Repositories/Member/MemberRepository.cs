using CanThoTravel.Application.Repository;
using CanThoTravel.Application.Repository.PostgreSQL;
using CanThoTravel.Domain.Entities.Member;
using CanThoTravel.Infrastructure.Abstracts;
using Microsoft.Extensions.Logging;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanThoTravel.Infrastructure.Repository.Member
{
    public class MemberRepository : PostgresFunctionBase<MemberEntity>, IMemberRepository
    {
        public static List<MemberEntity> lstMembers = new List<MemberEntity>()
        {
            new MemberEntity { Id = 1, Name = "John Doe", Type = "Regular", Address = "123 Main St" },
            new MemberEntity { Id = 2, Address = "456 Elm St", Name = "Jane Smith", Type = "Premium" },
            new MemberEntity { Id = 3, Address = "789 Oak St", Name = "Alice Johnson", Type = "Regular" },
            new MemberEntity { Id = 4, Address = "321 Pine St", Name = "Bob Brown", Type = "Premium" },
            new MemberEntity { Id = 5, Address = "654 Maple St", Name = "Charlie White", Type = "Regular" }
        };

        public MemberRepository(NpgsqlConnection npgsqlConnection, ITransactionManager transactionManager) : base(npgsqlConnection, transactionManager)
        {
        }

        public async Task<List<MemberEntity>> Get()
        {
            var lstParams = new Dictionary<string, object>();
            lstParams.Add("p_member_id", "1");

            var result = await ExecuteFunctionWithCursorAsync<MemberEntity>("get_member", lstParams);
            return result.ToList();
        }

        public async Task<List<MemberEntity>> GetAll()
        {

            return lstMembers;
        }
    }
}
