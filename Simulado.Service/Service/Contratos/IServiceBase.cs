namespace Simulado.Service.Service.Contratos
{
    public interface IServiceBase<DTO, D> : IServiceEstatico<D> where D : class
    {
        Task<bool> Add(DTO entidade);
    }
}
