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
                var activity = new Activity
                {
                    Id = request.Id,
                    Title = request.Title,
                    Description = request.Description,
                    Category = request.Category,
                    Date = request.Date,
                    City = request.City,
                    Venue = request.Venue
                };

                // SqlParameter[] param = new SqlParameter[] {
                //     new SqlParameter("@pID", request.Id) ,
                //     new SqlParameter("@pTitle", request.Id) ,
                //     new SqlParameter("@pDescription", request.Id) ,
                //     new SqlParameter("@pCategory", request.Id) ,
                //     new SqlParameter("@pDate", request.Id) ,
                //     new SqlParameter("@pCity", request.Id) ,
                //     new SqlParameter("@pVenue", request.Id) ,
                //     new SqlParameter("@pTableId", request.Id) 

                // };
                
                // param[7].Direction =System.Data.ParameterDirection.Output;


              //  var context = new Activities(); 
             // _context.Database.ExecuteSqlCommand("spInsert_Activities @p0, @p1", parameters: new[] { "Bill", "Gates" });
              var success = await _context.Database.ExecuteSqlRawAsync("spInsert_Activities {0}, {1}, {2}, {3}, {4}, {5}, {6}",
                                                request.Id,
                                                request.Title,request.Date, request.Description, 
                                                request.Category, request.City, request.City) > 0;

               // _context.Activities.Add(activity);
               //var success = await _context.SaveChangesAsync( ) > 0;

               // if (success) return Unit.Value;
                if (success) return Unit.Value;
                else return  Unit.Value;

                //throw new Exception ("Problem saving changes" ); 
            }

        }
    }
}