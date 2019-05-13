using AspMvc.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AspMvc
{
    class Utils
    {
        public static IList<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, ctx, validationResults, true);
            return validationResults;
        }

        public static void PopulateWithTestData(IRepository repository)
        {
            Manufacturer manufacturer1 = new Manufacturer();

            manufacturer1.Id = 1;
            manufacturer1.Name = "Producent";
            manufacturer1.Rating = 4.59;
            manufacturer1.CreationDate = new DateTime(2017, 01, 21);

            Manufacturer manufacturer2 = new Manufacturer();

            manufacturer2.Id = 1;
            manufacturer2.Name = "Producent2";
            manufacturer2.Rating = 3.19;
            manufacturer2.CreationDate = new DateTime(2016, 11, 15);

            Tool tool1 = new Tool();

            tool1.Id = 0;
            tool1.Name = "Narzędzie";
            tool1.ProductionDate = new DateTime(2018, 05, 06);
            tool1.Price = 50.99;
            tool1.Rating = 4.24;
            tool1.ManufacturerId = 1;
            tool1.Manufacturer = manufacturer1;

            Tool tool2 = new Tool();

            tool2.Id = 0;
            tool2.Name = "Inne narzędzie";
            tool2.ProductionDate = new DateTime(2019, 01, 02);
            tool2.Price = 150.50;
            tool2.Rating = 3.12;
            tool2.ManufacturerId = 1;
            tool2.Manufacturer = manufacturer1;

            Tool tool3 = new Tool();

            tool3.Id = 0;
            tool3.Name = "Stare narzędzie";
            tool3.ProductionDate = new DateTime(2017, 07, 10);
            tool3.Price = 35.00;
            tool3.Rating = 5.00;
            tool3.ManufacturerId = 2;
            tool3.Manufacturer = manufacturer2;

            repository.Add(manufacturer1);
            repository.Add(manufacturer2);
            repository.Add(tool1);
            repository.Add(tool2);
            repository.Add(tool3);
        }
    }
}
