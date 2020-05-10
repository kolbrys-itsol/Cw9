using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqConsoleApp
{
    public class LinqSamples
    {
        public static IEnumerable<Emp> Emps { get; set; }
        public static IEnumerable<Dept> Depts { get; set; }

        public LinqSamples()
        {
            LoadData();
        }

        public void LoadData()
        {
            var empsCol = new List<Emp>();
            var deptsCol = new List<Dept>();

            #region Load depts

            var d1 = new Dept
            {
                Deptno = 1,
                Dname = "Research",
                Loc = "Warsaw"
            };

            var d2 = new Dept
            {
                Deptno = 2,
                Dname = "Human Resources",
                Loc = "New York"
            };

            var d3 = new Dept
            {
                Deptno = 3,
                Dname = "IT",
                Loc = "Los Angeles"
            };

            deptsCol.Add(d1);
            deptsCol.Add(d2);
            deptsCol.Add(d3);
            Depts = deptsCol;

            #endregion

            #region Load emps

            var e1 = new Emp
            {
                Deptno = 1,
                Empno = 1,
                Ename = "Jan Kowalski",
                HireDate = DateTime.Now.AddMonths(-5),
                Job = "Backend programmer",
                Mgr = null,
                Salary = 2000
            };

            var e2 = new Emp
            {
                Deptno = 1,
                Empno = 20,
                Ename = "Anna Malewska",
                HireDate = DateTime.Now.AddMonths(-7),
                Job = "Frontend programmer",
                Mgr = e1,
                Salary = 4000
            };

            var e3 = new Emp
            {
                Deptno = 1,
                Empno = 2,
                Ename = "Marcin Korewski",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "Frontend programmer",
                Mgr = null,
                Salary = 5000
            };

            var e4 = new Emp
            {
                Deptno = 2,
                Empno = 3,
                Ename = "Paweł Latowski",
                HireDate = DateTime.Now.AddMonths(-2),
                Job = "Frontend programmer",
                Mgr = e2,
                Salary = 5500
            };

            var e5 = new Emp
            {
                Deptno = 2,
                Empno = 4,
                Ename = "Michał Kowalski",
                HireDate = DateTime.Now.AddMonths(-2),
                Job = "Backend programmer",
                Mgr = e2,
                Salary = 5500
            };

            var e6 = new Emp
            {
                Deptno = 2,
                Empno = 5,
                Ename = "Katarzyna Malewska",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "Manager",
                Mgr = null,
                Salary = 8000
            };

            var e7 = new Emp
            {
                Deptno = null,
                Empno = 6,
                Ename = "Andrzej Kwiatkowski",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "System administrator",
                Mgr = null,
                Salary = 7500
            };

            var e8 = new Emp
            {
                Deptno = 2,
                Empno = 7,
                Ename = "Marcin Polewski",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "Mobile developer",
                Mgr = null,
                Salary = 4000
            };

            var e9 = new Emp
            {
                Deptno = 2,
                Empno = 8,
                Ename = "Władysław Torzewski",
                HireDate = DateTime.Now.AddMonths(-9),
                Job = "CTO",
                Mgr = null,
                Salary = 12000
            };

            var e10 = new Emp
            {
                Deptno = 2,
                Empno = 9,
                Ename = "Andrzej Dalewski",
                HireDate = DateTime.Now.AddMonths(-4),
                Job = "Database administrator",
                Mgr = null,
                Salary = 9000
            };

            empsCol.Add(e1);
            empsCol.Add(e2);
            empsCol.Add(e3);
            empsCol.Add(e4);
            empsCol.Add(e5);
            empsCol.Add(e6);
            empsCol.Add(e7);
            empsCol.Add(e8);
            empsCol.Add(e9);
            empsCol.Add(e10);
            Emps = empsCol;

            #endregion
        }


        /*
            Celem ćwiczenia jest uzupełnienie poniższych metod.
         *  Każda metoda powinna zawierać kod C#, który z pomocą LINQ'a będzie realizować
         *  zapytania opisane za pomocą SQL'a.
        */

        /// <summary>
        /// SELECT * FROM Emps WHERE Job = "Backend programmer";
        /// </summary>
        public void Przyklad1()
        {
            Console.WriteLine("Przykład 1:");
            //1. Query syntax (SQL)
            var sql = from emp in Emps
                where emp.Job == "Backend programmer"
                select new
                {
                    Nazwisko = emp.Ename,
                    Zawod = emp.Job
                };
            foreach (var emp in sql)
            {
                Console.WriteLine(emp);
            }

            //2. Lambda and Extension methods
            var lambda = Emps
                .Where(e => e.Job.Equals("Backend programmer"));
            foreach (var emp in lambda)
            {
                Console.WriteLine(emp);
            }
        }

        /// <summary>
        /// SELECT * FROM Emps Job = "Frontend programmer" AND Salary>1000 ORDER BY Ename DESC;
        /// </summary>
        public void Przyklad2()
        {
            Console.WriteLine("Przykład 2:");
            var sql = from emp in Emps
                where emp.Job == "Frontend programmer" && emp.Salary > 1000
                orderby emp.Ename descending
                select emp;

            foreach (var emp in sql)
            {
                Console.WriteLine(emp);
            }

            var lambda = Emps
                .Where(e => e.Job == "Frontend programmer" && e.Salary > 1000)
                .OrderByDescending(e => e.Ename);
            foreach (var emp in lambda)
            {
                Console.WriteLine(emp);
            }
        }

        /// <summary>
        /// SELECT MAX(Salary) FROM Emps;
        /// </summary>
        public void Przyklad3()
        {
            Console.WriteLine("Przykład 3:");
            var sql = from emp in Emps
                select emp.Salary;
            Console.WriteLine(sql.Max());

            var lambda = Emps
                .Max(e => e.Salary);

            Console.WriteLine(lambda);
        }


        /// <summary>
        /// SELECT * FROM Emps WHERE Salary=(SELECT MAX(Salary) FROM Emps);
        /// </summary>
        public void Przyklad4()
        {
            Console.WriteLine("Przykład 4:");
            var sql = from emp in Emps
                where emp.Salary == (from emp1 in Emps
                    select emp1.Salary).Max()
                select emp;


            foreach (var emp in sql)
            {
                Console.WriteLine(emp);
            }

            var lambda = Emps
                .Where(e => e.Salary == (from emp in Emps select emp.Salary).Max());

            foreach (var emp in lambda)
            {
                Console.WriteLine(emp);
            }
        }

        /// <summary>
        /// SELECT ename AS Nazwisko, job AS Praca FROM Emps;
        /// </summary>
        public void Przyklad5()
        {
            Console.WriteLine("Przykład 5:");
            var sql = from emp in Emps
                select new
                {
                    Nazwisko = emp.Ename,
                    Praca = emp.Job
                };
            foreach (var emp in sql)
            {
                Console.WriteLine(emp);
            }

            var lambda = Emps
                .Select(e => new
                {
                    Nazwisko = e.Ename,
                    Praca = e.Job
                });

            foreach (var emp in lambda)
            {
                Console.WriteLine(emp);
            }
        }

        /// <summary>
        /// SELECT Emps.Ename, Emps.Job, Depts.Dname FROM Emps
        /// INNER JOIN Depts ON Emps.Deptno=Depts.Deptno
        /// Rezultat: Złączenie kolekcji Emps i Depts.
        /// </summary>
        public void Przyklad6()
        {
            Console.WriteLine("Przykład 6:");
            var sql = from emp in Emps
                join dept in Depts
                    on emp.Deptno equals dept.Deptno
                select emp.Ename + ", " + dept.Deptno;
            foreach (var emp in sql)
            {
                Console.WriteLine(emp);
            }

            var lambda = Emps
                .Join(Depts, emp => emp.Deptno, dept => dept.Deptno, (emp, dept) => new
                {
                    emp,
                    dept
                })
                .Select(e => e.emp.Ename + " " + e.dept.Dname);

            foreach (var emp in lambda)
            {
                Console.WriteLine(emp);
            }
        }

        /// <summary>
        /// SELECT Job AS Praca, COUNT(1) LiczbaPracownikow FROM Emps GROUP BY Job;
        /// </summary>
        public void Przyklad7()
        {
            Console.WriteLine("Przykład 7:");
            var sql = (from emp in Emps
                group emp by emp.Job
                into newEmps
                select new
                {
                    Praca = newEmps.Key,
                    EmpCount = newEmps.Count()
                });
            foreach (var job in sql)
            {
                Console.WriteLine(job);
            }

            var lambda = Emps
                .GroupBy(e => e.Job)
                .Select(e => new
                {
                    Praca = e.Key,
                    LiczbaPracowników = e.Count()
                });

            foreach (var job in lambda)
            {
                Console.WriteLine(job);
            }
        }

        /// <summary>
        /// Zwróć wartość "true" jeśli choć jeden
        /// z elementów kolekcji pracuje jako "Backend programmer".
        /// </summary>
        public void Przyklad8()
        {
            Console.WriteLine("Przykład 8:");
            var sql = from emp in Emps
                where emp.Job == "Backend programmer"
                select emp;
            Console.WriteLine(sql.Any());
            var lambda = Emps
                .Where(e => e.Job == "Backend programmer");
            Console.WriteLine(lambda.Any());
        }

        /// <summary>
        /// SELECT TOP 1 * FROM Emp WHERE Job="Frontend programmer"
        /// ORDER BY HireDate DESC;
        /// </summary>
        public void Przyklad9()
        {
            Console.WriteLine("Przykład 9:");
            var sql = from emp in Emps
                where emp.Job == "Frontend programmer"
                orderby emp.HireDate descending
                select emp;
            Console.WriteLine(sql.First());
            var lambda = Emps
                .Where(e => e.Job == "Frontend programmer")
                .OrderByDescending(e => e.HireDate);

            Console.WriteLine(lambda.First());
        }

        /// <summary>
        /// SELECT Ename, Job, Hiredate FROM Emps
        /// UNION
        /// SELECT "Brak wartości", null, null;
        /// </summary>
        public void Przyklad10()
        {
            Console.WriteLine("Przykład 10:");
            var sql = (from emp in Emps
                select new
                {
                    emp.Ename,
                    emp.Job,
                    emp.HireDate
                }).Union(new[] {new {Ename = "Brak wartości", Job = (string) null, HireDate = (DateTime?) null}});
            foreach (var emp in sql)
            {
                Console.WriteLine(emp);
            }

            var lambda = Emps
                .Select(e => (e.Ename, e.Job, e.HireDate))
                .Union(Emps
                    .Select(e2 => (Ename: "Brak Wartości", Job: (string) null, HireDate: (DateTime?) null)));
            foreach (var emp in lambda)
            {
                Console.WriteLine(emp);
            }
        }

        //Znajdź pracownika z najwyższą pensją wykorzystując metodę Aggregate()
        public void Przyklad11()
        {
            Console.WriteLine("Przykład 11:");
            var sql = (from emp in Emps
                select emp).Aggregate((e1, e2) => e2.Salary > e1.Salary ? e2 : e1);
            Console.WriteLine(sql);
            var lambda = Emps
                .Aggregate((e1, e2) => e2.Salary > e1.Salary ? e2 : e1);
            Console.WriteLine(lambda);
        }

        //Z pomocą języka LINQ i metody SelectMany wykonaj złączenie
        //typu CROSS JOIN
        public void Przyklad12()
        {
            Console.WriteLine("Przykład 12:");
            var sql = from emp in Emps
                from dept in Depts
                select new
                {
                    emp,
                    dept
                };
            foreach (var result in sql)
            {
                Console.WriteLine(result);
            }
            var lambda = Emps
                .SelectMany(e => Depts, (e2, d) => new { EmpName = e2.Ename, EmpJob = e2.Job, d.Dname});
            foreach (var result in lambda)
            {
                Console.WriteLine(result);
            }
        }
    }
}