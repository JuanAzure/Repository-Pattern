
namespace Entities.RequestFeatures
{
    public class ArticuloParameters : RequestParameters
    {

        public ArticuloParameters()
        {
            OrderBy = "nombre";
        }
        public uint MinStock { get; set; }
        public uint MaxStock { get; set; } = int.MaxValue;
        public bool ValidaStockRange => MaxStock > MinStock;
        public string SearchTerm { get; set; }

    }
}
