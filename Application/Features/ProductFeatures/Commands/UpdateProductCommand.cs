using Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.ProductFeatures.Commands
{
    public class UpdateProductCommand: IRequest<int>
    {
        public int Id { get; set;}
        public string Name { get; set; }
        public string BarCode { get; set; }
        public string Description { get; set; }
        public decimal Rate { get; set; }

        public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, int>
        {
            private readonly IApplicationDbContext context;

            public UpdateProductCommandHandler(IApplicationDbContext context)
            {
                this.context = context;
            }
            public async Task<int> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
            {
                var product = context.Products.FirstOrDefault(x => x.Id == request.Id);

                if (product == null) return default;

                product.Name = request.Name;
                product.BarCode = request.BarCode;
                product.Description = request.Description;
                product.Rate = request.Rate;
                await context.SaveChangesAsync();

                return product.Id;
            }
        }
    }
}
