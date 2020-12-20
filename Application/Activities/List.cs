using System;
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
        public class Query : IRequest<List<Activity>> { 
            public Guid Id { get; set; }
        }

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
                var activities = await _context.Activities.FromSqlRaw<Activity>("spGetActivitiesAll").ToListAsync();

                return activities;
            }
        }
    }
}