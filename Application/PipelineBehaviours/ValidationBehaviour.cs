﻿using Application.Wrappers;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.PipelineBehaviours
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
        where TResponse: ResponseApi, new()
    {
        private readonly IEnumerable<IValidator<TRequest>> validators;

        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            this.validators = validators;
        }
       
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);
                var validationResults = await Task.WhenAll(validators.Select(v => v.ValidateAsync(context, cancellationToken)));
                var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();
                if (failures.Count != 0)
                {
                    var response = new ResponseApi
                    {

                        Erros = failures
                    };
                    return response as TResponse;
                }
            }

            return await next();
        }
    }

    //public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    //{
    //    private readonly IEnumerable<IValidator<TRequest>> validators;

    //    public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
    //    {
    //        this.validators = validators;
    //    }
    //    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    //    {
    //        if (validators.Any())
    //        {
    //            var context = new ValidationContext<TRequest>(request);
    //            var validationResults = await Task.WhenAll(validators.Select(v => v.ValidateAsync(context, cancellationToken)));
    //            var failures = validationResults.SelectMany(r=>r.Errors).Where(f=>f!=null).ToList();
    //            if (failures.Count != 0) throw new ValidationException(failures);
    //        }

    //        return await next();
    //    }
    //}
}
