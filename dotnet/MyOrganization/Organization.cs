using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOrganization
{
    internal abstract class Organization
    {
        private Position root;
        public Position Root => root;

        public Organization()
        {
            root = CreateOrganization();
        }


        protected abstract Position CreateOrganization();

        /**
         * hire the given person as an employee in the position that has that title
         * 
         * @param person
         * @param title
         * @return the newly filled position or empty if no position has that title
         */
        public Position? Hire(Name person, string title)
        {
            // kn - made changes per instructions in ReadMe.md

            Position filledPosition;
            var lastId = this.GetAllPositions().Where(p => p.GetEmployee() != null).Select(p => p.GetEmployee().GetIdentifier()).OrderBy(i => i).LastOrDefault();

            lastId++;

            filledPosition = this.GetAllPositions().SingleOrDefault(p => p.GetTitle() == title);

            filledPosition.SetEmployee(new Employee(lastId, person));

            if (filledPosition == null)
            {
                throw new Exception($"Invalid position title: { title }");
            }

            return filledPosition;
        }

        override public string ToString()
        {
            return PrintOrganization(root, "");
        }

        private string PrintOrganization(Position pos, string prefix)
        {
            // kn - made changes to include id and name

            StringBuilder sb = new StringBuilder(prefix + "+-" + pos.ToString() + "\n");
            foreach (Position p in pos.GetDirectReports())
            {
                var employee = p.GetEmployee();

                if (employee == null)
                {
                    sb.Append(PrintOrganization(p, prefix + "  "));
                }
                else
                {
                    sb.Append(PrintOrganization(p, prefix + employee.GetIdentifier() + " " + employee.GetName()));
                }
            }
            return sb.ToString();
        }
    }
}
