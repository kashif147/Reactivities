using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Domain;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Activities
{
    public class Delete
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }

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
                // Linq Code
                // var activity = await _context.Activities.FindAsync(request.Id);
                // _context.Remove(activity);

                // var success = await _context.SaveChangesAsync() > 0;

                var activity = await _context.Activities.FromSqlRaw<Activity>("uspLoadActivitybyID {0}", request.Id)
                .ToListAsync();

                if (activity.Count == 0)
                    throw new RestException(HttpStatusCode.NotFound, new { activity = "Not Found" });


                SqlParameter[] param = new SqlParameter[]{
                    new SqlParameter("@pID", request.Id),
                    new SqlParameter("@pRowsOut",System.Data.SqlDbType.Int){Direction = System.Data.ParameterDirection.Output}
                };

                await _context.Database.ExecuteSqlRawAsync("uspDeleteActivity  {0}, {1} OUT", param);
                
                if (Int32.Parse(param[1].Value.ToString()) > 0) return Unit.Value; 

                throw new Exception("Problem saving changes");
            }
        }
    }
}