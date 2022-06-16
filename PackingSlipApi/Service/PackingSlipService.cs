using Microsoft.EntityFrameworkCore;
using PackingSlipApi.Dtos;
using PackingSlipApi.Interface;
using PackingSlipApi.Models;
using System.Diagnostics.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using TheArtOfDev.HtmlRenderer.PdfSharp;
using PackingSlipApi.Helpers;

namespace PackingSlipApi.Service
{
    [ExcludeFromCodeCoverage]
    public class PackingSlipService : IPackingSlipService
    {
        StringBuilder htmlText = new();
        readonly PhoenixContext _phoenixContext;

        public PackingSlipService(PhoenixContext phoenixContext)
        {
            _phoenixContext = phoenixContext;
        }

        public byte[] GeneratPDF(PackingSlipInputDto order)
        {
            using (StreamReader template = new StreamReader("Template/Index.html"))
                {
                    htmlText.Append(PdfGenerationHelper.ConvertHtmlToString(template, false));
                    template.Close();
                }
                BuildModel(order);
                return CreatePDF();

        }

        private void BuildModel(PackingSlipInputDto order)
        {
            var orderData = from orderHeader in _phoenixContext.OrderHeaders
                       .Include(o => o.Account)
                       .Include(o => o.Contact)
                       .Include(o => o.OrderShipToAddress)
                            where orderHeader.Id == order.OrderId
                            select orderHeader;
            OrderHeader? orderHeaderData = orderData.FirstOrDefault();

            if (orderHeaderData == null)
                throw new ArgumentException("Order Detail data does not exist for given OrderDetailIDs");


            var orderDetailsId = order.PackingSlipDetailDtos.Select(o => o.OrderDetailId).ToList();
            IQueryable<OrderDetail>? orderDetailData = from orderDetail in _phoenixContext.OrderDetails
                                  .Include(o => o.Product)
                                                       where orderDetailsId.Contains(orderDetail.Id)
                                                       select orderDetail;

            ReplaceHtmlTextWithModelData(orderHeaderData);
            CreateProductsTableData(orderDetailData);
            SetFooter(order.PackingSlipDetailDtos.Count, orderHeaderData);

            //htmlText.Replace("{{PrintDate}}", DateTime.Now.ToString("MM/dd/yyyy"));
            //    htmlText.Replace("{{OrderDate}}", orderHeaderData.OrderDate.ToString("MM/dd/yyyy"));
            //    htmlText.Replace("{{PaymentTerms}}", orderHeaderData.PaymentTerms);
            //    htmlText.Replace("{{ShipVia}}", orderHeaderData.ShipmentMethod);
            //    htmlText.Replace("{{PurchaseOrderNumber}}", orderHeaderData.PONumber);
            //    htmlText.Replace("{{SalesOrderNumber}}", orderHeaderData.Id.ToString());
            //    htmlText.Replace("{{OrderedBy}}", orderHeaderData.Contact.FirstName + " " + orderHeaderData.Contact.LastName); //TODO: To be reviewed
            //    htmlText.Replace("{{CustomerNumber}}", orderHeaderData.AccountId.ToString());
            //    htmlText.Replace("{{AccountType}}", orderHeaderData.Account.AccountTypeCode);
            //    htmlText.Replace("{{Comments}}", ((orderHeaderData.SystemComments ?? "") + " " + (orderHeaderData.FreeFormComments ?? "")).Trim()); //TODO: To be reviewed

            //    FillShipToAddress(orderHeaderData);
            //    FillBillToAddress(orderHeaderData);
            //    CreateProductsTableData(orderDetailData);
            }

        private void ReplaceHtmlTextWithModelData(OrderHeader orderHeaderData)
        {
            htmlText.Replace("{{PrintDate}}", DateTime.Now.ToString("MM/dd/yyyy"));
            htmlText.Replace("{{OrderDate}}", orderHeaderData.OrderDate.ToString("MM/dd/yyyy"));
            htmlText.Replace("{{PaymentTerms}}", orderHeaderData.PaymentTerms);
            htmlText.Replace("{{ShipVia}}", orderHeaderData.ShipmentMethod);
            htmlText.Replace("{{PurchaseOrderNumber}}", orderHeaderData.PONumber);
            htmlText.Replace("{{SalesOrderNumber}}", orderHeaderData.Id.ToString());
            htmlText.Replace("{{OrderedBy}}", orderHeaderData.Contact.FirstName + " " + orderHeaderData.Contact.LastName);
            htmlText.Replace("{{CustomerNumber}}", orderHeaderData.AccountId.ToString());
            htmlText.Replace("{{AccountType}}", orderHeaderData.Account.AccountTypeCode);
            htmlText.Replace("{{Comments}}", ((orderHeaderData.SystemComments ?? "") + " " + (orderHeaderData.FreeFormComments ?? "")).Trim());

            FillShipToAddress(orderHeaderData);
            FillBillToAddress(orderHeaderData);
        }


        private void FillShipToAddress(OrderHeader? orderHeaderData)
        {
            htmlText.Replace("{{ShipToAdress_CompanyName}}", orderHeaderData.OrderShipToAddress?.Company?.Trim());
            htmlText.Replace("{{ShipToAdress_Street}}", orderHeaderData.OrderShipToAddress.Street?.Trim());
            string shipToCityStateCtry = orderHeaderData.OrderShipToAddress.City?.Trim() + "," +
                orderHeaderData.OrderShipToAddress.State?.Trim() + " " + orderHeaderData.OrderShipToAddress.Country.Trim();
            htmlText.Replace("{{ShipToAdress_City_State_Country}}", shipToCityStateCtry);
            htmlText.Replace("{{ShipToAdress_Zip}}", orderHeaderData.OrderShipToAddress.Zip?.Trim());
        }

        private void FillBillToAddress(OrderHeader? orderHeaderData)
        {
            htmlText.Replace("{{BillToAdress_CompanyName}}", orderHeaderData.Account?.Company?.Trim());
            htmlText.Replace("{{BillToAdress_Street}}", orderHeaderData.Account.Street?.Trim());
            string billToCityStateCtry = orderHeaderData.Account.City?.Trim() + "," +
                orderHeaderData.Account.State?.Trim() + " " + orderHeaderData.Account.Country?.Trim();
            htmlText.Replace("{{BillToAdress_City_State_Country}}", billToCityStateCtry);
            htmlText.Replace("{{BillToAdress_Zip}}", orderHeaderData.Account.Zip?.Trim());
        }

        private void CreateProductsTableData(IQueryable<OrderDetail> orderDetailData)
        {
            StringBuilder productTableHtml = new StringBuilder();
            int iterator = 0;
            foreach (OrderDetail orderDetail in orderDetailData)
            {
                string orderDetailHtmlText;
                if (iterator == 11 || (iterator - 11) % 20 == 0)
                {
                    orderDetailHtmlText = "</table><br style=\"margin-top:50px\"><table class=\"width-95 product-table margin-table\"><tr><td class=\"add-border padding-for-cell\"><h4 class=\"m-0 p-0\">{{Quantity}}</h4 ></td><td class= \"add-border padding-for-cell\" ><h4 class= \"m-0 p-0\" >{{ItemCode}}</h4 ></td><td class= \"add-border padding-for-cell\" > <h4 class= \"m-0 p-0\" >{{Type}}</h4 ><p class= \"m-0 p-0\" > ISBN:{{ISBN}}</p ></td></tr > ";
                }
                else
                {
                    orderDetailHtmlText = "<tr><td class=\"add-border padding-for-cell\"><h4 class=\"m-0 p-0\">{{Quantity}}</h4 ></td><td class= \"add-border padding-for-cell\" ><h4 class= \"m-0 p-0\" >{{ItemCode}}</h4 ></td><td class= \"add-border padding-for-cell\" > <h4 class= \"m-0 p-0\" >{{Type}}</h4 ><p class= \"m-0 p-0\" > ISBN:{{ISBN}}</p ></td></tr > ";
                }

                orderDetailHtmlText = orderDetailHtmlText.Replace("{{Quantity}}", (orderDetail.OpenQuantity + orderDetail.ShippedQuantity).ToString());
                orderDetailHtmlText = orderDetailHtmlText.Replace("{{ItemCode}}", orderDetail.ProductCode);
                orderDetailHtmlText = orderDetailHtmlText.Replace("{{Type}}", orderDetail.Product.Description);
                orderDetailHtmlText = orderDetailHtmlText.Replace("{{ISBN}}", orderDetail.Product.Isbn?.Trim());
                
                productTableHtml.Append(orderDetailHtmlText);
                iterator++;
            }
            htmlText.Replace("{{productDetailsContent}}", productTableHtml.ToString());

        }

        private void SetFooter(int orderDetailProductsCount, OrderHeader orderHeaderData)
        {
            int n = orderDetailProductsCount;
            string footerHtml;
            if (n == 8 || (n - 11) % 17 == 0 || (n - 11) % 16 == 0)
            {
                footerHtml = "<div class=\"margin-table add-border footer w-95\" style=\"margin: 90px 10px 0 10px\" ><p>Comments: <span class=\"font-weight-bold\">{{Comments}}</span></p> </div>";
            }
            else
            {
                footerHtml = "<div class=\"margin-table add-border footer w-95\"> <p>Comments: <span class=\"font-weight-bold\">{{Comments}}</span></p> </div>";
            }
            footerHtml = footerHtml.Replace("{{Comments}}", ((orderHeaderData.SystemComments ?? "") + " " + (orderHeaderData.FreeFormComments ?? "")).Trim());
            htmlText = htmlText.Replace("{{footer}}", footerHtml);
        }

        private byte[] CreatePDF()
        {
            //Configure page settings
            var configurationOptions = new PdfGenerateConfig
            {
                PageOrientation = PageOrientation.Portrait,
                PageSize = PageSize.A4,

                MarginBottom = 19,
                MarginLeft = 2,
                // MarginBottom = 70,
                //MarginLeft = 20,
                //MarginRight = 20,
                //MarginTop = 70,
            };

            //The actual PDF generation
            var OurPdfPage = PdfGenerator.GeneratePdf(htmlText.ToString(), configurationOptions);            

            OurPdfPage.Save("Output.pdf"); //TODO: to be removed. Added just for viewing purpose

            MemoryStream memoryStream = new MemoryStream();
            //OurPdfPage.Save(memoryStream);
            return memoryStream.ToArray();
        }
    }
}
