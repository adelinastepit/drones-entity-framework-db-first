using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDbFirst
{
	abstract public class Point
	{
		protected double x;
		protected double y;
		public Point(double x, double y)
		{
			this.x = x;
			this.y = y;
		}
		public abstract double Area();

		public abstract double Perimeter();
	}

	 public class Circle : Point
	{
		private double _r;

		public Circle(double x, double y, double r)	
			: base(x, y)
		{
			this._r = r;
		}

		public override double Area()
		{
			return Math.PI * _r * _r;
		}

		public override double Perimeter()
		{
			return 2 * Math.PI * _r;
		}
	}

	public class Rectangle : Point
	{
		private double width;
		private double height;

		public Rectangle(double x, double y, double width, double height)
			: base(x, y)
		{
			this.width = width;
			this.height = height;
		}

		public override double Area()
		{
			return width * height;
		}

		public override double Perimeter()
		{
			return 2 * (width + height);
		}
	}

	class Program
	{

		static void MyTransaction()
		{
			using (LibraryEntities db = new LibraryEntities())
			{
				using (System.Data.Entity.DbContextTransaction
				dbTran = db.Database.BeginTransaction())
				{
					try
					{
						string tmp = "";
						for (int i=0;i<101;i++)
						{
							tmp += i.ToString();
						}
						
						Author author = new Author
						{
							FirstName = tmp,
							LastName = "TESTTRANSACTION"
						};
						db.Author.Add(author);
						//db.Author.Remove(author);
						db.SaveChanges();
						dbTran.Commit();
					}
					catch (Exception ex)
					{
						dbTran.Rollback();
					}
				}
			}
		}

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
			Circle circle = new Circle(1, 1, 2);
			Console.WriteLine("Circle area " + circle.Area() + " perimeter "+circle.Perimeter());

			Rectangle rectangle = new Rectangle(1, 1, 4, 5);
			Console.WriteLine("Rectangle area" + rectangle.Area() + " perimeter "+ rectangle.Perimeter());
		}
	}
}
