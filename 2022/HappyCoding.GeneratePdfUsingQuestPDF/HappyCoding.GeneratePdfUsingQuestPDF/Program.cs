using System.Diagnostics;
using HappyCoding.GeneratePdfUsingQuestPDF;
using HappyCoding.GeneratePdfUsingQuestPDF.Templates;
using QuestPDF.Fluent;

// This project is based on
// https://www.questpdf.com/documentation/getting-started.html#address-component

var invoice = InvoiceFactory.GetInvoiceDetails();
var invoiceDoc = new InvoiceDocument(invoice);
invoiceDoc.GeneratePdf("invoice.pdf");

Process.Start("explorer.exe", "invoice.pdf");