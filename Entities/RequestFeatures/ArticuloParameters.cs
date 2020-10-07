
namespace Entities.RequestFeatures
{
    public class ArticuloParameters : RequestParameters
    {
        public uint MinStock { get; set; }
        public uint MaxStock { get; set; } = int.MaxValue;
        public bool ValidaStockRange => MaxStock > MinStock;
    }
}
