using System;
using ForeScoutWrapper;
using ForeScoutWrapper.Data;

namespace ForeScoutCmd
{
    class Program
    {
        // So unless I'm well versed with a given tech stack, the easiest means to learn
        // an API structure is to do things via the command line?
        // 
        
        static void Main(string[] args)
        {
            try{            
                IForeScoutWrapper wrapper = new ForeScoutClient();
                ForeScoutDataObject entity =wrapper.GetForeScoutDataObjectFromOITSystem();

                Console.WriteLine("Entity[Property1]:={0}, Entity[Property2]:={1}",entity.Property1,entity.Property2);
            }
            catch(Exception ex){
                Console.WriteLine(ex.ToString());
            }



        }
    }
}
