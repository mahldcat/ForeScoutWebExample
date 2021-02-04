using ForeScoutWrapper.Data;

namespace ForeScoutWrapper
{
    // This is another mechanism that I like to utilize to separate the "what are you doing" from the "how do you do it"
    //
    // Interfaces are also the preferred entity to use when utilizing Dependency Injection (e.g. when this gets used in the ASP.Net website) 
    public interface IForeScoutWrapper{
        ForeScoutDataObject GetForeScoutDataObjectFromOITSystem();
    }
    
}
