﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;
using Microsoft.AspNetCore.Http;
using WebApplication2.DTOs;
using System.ComponentModel;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaxFreeController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<TaxFree>> GetAll()
        {
            using (var context = new TaxFreeContext(null))
            {
                var tax = context.TaxFrees.ToList();
                return tax;
            }

        }

        [HttpGet("{id:Guid}")]
        public ActionResult<TaxFree> GetById(Guid id)
        {
            using var context = new TaxFreeContext(null);
            var taxFree = context.TaxFrees.FirstOrDefault(t => t.Id == id);

            if (taxFree is null)
            {
                return NotFound(new { message = $"TaxFree with { id } not found!" });
            }
            return taxFree;

        }

        [HttpPost]
        public ActionResult<TaxFree> PostNewTaxFree(TaxFreeDTO tax)
        {
            //            if (!tax.isValid())
            //                return BadRequest("Invalid data.");


            using (var context = new TaxFreeContext(null))
            {

                context.TaxFrees.Add(new TaxFree()
                {
                    Company = tax.Company,
                    Country = tax.Country,
                    VatRate = tax.VatRate,
                    DateOfPurchase = tax.DateOfPurchase,
                    VatCode = tax.VatCode,
                    DateTaxFreeRegistration = tax.DateTaxFreeRegistration
                }); ;

                context.SaveChanges();
            }

            return Ok();
        }

        [HttpDelete("{id:Guid}")]
        public ActionResult DeletePostById(Guid id)
        {
            using var context = new TaxFreeContext(null);
            var taxFree = context.TaxFrees.FirstOrDefault(t => t.Id == id);
            if (taxFree is null)
            {
                return NotFound(new { message = $"TaxFree with { id } not found!" });
            }
            context.TaxFrees.Remove(taxFree);
            context.SaveChanges();
            return Ok(new { message = $"TaxFree with { id } succesfuly deleted!" });
        }

        [HttpGet("search")]
        public ActionResult<IEnumerable<TaxFree>> Serch([FromQuery] string search_string, [FromQuery] string sort_by = nameof(TaxFree.Id), [FromQuery] bool sort_type = true)
        {
            using var context = new TaxFreeContext(null);
            //var filteredTaxFrees = context.TaxFrees.Where(serch => serch.Id.ToString().Contains(search_string) ||
            //serch.Country.Contains(search_string) || serch.Country.Contains(search_string) ||
            //serch.VatRate.ToString().Contains(search_string) || serch.DateOfPurchase.ToString() == search_string || serch.VatCode == search_string ||
            //serch.DateTaxFreeRegistration.ToString() == search_string).ToList();
            if(search_string is null)
            {
                return BadRequest(new { message = $"Serch string can't be null" });
            }
            var filteredTaxFrees = context.TaxFrees.ToList().Where(taxFree => taxFree.ToString().Contains(search_string)).ToList();

            try
            {
                if (sort_type)
                {
                    filteredTaxFrees = filteredTaxFrees.OrderBy(GetKeySelector(sort_by)).ToList();
                }
                else
                {
                    filteredTaxFrees = filteredTaxFrees.OrderByDescending(GetKeySelector(sort_by)).ToList();
                }

            }
            catch(Exception e)
            {
                return BadRequest(new { message = $"Sost type is not valid!" });
            }
            return filteredTaxFrees;
        }

        public Func<TaxFree, string> GetKeySelector(string sortBy)
        {
            switch(sortBy)
            {
                case nameof(TaxFree.Id):
                    return (TaxFree t) => t.Id.ToString();
                case nameof(TaxFree.Company):
                    return (TaxFree t) => t.Company;
                case nameof(TaxFree.Country):
                    return (TaxFree t) => t.Country;
                case nameof(TaxFree.VatRate):
                    return (TaxFree t) => t.VatRate.ToString();
                case nameof(TaxFree.DateOfPurchase):
                    return (TaxFree t) => t.DateOfPurchase.ToString();
                case nameof(TaxFree.VatCode):
                    return (TaxFree t) => t.VatCode;
                case nameof(TaxFree.DateTaxFreeRegistration):
                    return (TaxFree t) => t.DateTaxFreeRegistration.ToString();
                default:
                    throw new ArgumentException();

            }
        }

        [HttpPut("{id:Guid}")]
        public ActionResult<TaxFree> UpdateOnId(TaxFreeDTO tax, Guid id)
        {
            using (var context = new TaxFreeContext(null))
            {
                var taxFree = context.TaxFrees.FirstOrDefault(t => t.Id == id);
                if (taxFree is null)
                {
                    return NotFound(new { message = $"TaxFree with { id } not found!" });
                }

                taxFree.Company = tax.Company;
                taxFree.Country = tax.Country;
                taxFree.VatRate = tax.VatRate;
                taxFree.DateOfPurchase = tax.DateOfPurchase;
                taxFree.VatCode = tax.VatCode;
                taxFree.DateTaxFreeRegistration = tax.DateTaxFreeRegistration;
                context.SaveChanges();
            }

            return Ok();
        }
    }
}