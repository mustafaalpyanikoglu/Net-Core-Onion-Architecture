using Application.Features.UserOperationClaims.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Concrete;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Application.Features.UserOperationClaims.Constants.OperationClaims;
using static Domain.Constants.OperationClaims;

namespace Application.Features.UserOperationClaims.Queries.GetListUserOperationClaim;

public class GetListUserUperationClaimQuery : IRequest<UserOperationClaimListModel>//, ISecuredRequest
{
    public PageRequest PageRequest { get; set; }
    public string[] Roles => new[] { ADMIN };

    public class GetListUserUperationClaimQueryHandler : IRequestHandler<GetListUserUperationClaimQuery, UserOperationClaimListModel>
    {
        private readonly IUserOperationClaimRepository _userOperationClaimDal;
        private readonly IMapper _mapper;

        public GetListUserUperationClaimQueryHandler(IUserOperationClaimRepository userOperationClaimDal, IMapper mapper)
        {
            _userOperationClaimDal = userOperationClaimDal;
            _mapper = mapper;
        }

        public async Task<UserOperationClaimListModel> Handle(GetListUserUperationClaimQuery request, CancellationToken cancellationToken)
        {
            IPaginate<UserOperationClaim> userOperationClaims =
                await _userOperationClaimDal.GetListAsync(
                include: c => c.Include(c => c.User).Include(c => c.OperationClaim),
                index: request.PageRequest.Page,
                size: request.PageRequest.PageSize
            );

            UserOperationClaimListModel mappedUserOperationClaimListModel =
                _mapper.Map<UserOperationClaimListModel>(userOperationClaims);

            return mappedUserOperationClaimListModel;

        }
    }
}
