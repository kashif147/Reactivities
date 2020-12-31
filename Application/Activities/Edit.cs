using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Activities
{
    public class Edit
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
            public string Title { get; set; }

            public string Description { get; set; }
            public string Category { get; set; }

            public DateTime? Date { get; set; }
            public string City { get; set; }

            public string Venue { get; set; }

            public int ActivityID { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Title).NotEmpty();
                RuleFor(x => x.Description).NotEmpty();
                RuleFor(x => x.Category).NotEmpty();
                RuleFor(x => x.Date).NotEmpty();
                RuleFor(x => x.City).NotEmpty();
                RuleFor(x => x.Venue).NotEmpty();
            }
        }
        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {

                var activity = await _context.Activities.FromSqlRaw<Activity>("uspLoadActivitybyID {0}", request.Id)
                                               .ToListAsync();

                if (activity.Count == 0)
                    throw new RestException(HttpStatusCode.NotFound, new { activity = "Not Found" });
                    
                SqlParameter[] param = new SqlParameter[]{
                    new SqlParameter("@pID", request.Id),
                    new SqlParameter("@pTitle", request.Title),
                    new SqlParameter("@pDescription", request.Description),
                    new SqlParameter("@pCategory", request.Category),
                    new SqlParameter("@pDate", request.Date),
                    new SqlParameter("@pCity", request.City),
                    new SqlParameter("@pVenue", request.Venue),
                    new SqlParameter("@pActivityID",request.ActivityID)
                };

                var rtnStatus = await _context.Database.ExecuteSqlRawAsync("uspUpdateActivity  {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}", param);

                if (rtnStatus != 0) return Unit.Value;
                throw new Exception("Problem saving changes");
            }
        }
    }
}