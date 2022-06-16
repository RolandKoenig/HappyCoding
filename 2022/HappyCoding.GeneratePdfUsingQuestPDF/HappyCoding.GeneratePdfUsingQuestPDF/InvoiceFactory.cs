using HappyCoding.GeneratePdfUsingQuestPDF.Data;
using QuestPDF.Helpers;

namespace HappyCoding.GeneratePdfUsingQuestPDF;

internal class InvoiceFactory
{
    private static readonly Random s_random = new Random(100);

    public static Invoice GetInvoiceDetails()
    {
        var items = Enumerable
            .Range(1, 55)
            .Select(i => GenerateRandomOrderItem())
            .ToList();

        return new Invoice
        {
            InvoiceNumber = s_random.Next(1_000, 10_000),
            IssueDate = DateTime.Now,
            DueDate = DateTime.Now + TimeSpan.FromDays(14),

            SellerAddress = GenerateRandomAddress(),
            CustomerAddress = GenerateRandomAddress(),

            Items = items,
            Comments = Placeholders.Paragraph()
        };
    }

    private static OrderItem GenerateRandomOrderItem()
    {
        return new OrderItem
        {
            Name = Placeholders.Label(),
            Price = (decimal) Math.Round(s_random.NextDouble() * 100, 2),
            Quantity = s_random.Next(1, 10)
        };
    }

    private static Address GenerateRandomAddress()
    {
        return new Address
        {
            CompanyName = Placeholders.Name(),
            Street = Placeholders.Label(),
            City = Placeholders.Label(),
            Email = Placeholders.Email(),
            Phone = Placeholders.PhoneNumber()
        };
    }
}