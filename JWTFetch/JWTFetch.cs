using System;
using Microsoft.Extensions.Logging;

namespace JWTFetch
{
    public class JWTTokenFetcher:IJWTTokenFetcher
    {
        private static object lockSync=new object();
        private readonly ILogger<JWTTokenFetcher> _logger;
        private static string _cachedToken=null;
        private static DateTime expiresOn=DateTime.Now.AddYears(-5);

        JWTTokenFetcher(ILogger<JWTTokenFetcher> logger){
            _logger=logger;
        }

        private string GetUpdatedToken(){
            //make your requst to your token provider here....you should be able to do this via REST Sharp? 
            string JWTToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c";

            //pretty sure there should be a nuget package that will handle this for you...
            //but the gist is there should be a field that gives info about the TTL of the token
            //which you can use to determine if you need a new value or not;

            expiresOn = DateTime.Now.AddSeconds(300);


            return JWTToken;
        }

        public bool ShouldUpdate(){
            if(_cachedToken==null){
                return true;
            }

            return DateTime.Now> expiresOn;
        }

        public string Token
        {
            get
            {                
                // If there are multiple requests coming in, across several threads, 
                // this is a (somewhat critical section)...so make sure only 
                // one thread updates this at a time!
                lock(lockSync){
                    if(ShouldUpdate()){
                        _cachedToken= GetUpdatedToken();     
                    }
                }

                return _cachedToken;

            }
        }


        
    }
}
