namespace SchoolAssistant.Infrastructure.ModelValidation
{
    public interface IValidatorBase<TModel>
    {
        Task<bool> ValidateAsync(TModel model, CancellationToken? cancellationToken = null);
    }

    public abstract class ValidatorBase<TModel> : IValidatorBase<TModel>
    {
        protected TModel _model = default!;

        public Task<bool> ValidateAsync(TModel model, CancellationToken? cancellationToken = null)
        {
            _model = model;

            return ImplementationAsync(cancellationToken);
        }


        protected abstract Task<bool> ImplementationAsync(CancellationToken? cancellationToken = null);
    }
}
