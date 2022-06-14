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


            var orderDetailsId = order.PackingSlipDetailDtos.Select(o => o.OrderDetailId).ToList();
            IQueryable<OrderDetail>? orderDetailData = from orderDetail in _phoenixContext.OrderDetails
                                  .Include(o => o.Product)
                                                       where orderDetailsId.Contains(orderDetail.Id)
                                                       select orderDetail;


            htmlText.Replace("{{PrintDate}}", DateTime.Now.ToString("MM/dd/yyyy"));
                htmlText.Replace("{{OrderDate}}", orderHeaderData.OrderDate.ToString("MM/dd/yyyy"));
                htmlText.Replace("{{PaymentTerms}}", orderHeaderData.PaymentTerms);
                htmlText.Replace("{{ShipVia}}", orderHeaderData.ShipmentMethod);
                htmlText.Replace("{{PurchaseOrderNumber}}", orderHeaderData.PONumber);
                htmlText.Replace("{{SalesOrderNumber}}", orderHeaderData.Id.ToString());
                htmlText.Replace("{{OrderedBy}}", orderHeaderData.Contact.FirstName + " " + orderHeaderData.Contact.LastName); //TODO: To be reviewed
                htmlText.Replace("{{CustomerNumber}}", orderHeaderData.AccountId.ToString());
                htmlText.Replace("{{AccountType}}", orderHeaderData.Account.AccountTypeCode);
                htmlText.Replace("{{Comments}}", ((orderHeaderData.SystemComments ?? "") + " " + (orderHeaderData.FreeFormComments ?? "")).Trim()); //TODO: To be reviewed

                FillShipToAddress(orderHeaderData);
                FillBillToAddress(orderHeaderData);
                CreateProductsTableData(orderDetailData);
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
            foreach (OrderDetail orderDetail in orderDetailData)
            {
                string txt = "<tr><td class=\"add-border padding-for-cell\"><h4 class=\"m-0 p-0\">{{Quantity}}</h4 ></td><td class= \"add-border padding-for-cell\" ><h4 class= \"m-0 p-0\" >{{ItemCode}}</h4 ></td><td class= \"add-border padding-for-cell\" > <h4 class= \"m-0 p-0\" >{{Type}}</h4 ><p class= \"m-0 p-0 address-font\" > ISBN:{{ISBN}}</p ></td></tr > ";

                txt = txt.Replace("{{Quantity}}", (orderDetail.OpenQuantity + orderDetail.ShippedQuantity).ToString());
                txt = txt.Replace("{{ItemCode}}", orderDetail.ProductCode);
                txt = txt.Replace("{{Type}}", orderDetail.Product.Description);
                txt = txt.Replace("{{ISBN}}", orderDetail.Product.Isbn?.Trim());
                
                productTableHtml.Append(txt);
            }
            htmlText.Replace("{{productDetailsContent}}", productTableHtml.ToString());

        }

        private byte[] CreatePDF()
        {
            //Configure page settings
            var configurationOptions = new PdfGenerateConfig();

            //Page is in Landscape mode, other option is Portrait
            configurationOptions.PageOrientation = PdfSharp.PageOrientation.Portrait;

            //Set page type as Letter. Other options are A4 …
            configurationOptions.PageSize = PdfSharp.PageSize.A4;

            //This is to fit Chrome Auto Margins when printing.Yours may be different
            configurationOptions.MarginBottom = 10;
            configurationOptions.MarginLeft = 1;

            //The actual PDF generation
            //var OurPdfPage = PdfGenerator.GeneratePdf(htmlText.ToString(), configurationOptions);
            var OurPdfPage = PdfGenerator.GeneratePdf(htmlText.ToString(), PageSize.A4);

            OurPdfPage.Save("Output.pdf"); //TODO: to be removed. Added just for viewing purpose

            MemoryStream memoryStream = new MemoryStream();
            OurPdfPage.Save(memoryStream);
            return memoryStream.ToArray();
        }
    }
}
