using SchoolAssistant.Infrastructure.Models.Shared.Json;

namespace SchoolAssistant.Infrastructure.ModelValidation
{
    public interface IValidatorBaseWithResp<TModel, TResponse> : IValidatorBase<TModel>
        where TResponse : ResponseJson, new()
    {
        TResponse Response { get; }

    }


    public abstract class ValidatorBaseWithResp<TModel, TResponse> : ValidatorBase<TModel>
        where TResponse : ResponseJson, new()
    {

        public TResponse Response { get; protected set; } = null!;

        protected override Task<bool> ImplementationAsync(CancellationToken? cancellationToken = null)
        {
            Response = new();

            return ImplementationIIAsync();
        }

        protected abstract Task<bool> ImplementationIIAsync(CancellationToken? cancellationToken = null);

        /// <param name="msg"> Message for response </param>
        /// <returns> <c>false</c> </returns>
        protected bool FalseWithMsgRegistration(string msg)
        {
            Response.message = msg;
            return false;
        }
    }
}
