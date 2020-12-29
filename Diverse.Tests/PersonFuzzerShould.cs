﻿using System.Collections.Generic;
using System.Linq;
using NFluent;
using NUnit.Framework;

namespace Diverse.Tests
{
    /// <summary>
    /// All about the primitives related to <see cref="Person"/>, Email etc.
    /// </summary>
    [TestFixture]
    public class PersonFuzzerShould
    {
        [Test]
        public void Be_able_to_Fuzz_FirstNames_for_male()
        {
            var fuzzer = new Fuzzer(381025586);

            var firstNames = new List<string>();
            for (var i = 0; i < 10; i++)
            {
                firstNames.Add( fuzzer.GenerateFirstName(Gender.Male));
            }

            Check.That(firstNames).ContainsExactly("Einar", "Rajiv", "Hwang", "Gunne", "James", "Elias", "Sacha", "Djibril", "Gjurd", "Jin");
        }

        [Test]
        public void Be_able_to_Fuzz_FirstNames_for_female()
        {
            var fuzzer = new Fuzzer(1834164083);

            var firstNames = new List<string>();
            for (var i = 0; i < 10; i++)
            {
                firstNames.Add(fuzzer.GenerateFirstName(Gender.Female));
            }

            Check.That(firstNames).ContainsExactly("Malika", "Kamini", "My Lê", "Karine", "Nzinga", "Hailey", "Zoe", "Nichelle", "Mây", "Noûr");
        }

        [Test]
        public void Be_able_to_Fuzz_FirstNames_for_every_genders()
        {
            var fuzzer = new Fuzzer(1340516487);

            var firstNames = new List<string>();
            for (var i = 0; i < 10; i++)
            {
                firstNames.Add(fuzzer.GenerateFirstName());
            }

            Check.That(firstNames).ContainsExactly("Kyeol", "Koffi", "Ba-Thong", "Maxime", "Johanna", "Diego", "Tian", "Aata", "Ximena", "Jemila");
        }

        [Test]
        public void Be_able_to_Fuzz_LastName_for_every_FirstName()
        {
            var fuzzer = new Fuzzer(2000210944);

            var lastNames = new List<string>();
            for (var i = 0; i < 10; i++)
            {
                var firstName = fuzzer.GenerateFirstName();
                lastNames.Add($"{firstName} {fuzzer.GenerateLastName(firstName)}");
            }

            Check.That(lastNames).ContainsExactly("Beatriz González", "Drell Walker", "Mei Laghari", "Mamadou Diop", "Barack Sanchez", "Xuân Madan", "Marija Boucard", "Hoàng Laghari", "Demba Ibrahim", "Jemila Nwadike");
        }

        [Test]
        public void Be_able_to_Fuzz_Diverse_Persons()
        {
            var fuzzer = new Fuzzer(541936699);

            var persons = new List<Person>();
            for (var i = 0; i < 10; i++)
            {
                persons.Add(fuzzer.GeneratePerson());
            }

            Check.That(persons.Select(x => x.ToString()))
                .ContainsExactly(
                    "Ms. Valeria DENILSON (Female) vdenilson@microsoft.com (age: 62 years)", 
                    "Ms. Freja ABRAHAMSEN (Female) fabrahamsen@ibm.com (age: 76 years)", 
                    "Mr. Kevin BRAND (Male) kevin.brand@42skillz.com (married - age: 86 years)", 
                    "Mx. Jaycarran KHATRI (NonBinary) jaycarran.khatri@gmail.com (married - age: 32 years)", 
                    "Ms. Fatima SELASSIE (Female) fatima.selassie@aol.com (age: 68 years)", 
                    "Mx. Demba ADOMAKO (NonBinary) demba.adomako@protonmail.com (age: 34 years)", 
                    "Mrs. Erika TVEIT (Female) etveit@yahoo.fr (married - age: 24 years)", 
                    "Ms. Anneke JOHNSON (Female) anneke.johnson@kolab.com (age: 37 years)", 
                    "Ms. Renata AMBRÍZ (Female) renata.ambriz@microsoft.com (age: 60 years)", 
                    "Mr. Kumar YOON (Male) kyoon@yahoo.fr (age: 53 years)");
        }

        [Test]
        public void Be_able_to_generate_email_from_scratch()
        {
            var fuzzer = new Fuzzer(1996243347);

            var email = fuzzer.GenerateEMail();
            var email2 = fuzzer.GenerateEMail();
            var email3 = fuzzer.GenerateEMail();

            Check.That(email).IsEqualTo("atef.ben-achour@yopmail.com");
            Check.That(email2).IsEqualTo("jbak@kolab.com");
            Check.That(email3).IsEqualTo("markus.hendricks@42skillz.com");
        }

        [Test]
        public void Generate_Email_with_dashes_instead_of_spaces_and_pure_ascii_char()
        {
            var fuzzer = new Fuzzer(40816378);

            var email = fuzzer.GenerateEMail("Saïd Ef", "Ben Achour");

            Check.That(email).IsEqualTo("said-ef.ben-achour@microsoft.com");
        }
    }
}
