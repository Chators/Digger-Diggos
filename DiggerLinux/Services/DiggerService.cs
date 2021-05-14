using DiggerLinux.Helpers;
using DiggerLinux.Models;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DiggerLinux.Services
{
    public class DiggerService
    {
        readonly IOptions<DiggerServiceOptions> _options;
        readonly TokenService _tokenService;
        readonly ShellHelper _shellHelper;

        public DiggerService(IOptions<DiggerServiceOptions> options, ShellHelper shellHelper, TokenService tokenService)
        {
            _options = options;
            _shellHelper = shellHelper;
            _tokenService = tokenService;
        }

        public async Task SearchData(SearchViewModel model)
        {
            string resultJson = await GetSearchData(model);
            StringViewModel str = new StringViewModel();
            str.Key = resultJson;
            string jsonModel = Json.Serialize<StringViewModel>(str);
            Console.WriteLine(jsonModel);
            
            string url = _options.Value.Url + "api/Diggos/SaveSearchData/" + model.RequestId;

            using (HttpClient client = new HttpClient())
            using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, new Uri(url)))
            {
                //Console.WriteLine("création du fake json");
                /*StringBuilder fakeJson = new StringBuilder();
                fakeJson.AppendLine("{");
                // Un New Logiciel
                fakeJson.AppendLine("[");
                // Un élément
                fakeJson.AppendLine("{");
                fakeJson.AppendLine("\"Data\" : \"+339817264693\",");
                fakeJson.AppendLine("\"Source\" : \"TwitterSearchPhone\",");
                fakeJson.AppendLine("\"TypeOfData\" : \"Phone Number\"");
                fakeJson.AppendLine("}");
                // Un élément
                fakeJson.AppendLine("{");
                fakeJson.AppendLine("\"Data\" : \"0609157788\",");
                fakeJson.AppendLine("\"Source\" : \"TwitterSearchPhone\",");
                fakeJson.AppendLine("\"TypeOfData\" : \"Phone Number\"");
                fakeJson.AppendLine("}");
                fakeJson.AppendLine("]");
                // Un New Logiciel
                fakeJson.AppendLine("[");
                // Un élément
                fakeJson.AppendLine("{");
                fakeJson.AppendLine("\"Data\" : \"arobasemachin@aol.fr\",");
                fakeJson.AppendLine("\"Source\" : \"SearchMailByName\",");
                fakeJson.AppendLine("\"TypeOfData\" : \"Mail\"");
                fakeJson.AppendLine("}");
                // Un élément
                fakeJson.AppendLine("{");
                fakeJson.AppendLine("\"Data\" : \"arobaseohmachin@yahoo.fr\",");
                fakeJson.AppendLine("\"Source\" : \"SearchMailByName\",");
                fakeJson.AppendLine("\"TypeOfData\" : \"Mail\"");
                fakeJson.AppendLine("}");
                // Un élément
                fakeJson.AppendLine("{");
                fakeJson.AppendLine("\"Data\" : \"arobaoijsemachin@darkmdr.fr\",");
                fakeJson.AppendLine("\"Source\" : \"SearchMailByName\",");
                fakeJson.AppendLine("\"TypeOfData\" : \"Mail\"");
                fakeJson.AppendLine("}");
                fakeJson.AppendLine("]");
                fakeJson.AppendLine("}");
                StringViewModel str = new StringViewModel();
                str.Key = fakeJson.ToString();*/
                /*StringViewModel str = new StringViewModel();
                str.Key = "[{\"Data\" : \"EFRIPGFERHIEGROFPEGR\",\"Source\" : \"TwitterSearchPhone\",\"TypeOfData\" : \"Phone Number\", \"Link\" : [{\"Uid\" : \"master\"}]},{\"Data\" : \"0609157788\",\"Source\" : \"TwitterSearchPhone\",\"TypeOfData\" : \"Phone Number\"},{\"Data\" : \"arobasemachin@aol.fr\",\"Source\" : \"SearchMailByName\",\"TypeOfData\" : \"Mail\"},{\"Data\" : \"arobaseohmachin@yahoo.fr\",\"Source\" : \"SearchMailByName\",\"TypeOfData\" : \"Mail\"},{\"Data\" : \"arobaoijsemachin@darkmdr.fr\",\"Source\" : \"SearchMailByName\",\"TypeOfData\" : \"Mail\"}]";
                string jsonModel = Json.Serialize<StringViewModel>(str);*/
                //Console.WriteLine(fakeJson.ToString());
                request.Content = new StringContent(jsonModel, Encoding.UTF8, "application/json");
                request.Headers.Add("Authorization", string.Format("Bearer {0}", _tokenService.GenerateToken()));
                
                await client.SendAsync(request);
            }
        }

        private async Task<string> GetSearchData(SearchViewModel model)
        {
            SearchSoftwareViewModel soft;
            string resultComplete = "[";
            for (int i = 0; i < model.Softwares.Count; i++)
            {
                soft = model.Softwares[i];
                for (int y = 0; y<soft.ResearchModules.Count; y++)
                {
                    Console.WriteLine("execSoftwareOsint.sh " + soft.Name + " " + soft.ResearchModules[y] + " " + model.DataEntity);
                    Console.WriteLine("execSoftwareOsint.sh " + soft.Name + " " + soft.ResearchModules[y] + " " + model.DataEntity);
                    Console.WriteLine("execSoftwareOsint.sh " + soft.Name + " " + soft.ResearchModules[y] + " " + model.DataEntity);
                    Console.WriteLine("execSoftwareOsint.sh " + soft.Name + " " + soft.ResearchModules[y] + " " + model.DataEntity);
                    string result = _shellHelper.Bash("execSoftwareOsint.sh " + soft.Name + " " + soft.ResearchModules[y] + " " + model.DataEntity);
                    resultComplete += result + ",";
                }
            }
            resultComplete = resultComplete.Substring(0, resultComplete.Length - 1);
            resultComplete += "]";
            return resultComplete;
        }
    }

    public class DiggerServiceOptions
    {
        public string Url { get; set; }
    }
}
