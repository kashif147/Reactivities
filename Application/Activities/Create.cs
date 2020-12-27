using System;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Persistence;




namespace Application.Activities
{
    public class Create
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; } 
            public string Title { get; set; }

            public string Description { get; set; }
            public string Category { get; set; }

            public DateTime Date { get; set; }
            public string City { get; set; }

            public string Venue { get; set; }

            public int ActivityID {get; set; }
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
                // Linq code 
                // _context.Activities.Add(activity);
                // var success = await _context.SaveChangesAsync( ) > 0;


                // declaring sql parameter to give sql data types to our attributes 
                SqlParameter[] param = new SqlParameter[]{
                    new SqlParameter("@pID", request.Id),
                    new SqlParameter("@pTitle", request.Title),
                    new SqlParameter("@pDescription", request.Description),
                    new SqlParameter("@pCategory", request.Category),
                    new SqlParameter("@pDate", request.Date),
                    new SqlParameter("@pCity", request.City),
                    new SqlParameter("@pVenue", request.Venue),
                    new SqlParameter("@pActivityID",System.Data.SqlDbType.Int){Direction = System.Data.ParameterDirection.Output}
                };

                var rtnStatus = await _context.Database.ExecuteSqlRawAsync("uspCreateActivity  {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7} OUT", param);
                // Console.WriteLine(rtnStatus);
                // Console.WriteLine(param[7].Value);

                if (rtnStatus != 0) return Unit.Value;

                throw new Exception("Problem saving changes");
                
            }
        }
    }
}