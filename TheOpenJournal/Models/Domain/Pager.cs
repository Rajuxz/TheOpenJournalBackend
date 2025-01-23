using AutoMapper.Configuration.Conventions;

namespace TheOpenJournal.Models.Domain
{
    public class PaginationParameters
    {
        //By default, pagesize is 5 and page number is 1
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 5;
    }
}
