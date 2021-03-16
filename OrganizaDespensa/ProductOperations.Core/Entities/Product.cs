using OrganizaDespensa.SharedKernel.Core.Entities;

namespace ProductOperations.Core.Entities
{
    public  class Product:Entity
    {
        public ProductData ProductData { get; private set; }
        public DateTimeProduct DateTimeProduct { get; private set; }
        public ProductInformation ProductInformation { get; private set; }
        public Category Category { get; private set; }
        public string Status { get; private set; }
        public string UserId { get; private set; }

        protected Product() { }

        public Product(ProductData productData, DateTimeProduct dateTimeProduct, ProductInformation productInformation,
           Category category, string userId)
        {
            ProductData = productData;
            DateTimeProduct = dateTimeProduct;
            ProductInformation = productInformation;
            Category = category;
            Status = StatusProduto.QuantidadeIdeal.ToString();
            UserId = usuario;
        }

        public bool VerificarSeProdutoEstaNaValidade()
        {
            var dataAtual = DateTime.Now;

            if (DatasDoProduto.DataDeValidade > dataAtual)
                return true;

            return false;
        }

        public void AlterarStatus(string status) => Status = status;

        public void AlterarCategoria(Categoria categoria) => Category = categoria;

        public void AlterarDadosDoProduto(DadosProduto dadosDoProduto) => DadosDoProduto = dadosDoProduto;

        public void AlterarDatasDoProduto(DatasProduto datasProduto) => DatasDoProduto = datasProduto;

        public void AlterarInformacoesDoProduto(InformacaoProduto informacaoProduto)
        {
            InformacoesDoProduto = informacaoProduto;

            if (informacaoProduto.Quantidade == 0)
                Status = StatusProduto.Comprar.ToString();
        }
    }
}
