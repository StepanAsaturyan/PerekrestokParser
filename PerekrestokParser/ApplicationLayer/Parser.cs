using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using PerekrestokParser.Common;
using PerekrestokParser.Helpers;
using PerekrestokParser.Infrastructure;
using PerekrestokParser.Interfaces;
using PerekrestokParser.Models;

namespace PerekrestokParser.ApplicationLayer
{
    public class Parser
    {
        private readonly IPerekrestokSettings _settings;
        private string _content;
        private IHtmlDocument _htmlDocument;
        private HtmlParser _htmlParser;
        private List<Product> _products;
        private static HttpClient _httpClient;
        private FileHelper _fileHelper;

        public Parser(IPerekrestokSettings settings)
        {
            _settings = settings;
            _htmlParser = new HtmlParser();
            _products = new List<Product>();
            _httpClient = Network.httpClient;
            _fileHelper = new FileHelper();
        }

        public async Task HandlePage(string link = null, string category = null)
        {
            if (link is not null)
            {
                _content = await _httpClient.GetStringAsync(link);
            }
            _htmlDocument = _htmlParser.ParseDocument(_content);
            string division = _htmlDocument.GetElementsByClassName("catalog-category__title").FirstOrDefault()?.TextContent;
            if (division is null)
            {
                division = _htmlDocument.GetElementsByClassName("page-header__title").FirstOrDefault()?.TextContent;
            }
            var allBlocksWithPrice = _htmlDocument.All.Where(b => b.ClassName == "product-card__content");

            foreach (var item in allBlocksWithPrice)
            {
                await GetProductContent(item);
            }

            _fileHelper.SaveDataToFile(_products.ToArray(), category,  division);
            _products.Clear();
        }

        public async Task HandleAllCatalogue()
        {
            _htmlDocument = _htmlParser.ParseDocument(_content);
            var catalogueItems = _htmlDocument.All.Where(p => p.ClassName == "sc-dlfnbm jwhrZg");

            foreach (var subCatalogue in catalogueItems)
            {
                var prefix = subCatalogue.GetElementsByClassName("sc-dQppl KquLJ").FirstOrDefault().GetAttribute("href");
                string linkToCategoryPage = $"{_settings.BaseUrl[..^4]}{prefix}";

                (List<string> listOfLinks, string categoryName)  = await GetListOfLinks(linkToCategoryPage);

                foreach (var finalLink in listOfLinks)
                {
                    await HandlePage(finalLink, categoryName);
                }                
            }
        }

        private async Task<(List<string>, string)> GetListOfLinks(string link)
        {
            var pages = await _httpClient.GetStringAsync(link);
            var htmlDocument = _htmlParser.ParseDocument(pages);
            var allBlocksWithLinks = htmlDocument.GetElementsByClassName("products-slider__header");
            IEnumerable<INode> anchors = allBlocksWithLinks.Select(b => b.LastChild);
            var allPrefixes = anchors.Select(a => ((IHtmlAnchorElement)a).GetAttribute("href"));
            var allLinks = allPrefixes.Select(p => $"{_settings.BaseUrl[..^4]}{p}").ToList();
            string categoryName = htmlDocument.GetElementsByClassName("catalog-category__title").FirstOrDefault().TextContent;

            return (allLinks, categoryName);
        }


        private Task GetProductContent(IElement productBlock)
        {
            var divWithName = productBlock.Children.Where(c => c.ClassName == "product-card-title__wrapper");
            var productName = divWithName!.FirstOrDefault().TextContent;

            // var divWithPrices = productBlock.GetElementsByClassName("product-card__price");
            string oldPrice;
            string actualPrice;
            oldPrice = productBlock.GetElementsByClassName("price-old-wrapper").FirstOrDefault()?.TextContent ?? "-";
            actualPrice = productBlock.GetElementsByClassName("price-new").FirstOrDefault().TextContent;

            var rating = productBlock.GetElementsByClassName("rating-value");
            var productRating = rating.FirstOrDefault().TextContent;

            var divWithNumOfVotes = productBlock.GetElementsByClassName("product-card__review-count");
            var productNumOfVotes = divWithNumOfVotes.FirstOrDefault().TextContent;

            var divWithProductPricing = productBlock.GetElementsByClassName("product-card__pricing");
            var productPricing = divWithProductPricing.FirstOrDefault().TextContent;

            _products.Add(new Product(productName, oldPrice, actualPrice, productRating, productNumOfVotes, productPricing));

            return Task.CompletedTask;
        }


        public async Task CheckSiteAvailability()
        {
            bool result = Uri.TryCreate(_settings.BaseUrl, UriKind.Absolute, out Uri uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            if (result)
            {
                _content = await _httpClient.GetStringAsync(uriResult);

                if (_content is null)
                {
                    throw new ParserException($"Сайт {uriResult} недоступен", Error.A002);
                }
            }
            else
            {
                throw new ParserException($"Не удается распознать адрес", Error.A001);
            }
        }
    }
}