using Contracts;
using System.Text;

namespace PatterRepository.Utility
{
    public  class TemplateGenerator
    {
        private readonly IRepositoryWrapper _repoWrapper;

        public TemplateGenerator(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }
       

        public string GetHTMLString()
        {
            //var employees = DataStorage.GetAllEmployess();
            var employees = _repoWrapper.Owner.FindAll(trackChanges:false);
            var sb = new StringBuilder();
            sb.Append(@"
                        <html>
                            <head>
                            </head>
                            <body>
                                <div class='header'><h1>This is the generated PDF report!!!</h1></div>
                                <table align='center'>
                                    <tr>
                                        <th>OwnerId</th>
                                        <th>DateOfBirth</th>
                                        <th>Name</th>
                                        <th>Address</th>
                                    </tr>");

            foreach (var emp in employees)
            {
                sb.AppendFormat(@"<tr>
                                    <td>{0}</td>
                                    <td>{1}</td>
                                    <td>{2}</td>  
                                    <td>{3}</td>                                    

                                  </tr>", emp.Id, emp.DateOfBirth.ToShortDateString(),emp.Name,emp.Address);
            }

            sb.Append(@"
                                </table>
                            </body>
                        </html>");

            return sb.ToString();
        }
    }
}
