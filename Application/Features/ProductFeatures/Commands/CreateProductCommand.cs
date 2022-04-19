using Application.Interfaces;
using Application.Wrappers;
using Domain.Entities;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.ProductFeatures.Commands
{
    //public class CreateProductCommand: IRequest<int>
    public class CreateProductCommand : IRequest<ResponseApi>
    {
        public string Name { get; set; }
        public string BarCode { get; set; }
        public string Description { get; set; }
        public decimal Rate { get; set; }

        public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ResponseApi>
        {
            private readonly IApplicationDbContext context;

            public CreateProductCommandHandler(IApplicationDbContext context)
            {
                this.context = context;
            }
           
            public async Task<ResponseApi> Handle(CreateProductCommand request, CancellationToken cancellationToken)
            {
                var product = new Product()
                {
                    BarCode = request.BarCode,
                    Description = request.Description,
                    Rate = request.Rate,
                    Name = request.Name
                };
                //context.Products.Add(product);
                //await context.SaveChangesAsync();
                return new ResponseApi(product);
            }
        }

        //public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
        //{
        //    private readonly IApplicationDbContext context;

        //    public CreateProductCommandHandler(IApplicationDbContext context)
        //    {
        //        this.context = context;
        //    }
        //    public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        //    {
        //        var product = new Product() { 
        //         BarCode = request.BarCode,
        //         Description = request.Description,
        //         Rate = request.Rate,
        //         Name = request.Name
        //        };
        //        context.Products.Add(product);
        //        await context.SaveChangesAsync();
        //        return product.Id;
        //    }
        //}

        public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand> {
            public CreateProductCommandValidator()
            {
                RuleFor(c=>c.BarCode).NotEmpty();
                RuleFor(c=>c.Name).NotEmpty();
            }
        }
    }
}
