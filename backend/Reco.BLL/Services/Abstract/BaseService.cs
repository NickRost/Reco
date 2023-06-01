using AutoMapper;
using Reco.DAL.Context;

namespace Reco.BLL.Services.Abstract
{
    public class BaseService
    {
        private protected readonly RecoDbContext _context;
        private protected readonly IMapper _mapper;

        public BaseService(RecoDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
    }
}
