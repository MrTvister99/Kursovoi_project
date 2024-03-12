using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KP
{
    public class appsettingsFlyoutMenuItem
    {
        public appsettingsFlyoutMenuItem()
        {
            TargetType = typeof(appsettingsFlyoutMenuItem);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}