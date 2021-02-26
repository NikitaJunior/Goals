using System;
using System.Collections.Generic;
using System.Linq;

namespace CovarianceAndContravariance
{
    public class Program
    {
        #region Interfaces

        interface IInvariant<T>
        {
            T Method(T argument);
        }

        interface ICovariant<out T>
        {
            T Method();
        }

        interface IContravariant<in T>
        {
            void Method(T argument);
        }

        public class Invariant<T> : IInvariant<T>
        {
            public T Method(T argument)
            {
                return argument;
            }
        }

        #endregion

        #region Classes

        public class Covariant<T> : ICovariant<T>
        {
            private T t;

            public T Method()
            {
                return t;
            }
        }

        public class Contravariant<T> : IContravariant<T>
        {
            public void Method(T argument)
            {

            }
        }

        public class Human
        {
            public Human()
            {
                Age = 18;
            }

            public string Name { get; set; }
            public int Age { get; set; }
        }

        public class JuniorDeveloper : Human
        {
            public JuniorDeveloper()
            {
                // By calling Human() the inherited variable a is also = 0; 
                Experience = 0;
            }

            public decimal Salary { get; set; }
            public int Experience { get; set; }
        }

        public class QAEngineer : Human
        {
            public decimal Salary { get; set; }
            public int Experience { get; set; }
        }

        #endregion

        #region Delegates

        delegate Human ReturnsHumanDelegate();
        delegate JuniorDeveloper ReturnsJuniorDeveloperDelegate();

        public static Human ReturnHuman()
        {
            return null;
        }

        public static JuniorDeveloper ReturnJuniorDeveloper()
        {
            return null;
        }

        delegate void TakesHumanDelegate(Human _base);
        delegate void TakesJuniorDeveloperDelegate(JuniorDeveloper _derived);

        public static void TakeHuman(Human _base)
        {

        }

        public static void TakeJuniorDeveloper(JuniorDeveloper _derived)
        {

        }

        #endregion

        public static void Main(string[] args)
        {
            //IntefaceInvariances();
            //IntefaceCovariances();
            //IntefaceContravariances();
            RunCoAndContravarianceExamples(); /*Runs the covariance Examples*/
            //RunCoAndContravarianceCollectionExamples(); /*Runs the covariance Collection CollectionExamples*/
            //CovarianceDelegates(); /*Runs the covariance Delegate CollectionExamples*/
            //ContravarianceDelegates(); /*Runs the contravariance Delegate CollectionExamples*/
            Console.ReadKey();
        }

        public static void RunCoAndContravarianceExamples()
        {
            JuniorDeveloper a = new JuniorDeveloper
            {
                Salary = 15000,
                Name = "Nikitka"
            };

            QAEngineer b = new QAEngineer
            {
                Salary = 70000,
                Experience = 4,
                Name = "QA Jesus"
            };

            Console.WriteLine($"JuniorDeveloper: {a.Name}, {a.Age}, {a.Experience}, {a.Salary}");
            Console.WriteLine($"QAEngineer: {b.Name}, {b.Age}, {b.Experience}, {b.Salary}");

            Human f = (Human)a;
            f.Age = 20;

            Human g = (Human)b;
            g.Age = 20;

            Console.WriteLine($"JuniorDeveloper (Casted): {f.Name}, {f.Age}");
            Console.WriteLine($"QAEngineer (Casted): {g.Name}, {g.Age}");

            a = (JuniorDeveloper)f;
            a.Experience = 10;
            //JuniorDeveloper a2 = (JuniorDeveloper)g; //This is incorrect

            Console.WriteLine($"JuniorDeveloper (ReCasted from Dev): {a.Name}, {a.Age}, {a.Experience}, {a.Salary}");
        }

        public static void RunCoAndContravarianceCollectionExamples()
        {
            List<Human> humanList = new List<Human>();
            List<QAEngineer> qaList = new List<QAEngineer>();

            humanList.Add((Human)new JuniorDeveloper
            {
                Salary = 15000,
                Age = 18,
                Experience = 0,
                Name = "Nikitka"
            });

            humanList.Add((Human)new QAEngineer
            {
                Salary = 70000,
                Age = 25,
                Experience = 4,
                Name = "QA Jesus"
            });

            //qaList.Add(new Human
            //{
            //    Name = "Human"
            //});

            //qaList.Add((QAEngineer) new Human
            //{
            //    Name = "Human"
            //});

            foreach (var h in humanList)
            {
                Console.WriteLine($"Human (Casted): {h.Name}, {h.Age}");
            }
        }

        private static void IntefaceInvariances()
        {
            IInvariant<Human> invariantHuman = new Invariant<Human>();
            IInvariant<JuniorDeveloper> invariantDeveloper = new Invariant<JuniorDeveloper>();

            //invariantHuman = invariantDeveloper; // Error
            //invariantDeveloper = invariantHuman; // Error
        }

        private static void IntefaceCovariances()
        {
            ICovariant<Human> covariantHuman = new Covariant<Human>();
            ICovariant<JuniorDeveloper> covariantDeveloper = new Covariant<JuniorDeveloper>();

            covariantHuman = covariantDeveloper;
            //covariantDeveloper = covariantHuman; /Error
        }

        private static void IntefaceContravariances()
        {
            IContravariant<Human> contravariantHuman = new Contravariant<Human>();
            IContravariant<JuniorDeveloper> contravariantDeveloper = new Contravariant<JuniorDeveloper>();

            //contravariantHuman = contravariantDeveloper; //Error
            contravariantDeveloper = contravariantHuman;
        }

        private static void CovarianceDelegates()
        {
            ReturnsHumanDelegate _delegate;
            _delegate = ReturnHuman;
            _delegate = ReturnJuniorDeveloper;

            ReturnsJuniorDeveloperDelegate _delegate2;
            //_delegate2 = ReturnHuman; //Error
            _delegate2 = ReturnJuniorDeveloper;
        }

        private static void ContravarianceDelegates()
        {
            TakesJuniorDeveloperDelegate _delegate;
            _delegate = TakeHuman;
            _delegate = TakeJuniorDeveloper;

            TakesHumanDelegate _delegate2;
            _delegate2 = TakeHuman; 
            //_delegate2 = TakeJuniorDeveloper; //Error
        }
    }
}
