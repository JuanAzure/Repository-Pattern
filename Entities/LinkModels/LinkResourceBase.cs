using System.Collections.Generic;

namespace Entities.LinkModels
{
    public class LinkResourceBase
    {
        public LinkResourceBase()
        {

        }

        public List<Link> links { get; set; } = new List<Link>();
        
    }
}
