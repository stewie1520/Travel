using MediatR;
using System.Threading.Tasks;
using System.Threading;

using Travel.Domain.Entities;
using Travel.Application.Common.Interfaces;

namespace Travel.Application.TourLists.Commands.CreateTourList
{
    public partial class CreateTourListCommand : IRequest<int>
    {
        public string City { get; set; }
        public string Country { get; set; }
        public string About { get; set; }
    }

    public class CreateTourListCommandHandler : IRequestHandler<CreateTourListCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateTourListCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateTourListCommand request, CancellationToken cancellationToken)
        {
            var entity = new TourList { City = request.City };
            _context.TourLists.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
