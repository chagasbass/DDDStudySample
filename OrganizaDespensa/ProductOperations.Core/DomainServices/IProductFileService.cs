namespace ProductOperations.Core.DomainServices
{
    public interface IProductFileService
    {
        byte[] ProcessProductImage(string imageBase64);
    }
}
