﻿using EntityClient.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EntityClient.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public async Task<ActionResult> Index()
        {
            List<Product> ProductList = new List<Product>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44312/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync("api/Product");
                if (response.IsSuccessStatusCode)
                {
                    var jasonString = response.Content.ReadAsStringAsync();
                    jasonString.Wait();
                    ProductList = JsonConvert.DeserializeObject<List<Product>>(jasonString.Result);

                }
                else
                {
                    var jsonString = response.Content.ReadAsStringAsync();
                    jsonString.Wait();

                    ViewBag.msg = JsonConvert.DeserializeObject<string>(jsonString.Result);

                }

            }
            return View(ProductList);
        }
        public async Task<ActionResult> Details(int? id)
        {
            Product product = new Product();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44312/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync("api/Product/" + id);
                if (response.IsSuccessStatusCode)
                {
                    product = await response.Content.ReadAsAsync<Product>();

                }
                else
                {
                    var jsonString = response.Content.ReadAsStringAsync();
                    jsonString.Wait();

                    ViewBag.msg = JsonConvert.DeserializeObject<string>(jsonString.Result);
                }

            }
            return View(product);
        }
        public async Task<ActionResult> Insert()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Insert(Product product)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44312/");
                HttpResponseMessage response = await client.PostAsJsonAsync("api/Product", product);
                if (response.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
            return View();
        }
        public async Task<ActionResult> Edit(int? id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44312/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync("api/Product/" + id);
                if (response.IsSuccessStatusCode)
                {
                    Product product = await response.Content.ReadAsAsync<Product>();
                    return View(product);
                }
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<ActionResult> Edit(int? id, Product product)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44312/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.PutAsJsonAsync("api/Product/" + id, product);

                if (response.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
            return View();
        }
        public async Task<ActionResult> Delete(int? id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44312");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync("api/Product/" + id);
                if (response.IsSuccessStatusCode)
                {
                    Product product = await response.Content.ReadAsAsync<Product>();
                    return View(product);
                }
            }
            return RedirectToAction("IndexAsync");
        }
        [HttpPost]
        public async Task<ActionResult> Delete(int? id, Product product)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44312/");
                client.DefaultRequestHeaders.Accept.Clear();
                HttpResponseMessage response = await client.DeleteAsync("api/Product/" + id);
                if (response.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
            return View();
        }
    }
}