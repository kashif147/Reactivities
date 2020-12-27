using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Activities
{
    public class List
    {
        public class Query : IRequest<List<Activity>> { }


        public class Handler : IRequestHandler<Query, List<Activity>>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;

            }

            public async Task<List<Activity>> Handle(Query request,
                CancellationToken cancellationToken)
            {
                // var activities = await _context.Activities.FromSqlRaw<Activity>("uspLoadActivities").ToListAsync();
                var activities = await _context.Activities.FromSqlRaw<Activity>("uspLoadActivities").ToListAsync();
                // var activities = await _context.Activities.FromSqlRaw<Activity>("uspLoadActivitybyID {0}", "06d4ff44-5c4a-4ac1-abac-73db68185d31").ToListAsync();

                return activities;
            }
        }
    }
}