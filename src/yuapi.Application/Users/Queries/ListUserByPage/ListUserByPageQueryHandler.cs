using AutoMapper;
using MediatR;
using yuapi.Application.Common.Interfaces.Persistence;
using yuapi.Application.Common.Models;
using yuapi.Application.Users.Common;
using yuapi.Domain.UserAggregate;

namespace yuapi.Application.Users.Queries.ListUserByPage
{
    public class ListUserByPageQueryHandler : 
        IRequestHandler<ListUserByPageQuery, PaginatedList<UserSafetyResult>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public ListUserByPageQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<PaginatedList<UserSafetyResult>> Handle(ListUserByPageQuery query, CancellationToken cancellationToken)
        {
            //var currentPage = query.Current == 0 ? 1 : query.Current;
            //var pageSize = query.PageSize == 0 ? 10 : query.PageSize;
            // Apply default values if necessary
            query.ApplyDefaults();

            User user = _mapper.Map<User>(query);
            //user.isDelete = false;

            var paginatedResult = await _userRepository.ListByPage(
                user,
                query.Current.Value,
                query.PageSize.Value,
                query.SortField,
                query.SortOrder);

            var result = _mapper.Map<List<UserSafetyResult>>(paginatedResult.Items);

            return new PaginatedList<UserSafetyResult>(result, paginatedResult.TotalCount, query.Current.Value, query.PageSize.Value);
        }
    }
}
