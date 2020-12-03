using System;

namespace Entities.Models
{
    public class ShapedEntity
    {
        public ShapedEntity()
        {
            Entity = new Entity();
        }
        public int  articuloId { get; set; }
        public Entity Entity { get; set; }
    }
}
