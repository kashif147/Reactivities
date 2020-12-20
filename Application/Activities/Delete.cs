using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Persistence;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;


namespace Application.Activities
{
    public class Delete
    {
        public class Command : IRequest
        {
            public long Id { get; set; }
            
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
                //var activity = await _context.Activities.FindAsync(request.Id);

                //if (activity == null)
                  //  throw new Exception("Could not find activity");

                //_context.Remove(activity);
                
                //var success = await _context.SaveChangesAsync() > 0;

                SqlParameter[] param = new SqlParameter[] {
                    
                    new SqlParameter("@pTableId", request.Id) 

               };
                var success = await _context.Database.ExecuteSqlRawAsync("spDelete_Activities {0}",param);
                if (success !=0 )  return Unit.Value;

                //if (success) return Unit.Value;

                throw new Exception("Problem Deleting Records");
            }
        }
    }
}