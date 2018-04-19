using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDbFirst
{
	class Program
	{
		static void AddPackage(Package package)
		{
			using (LibraryEntities db = new LibraryEntities())
			{
				db.Package.Add(package);
				db.SaveChanges();
				Console.WriteLine(package.Name+ " ");
			}
		}


		static void Main(string[] args)
		{

			Package package = new Package
			{
				Name = "X"
			};
			AddPackage(package);
			
		}
	}
}
