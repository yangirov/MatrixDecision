using System;
using System.Linq;
using System.ComponentModel;
using System.Collections.Generic;

using Bogus;

namespace MatrixDecision
{
    partial class Program
    {
        static void Main(string[] args)
        {
            #region Conditions
            bool Required() => true;
            bool SnilsMissingReasonIsEqual3(int reason) => reason == 3;
            bool SnilsMissingReasonIsEqual2Or3Or5(int reason) => new int[] { 2, 3, 5 }.Contains(reason);
            bool AgeIsGreater16(int age) => age > 16;
            bool AgeIsGreater14(int age) => age > 14;
            bool SocialStatusIsRange6And30(int status) => 6 <= status && status <= 30;
            bool SocialStatusIsEqual1(int status) => status == 1;
            #endregion

            void Initial()
            {
                Randomizer.Seed = new Random(8675309);

                var matrix = new Dictionary<string, bool[]>();

                var patient = new Faker<Patient>()
                    .RuleFor(u => u.Guid, f => Guid.NewGuid().ToString())
                    .RuleFor(u => u.FirstName, (f, u) => f.Name.FirstName())
                    .RuleFor(u => u.LastName, (f, u) => f.Name.LastName())
                    .RuleFor(u => u.SocialStatus, f => f.Random.Number(1, 45))
                    .RuleFor(u => u.SnilsMissingReason, f => f.Random.Number(1, 5))
                    .RuleFor(u => u.Age, f => f.Random.Number(10, 20))
                    .RuleFor(u => u.PatientIdentity, f => f.Random.Number(10, 20))
                    .RuleFor(u => u.InformationNewborn, f => f.Random.Number(10, 20))
                    .RuleFor(u => u.ShiftWorker, f => f.Random.Number(10, 20))
                    .RuleFor(u => u.Nationality, f => f.Random.Number(10, 20))
                    .RuleFor(u => u.MaritalStatus, f => f.Random.Number(10, 20))
                    .RuleFor(u => u.CitizenShip, f => f.Random.Number(10, 20))
                    .RuleFor(u => u.InvGroup, f => f.Random.Number(10, 20))
                    .RuleFor(u => u.Workplace, f => f.Random.Number(10, 20))
                    .Generate();

                CreateMatrixDecision(matrix, patient);
                CheckMatrixPairs(matrix, patient);
            };


            void CreateMatrixDecision(Dictionary<string, bool[]> matrix, Patient p)
            {
                matrix.Add("FirstName", new bool[] { SnilsMissingReasonIsEqual3(p.SnilsMissingReason) });
                matrix.Add("PatientIdentity", new bool[] { SnilsMissingReasonIsEqual2Or3Or5(p.SnilsMissingReason) });
                matrix.Add("InformationNewborn", new bool[] { SnilsMissingReasonIsEqual3(p.SnilsMissingReason) });
                matrix.Add("ShiftWorker", new bool[] { Required() });
                matrix.Add("Nationality", new bool[] { Required() });
                matrix.Add("SocialStatus", new bool[] { Required() });
                matrix.Add("MaritalStatus", new bool[] { AgeIsGreater16(p.Age) });
                matrix.Add("CitizenShip", new bool[] { AgeIsGreater14(p.Age) });
                matrix.Add("InvGroup", new bool[] { SocialStatusIsRange6And30(p.SocialStatus) });
                matrix.Add("Workplace", new bool[] { SocialStatusIsEqual1(p.SocialStatus) });
            }

            void CheckMatrixPairs(Dictionary<string, bool[]> matrix, Patient p)
            {
                // Выводим объект для сравнения
                foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(p))
                {
                    Console.WriteLine($"{descriptor.Name}={descriptor.GetValue(p)}");
                }

                // Сравниваем пропы
                Console.WriteLine("\nResult:\n");
                foreach (var pair in matrix)
                {
                    var show = true;

                    foreach (var condition in pair.Value)
                    {
                        show &= condition;
                    }

                    Console.WriteLine($"{pair.Key} — {show}");
                }
            }

            Initial();
        }
    }
}
