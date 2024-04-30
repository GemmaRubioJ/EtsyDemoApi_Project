using Api.Data.Repository.Queries;
using Api.Domain.Enum;
using Api.Service.Queries;
using Xunit;

namespace Api.Test.Queries
{
    public class GetProductsQueryTest
    {
        private const string BD = "Server=localhost\\SQLEXPRESS;Database=EtsyPruebas;User Id=GFT\\gare;Password=GFTgft%2024;Trusted_Connection=True;TrustServerCertificate=True";
        private readonly IEtsyQuery _etsyQyery;
        private readonly IGetEtsyService _getEtsyService;
        private readonly HttpClient _httpclient;
        public GetProductsQueryTest(IEtsyQuery etsyQuery, HttpClient httpclient, IGetEtsyService getEtsyService)
        {
            _etsyQyery = etsyQuery;
            _httpclient = httpclient;
            _getEtsyService = getEtsyService;
        }

        [Fact] 
        public async Task Get_Shops_Ok()
        {
            ////ARRANGE
            ////ACT
            //var result = await _getEtsyService.GetProductsAsync();
            ////ASSERT
            //Assert.Equals(StatusType.SUCCESS, result.Status);
            try
            {
                // ACT
                var result = await _getEtsyService.GetProductsAsync();

                // ASSERT
                Assert.Equals(StatusType.SUCCESS, result.Status);  // Asegúrate de usar Equal para comparar valores
            }
            catch (Exception ex)
            {
                // Log the exception or handle it
                Assert.True(false, "Exception thrown: " + ex.Message);
            }
        }
    }
}
