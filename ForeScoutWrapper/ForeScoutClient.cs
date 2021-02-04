using System;
using RestSharp;
using RestSharp.Authenticators;
using Newtonsoft.Json;

using ForeScoutWrapper.Data;

namespace ForeScoutWrapper
{

    // What I would also recommend doing is also seeing if 
    // the RESTful service you want to consume conforms to the open API specification.
    // e.g. does the damned thing load in  "Swagger"
    // https://swagger.io/
    //
    // If so then there ARE tools that will autogenerate a LOT of this code for you.
    //
    // Either way, what this code will do is keep the code that interacts with the API somewhat
    // separate from the presentation/interaction layer (be it a command line application, a react/redux site, or <gasp> a PHP app)  
    //
    public class ForeScoutClient:IForeScoutWrapper
    {
        private string JWTToken{get;set;}
        private string OITServerEndPointAddress {get;set;}
        private RestClient client {get;set;}

        public ForeScoutDataObject GetForeScoutDataObjectFromOITSystem(){
            string path="/601b663ed5aafc6431a3af5a/latest";            
            return MakeGetRequestToOITModule<ForeScoutDataObject>(path);
        }

        public ForeScoutClient(){
            // TODO:not sure what sort of system you have in place to actually get a JWT token?
            // I'd break this out into a separate class that would handle this element for you...
            string JWTToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c";


            // In this case I'm just using a placeholder setup that I know will return a blob of JSON
            // https://api.jsonbin.io/b/601b663ed5aafc6431a3af5a/latest

            string OITServerEndPointAddress = "https://api.jsonbin.io/b";

            client = new RestClient(OITServerEndPointAddress);
            // From my own xperience (e.g. GoDaddy's implementation of JWT), is the location of the JWT token MAY
            // have to placed on the request entity as a header or possibly sent in as a cookie.
            // 
            // (e.g. I'd look at the docs)
            //
            // uncomment this line to engage the RestSharp built in JWT auth....
            // client.Authenticator= new JwtAuthenticator(JWTToken);

        }

        private T MakeGetRequestToOITModule<T>(string path){

            var request = new RestRequest(path, DataFormat.Json);

            IRestResponse response = client.Get(request);

            // SO, in this case RestSharp does have the ability to deserialize
            // JSON blobs into objects, but I've found for debugging purposes
            // it's better to split this into two calls? 
            string rawResponseContent  = response.Content;
            
            T deserializedEntity = JsonConvert.DeserializeObject<T>(rawResponseContent);

            return deserializedEntity;
        }
    }
}
