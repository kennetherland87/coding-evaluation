using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOrganization
{
    internal static class ExtensionMethods
    {
        public static List<Position> GetAllPositions(this Organization organization)
        {
            var list = new List<Position>();
            Action<Position> recurse = null;

            recurse = new Action<Position>(p =>
            {
                var reports = p.GetDirectReports();
                list.AddRange(reports);

                reports.ForEach(r => recurse(r));
            });

            list.Add(organization.Root);

            recurse(organization.Root);

            return list;
        }
    }
}
