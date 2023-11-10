using Application.Features.UserOperationClaims.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using Domain.Concrete;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Application.Features.UserOperationClaims.Constants.OperationClaims;
using static Domain.Constants.OperationClaims;

namespace Application.Features.UserOperationClaims.Queries.GetListUserOperationClaimByDynamic;

public class GetListUserOperationClaimByDynamicQuery : IRequest<UserOperationClaimListModel>, ISecuredRequest
{
    public PageRequest PageRequest { get; set; }
    public DynamicQuery Dynamic { get; set; }
    public string[] Roles => new[] { ADMIN, UserOperationClaimGet };

    public class GetListUserOperationClaimByDynamicQueryHandler : IRequestHandler<GetListUserOperationClaimByDynamicQuery, UserOperationClaimListModel>
    {
        private readonly IUserOperationClaimRepository _userOperationClaimDal;
        private readonly IMapper _mapper;

        public GetListUserOperationClaimByDynamicQueryHandler(IUserOperationClaimRepository userOperationClaimDal, IMapper mapper)
        {
            _userOperationClaimDal = userOperationClaimDal;
            _mapper = mapper;
        }

        public async Task<UserOperationClaimListModel> Handle(GetListUserOperationClaimByDynamicQuery request, CancellationToken cancellationToken)
        {
            IPaginate<UserOperationClaim> userOperationClaims = await _userOperationClaimDal.GetListByDynamicAsync(
                request.Dynamic,
                include: c => c.Include(c => c.User).Include(c => c.OperationClaim),
                index: request.PageRequest.Page,
                size: request.PageRequest.PageSize
            );

            // Mapping the user operation claims to a UserOperationClaimListModel object
            UserOperationClaimListModel mappedUserOperationClaimListModel = _mapper.Map<UserOperationClaimListModel>(userOperationClaims);

            return mappedUserOperationClaimListModel;

        }
    }
}
