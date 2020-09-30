using System;
using Hattem.CQRS.DependencyInjection;

namespace Hattem.CEP.DependencyInjection
{
    public static class CQRSBuilderExtensions
    {
        public static CQRSBuilder AddCEP(this CQRSBuilder cqrsBuilder, Action<ICEPBuilder> configure)
        {
            if (cqrsBuilder == null)
            {
                throw new ArgumentNullException(nameof(cqrsBuilder));
            }

            if (configure == null)
            {
                throw new ArgumentNullException(nameof(configure));
            }

            var cepBuilder = new CEPBuilder(cqrsBuilder.Container);

            configure(cepBuilder);

            cepBuilder.Done();

            return cqrsBuilder;
        }
    }
}
